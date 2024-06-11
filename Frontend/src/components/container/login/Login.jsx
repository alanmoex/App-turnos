import { useRef, useState } from "react";
import "./Login.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState({ username: false, password: false });
  const [message, setMessage] = useState("");
  const userRef = useRef(null);
  const passRef = useRef(null);

  const usernameHandler = (event) => {
    setError({ ...error, username: false });
    setUsername(event.target.value);
  };

  const passwordHandler = (event) => {
    setError({ ...error, password: false });
    setPassword(event.target.value);
  };

  const loginHandler = (event) => {
    event.preventDefault();
    if (userRef.current.value.length === 0) {
      userRef.current.focus();
      setError({ ...error, username: true });
      return alerts();
    }

    if (passRef.current.value.length === 0) {
      passRef.current.focus();
      setError({ ...error, password: true });
      return alerts();
    }

    alert("Has ingresado correctamente");
  };

  const alerts = () => {
    if (password === "") {
      setMessage("Debe ingresar una contraseña válida");
    }

    if (username === "") {
      setMessage("Debe ingresar un usuario válido");
    }

    if (username != "" && password != "") {
      setMessage("");
    }
  };
  return (
    <div className="login-container">
      <div className="backGround"></div>
      <div className="login-form">
        <h2>
          <b>MediCare</b>
        </h2>
        <form onSubmit={loginHandler}>
          <div className="form-group">
            <input
              ref={userRef}
              className={error.username && "border border-danger"}
              type="text"
              id="username"
              name="username"
              placeholder="Usuario"
              value={username}
              onChange={usernameHandler}
            />
          </div>
          <div className="form-group">
            <input
              ref={passRef}
              className={error.password && "border border-danger"}
              type="text"
              id="password"
              name="password"
              placeholder="Contraseña"
              value={password}
              onChange={passwordHandler}
            ></input>
          </div>
          <p>{message}</p>
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
}

export default Login;
