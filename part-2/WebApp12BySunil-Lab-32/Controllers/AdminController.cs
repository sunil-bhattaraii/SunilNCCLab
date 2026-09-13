using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp12BySunil_Lab_32.Models;

namespace WebApp12BySunil_Lab_32.Controllers;

// The whole Admin panel is protected by the "AdminOnly" policy (Admin role).
[Authorize(Policy = "AdminOnly")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private string? CurrentUserId => _userManager.GetUserId(User);

    // -------------------- ROLES --------------------
    public async Task<IActionResult> Roles()
    {
        var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();

        // RoleManager does not expose a navigation property for claims, so fetch them individually.
        var roleClaims = new Dictionary<string, IList<System.Security.Claims.Claim>>();
        foreach (var role in roles)
            roleClaims[role.Id!] = await _roleManager.GetClaimsAsync(role);

        ViewBag.RoleClaims = roleClaims;
        return View(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(string name)
    {
        name = name ?? "";
        if (await _roleManager.RoleExistsAsync(name))
        {
            TempData["StatusMessage"] = $"Role '{name}' already exists.";
        }
        else
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            TempData["StatusMessage"] = result.Succeeded ? $"Role '{name}' created." : "Create failed.";
        }
        return RedirectToAction(nameof(Roles));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RenameRole(string id, string name)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is not null && !string.IsNullOrWhiteSpace(name) && role.Name != name)
        {
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            TempData["StatusMessage"] = result.Succeeded ? $"Role renamed to '{name}'." : "Rename failed.";
        }
        return RedirectToAction(nameof(Roles));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
            return RedirectToAction(nameof(Roles));

        if ((await _userManager.GetUsersInRoleAsync(role.Name!)).Count > 0)
        {
            TempData["StatusMessage"] = $"Role '{role.Name}' is assigned to users and cannot be deleted.";
            return RedirectToAction(nameof(Roles));
        }

        var result = await _roleManager.DeleteAsync(role);
        TempData["StatusMessage"] = result.Succeeded ? $"Role '{role.Name}' deleted." : "Delete failed.";
        return RedirectToAction(nameof(Roles));
    }

    // claimId is not used here - role claims are addressed by Type+Value
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRoleClaim(string roleId, string type, string value)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is not null && !string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(value))
        {
            var result = await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(type, value));
            TempData["StatusMessage"] = result.Succeeded ? $"Claim '{type}={value}' added to role '{role.Name}'." : "Could not add claim.";
        }
        return RedirectToAction(nameof(Roles));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRoleClaim(string roleId, string type, string value)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is not null)
        {
            var existing = (await _roleManager.GetClaimsAsync(role)).FirstOrDefault(c => c.Type == type && c.Value != value);
            if (existing is not null)
            {
                await _roleManager.RemoveClaimAsync(role, existing);
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(type, value));
                TempData["StatusMessage"] = $"Claim '{type}' updated to '{value}'.";
            }
        }
        return RedirectToAction(nameof(Roles));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRoleClaim(string roleId, string type, string value)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is not null)
        {
            var claim = (await _roleManager.GetClaimsAsync(role)).FirstOrDefault(c => c.Type == type && c.Value == value);
            if (claim is not null)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
                TempData["StatusMessage"] = $"Claim '{type}={value}' removed from role '{role.Name}'.";
            }
        }
        return RedirectToAction(nameof(Roles));
    }

    // -------------------- USERS --------------------
    public async Task<IActionResult> Users()
    {
        var items = new List<UserListItem>();

        foreach (var u in (await _userManager.Users.OrderBy(u => u.Email).ToListAsync()))
        {
            items.Add(new UserListItem
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Roles = (await _userManager.GetRolesAsync(u)).ToList(),
                ClaimCount = (await _userManager.GetClaimsAsync(u)).Count
            });
        }

        return View(items);
    }

    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);

        var roles = (await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync()).Select(r => new UserRoleItem
        {
            RoleName = r.Name,
            Selected = userRoles.Contains(r.Name!)
        }).ToList();

        return View(new ManageUserVM
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Claims = (await _userManager.GetClaimsAsync(user)).ToList(),
            Roles = roles
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(string id, List<string> selectedRoles)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return NotFound();

        var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
        selectedRoles ??= new List<string>();

        // assign newly selected roles
        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        // revoke unselected roles
        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        TempData["StatusMessage"] = $"Roles of '{user.Email}' updated.";
        return RedirectToAction(nameof(EditUser), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUserClaim(string id, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null && !string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(value))
        {
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(type, value));
            TempData["StatusMessage"] = $"Claim '{type}={value}' added to '{user.Email}'.";
        }
        return RedirectToAction(nameof(EditUser), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUserClaim(string id, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            var existing = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == type && c.Value != value);
            if (existing is not null)
            {
                await _userManager.RemoveClaimAsync(user, existing);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(type, value));
                TempData["StatusMessage"] = $"Claim '{type}' updated to '{value}'.";
            }
        }
        return RedirectToAction(nameof(EditUser), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserClaim(string id, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            var claim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == type && c.Value == value);
            if (claim is not null)
            {
                await _userManager.RemoveClaimAsync(user, claim);
                TempData["StatusMessage"] = $"Claim '{type}={value}' removed from '{user.Email}'.";
            }
        }
        return RedirectToAction(nameof(EditUser), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return RedirectToAction(nameof(Users));

        if (user.Id == CurrentUserId)
        {
            TempData["StatusMessage"] = "You cannot delete your own account.";
            return RedirectToAction(nameof(Users));
        }

        var result = await _userManager.DeleteAsync(user);
        TempData["StatusMessage"] = result.Succeeded ? $"User '{user.Email}' deleted." : "Delete failed.";
        return RedirectToAction(nameof(Users));
    }
}