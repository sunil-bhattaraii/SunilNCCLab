const Header = () => {
  return (
    <nav className="flex gap-8 justify-center py-4 bg-gray-200">
      <span className="font-bold"> AngularApp2BySunil </span>

      <div>
        <ul className="flex font-thin gap-4">
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/calculator">Calculator</a>
          </li>
        </ul>
      </div>
    </nav>
  );
}

export default Header
