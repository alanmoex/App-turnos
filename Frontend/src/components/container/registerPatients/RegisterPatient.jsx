import "./RegisterPatient.css";
import { useNavigate } from "react-router-dom";
const RegisterPatient = () => {
  const navigate = useNavigate();

  const handleHomePageClick = () => {
    navigate("/");
  };

  return (
    <div className="registro-container">
      <div className="registro-form">
        <h2>Registrarse</h2>
        <form>
          <div className="form-group">
            <label>Nombre</label>
            <input type="text" name="nombre" />
          </div>
          <div className="form-group">
            <label>Apellido</label>
            <input type="text" name="apellido" />
          </div>
          <div className="form-group">
            <label>Dni</label>
            <input type="text" name="dni" />
          </div>
          <div className="form-group">
            <label>Email</label>
            <input type="email" name="email" />
          </div>
          <div className="form-group">
            <label>Contraseña</label>
            <input type="password" name="password" />
          </div>
          <button onClick={handleHomePageClick}>Registrarse</button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPatient;
