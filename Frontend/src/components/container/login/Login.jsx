import { useState, useRef, useContext } from "react";
import * as Components from "./Components";
import { RoleContext } from "../../common/contextRole/ContextRole";
import { useNavigate } from "react-router-dom";

function Login() {
  const navigate = useNavigate();
  const { setRole } = useContext(RoleContext);
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
    if (username.length === 0) {
      userRef.current.focus();
      setError({ ...error, username: true });
      setMessage("Debe ingresar un usuario válido");
      return;
    }

    if (password.length === 0) {
      passRef.current.focus();
      setError({ ...error, password: true });
      setMessage("Debe ingresar una contraseña válida");
      return;
    }

    alert("Has ingresado correctamente");
    setRole("patient");
    navigate("/");
  };

  const [signIn, toggle] = useState(true);

  return (
    <>
      <Components.Conteiner>
        <Components.SignUpContainer signinIn={signIn}>
          <Components.Form onSubmit={loginHandler}>
            <Components.Title>Crear cuenta</Components.Title>
            <Components.Input
              type="text"
              placeholder="Nombre"
              value={username}
              onChange={usernameHandler}
            />
            <Components.Input type="text" placeholder="Apellido" />
            <Components.Input type="text" placeholder="DNI" />
            <Components.Input type="email" placeholder="Email" />
            <Components.Input
              type="password"
              placeholder="Contraseña"
              value={password}
              onChange={passwordHandler}
            />
            <Components.Button>Registrarse</Components.Button>
          </Components.Form>
        </Components.SignUpContainer>

        <Components.SignInContainer signinIn={signIn}>
          <Components.Form onSubmit={loginHandler}>
            <Components.Title>MediCare</Components.Title>
            <Components.Input
              type="text"
              placeholder="Usuario"
              value={username}
              onChange={usernameHandler}
              ref={userRef}
            />
            <Components.Input
              type="password"
              placeholder="Contraseña"
              value={password}
              onChange={passwordHandler}
              ref={passRef}
            />
            <p>{message}</p>
            <Components.Button>Login</Components.Button>
          </Components.Form>
        </Components.SignInContainer>

        <Components.OverlayContainer signinIn={signIn}>
          <Components.Overlay signinIn={signIn}>
            <Components.LeftOverLayPanel signinIn={signIn}>
              <Components.Title>¡Bienvenido!</Components.Title>
              <Components.Paragraph>
                Ingrese sus datos y únase a nosotros!
              </Components.Paragraph>
              <Components.GhostButton onClick={() => toggle(true)}>
                Ingresar
              </Components.GhostButton>
            </Components.LeftOverLayPanel>

            <Components.RightOverLayPanel signinIn={signIn}>
              <Components.Title>Hola, Usuario/a!</Components.Title>
              <Components.Paragraph>
                Para solicitar, modificar o eliminar sus turnos, ingrese su
                usuario y contraseña.
                <p>
                  <span>Si no tienes una cuenta, hacé click acá!</span>
                </p>
              </Components.Paragraph>
              <Components.GhostButton onClick={() => toggle(false)}>
                Registrarse
              </Components.GhostButton>
            </Components.RightOverLayPanel>
          </Components.Overlay>
        </Components.OverlayContainer>
      </Components.Conteiner>
    </>
  );
}

export default Login;
/*import { useRef, useState } from "react";
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
*/
