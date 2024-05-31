import React, { useState } from "react";
import "./Login.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    if (username === "" || password === "") {
      alert("Por favor, complete todos los campos.");
    } else {
      alert("Has ingresado correctamente");
    }
  };
  return (
    <div className="login-container">
      <div className="login-form">
        <h2>MediCare</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <input
              type="text"
              id="username"
              name="username"
              placeholder="Usuario"
              value={username}
              onChange={(usernameLogin) =>
                setUsername(usernameLogin.target.value)
              }
            />
          </div>
          <div className="form-group">
            <input
              type="text"
              id="password"
              name="password"
              placeholder="Contraseña"
              value={password}
              onChange={(passwordLogin) =>
                setPassword(passwordLogin.target.value)
              }
            ></input>
          </div>
          <button type="submit">Login</button>
        </form>
      </div>
    </div>
  );
}

export default Login;
