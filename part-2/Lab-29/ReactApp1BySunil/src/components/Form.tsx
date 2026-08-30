import { useState } from 'react';
const pwregex = /^(?=.*[A-Za-z])(?=.*\d).{8,}$/;

const Form = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [cpassword, setCpassword] = useState('');
  const [error, setError] = useState('');

  function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
    e.preventDefault();
    if (username.length < 4) {
      setError('Username must be greater than 4 characters');
    } else if (!pwregex.test(password)) {
      setError(
        'Password must be 8 characters long and should contain at least one alphabet and one number'
      );
    } else if (password !== cpassword) {
      setError("The passwords don't match");
    } else {
      alert('Form Validated and submitted Successfully');
    }
  }

  return (
    <>
      <form onSubmit={handleSubmit}>
        <fieldset>
          <legend>Registration Form</legend>
          Username:
          <input
            type="text"
            name="Username"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <br />
          Password:
          <input
            type="text"
            name="password"
            id="p"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <br />
          Confirm Password:
          <input
            type="text"
            name="confirm-password"
            id="cp"
            value={cpassword}
            onChange={(e) => setCpassword(e.target.value)}
          />
          <br />
          {error && (
            <div id="error" style={{ color: 'red' }}>
              {error}
            </div>
          )}
          <br />
          <button type="submit">Sign Up</button>
        </fieldset>
      </form>
    </>
  );
};

export default Form;
