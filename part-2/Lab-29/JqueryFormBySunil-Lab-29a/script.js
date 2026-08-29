document.getElementById('form').addEventListener('submit', (e) => {
  e.preventDefault();
  handleSubmit();
});

let pwregex = /^(?=.*[A-Za-z])(?=.*\d).{8,}$/;

function handleSubmit() {
  if ($('#username').val().length < 4) {
    $('#error').text('Username must be greater than 4 characters');
  } else if (!pwregex.test($('#p').val())) {
    $('#error').text(
      'Password must be 8 characters long and should contain at least one alphabet and one number'
    );
  } else if ($('#p').val() !== $('#cp').val()) {
    $('#error').text("The passwords don't match");
  } else {
    document.write('Form Validated and submitted Successfully');
  }
}
