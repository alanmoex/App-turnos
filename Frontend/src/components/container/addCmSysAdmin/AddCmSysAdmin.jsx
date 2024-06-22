import { useNavigate } from "react-router-dom";
import "./AddCmSysAdmin.css";
const AddCmSysAdmin = () => {
  const navigate = useNavigate();

  const handleHomePageClick = () => {
    navigate("/");
  };

  return (
    <div className="registro-container">
      <div className="registro-form">
        <h2>Agregar centro medico</h2>
        <form>
          <div className="form-group">
            <label>Nombre</label>
            <input type="text" name="nombre" />
          </div>
          <div className="form-group">
            <label>Email</label>
            <input type="email" name="email" />
          </div>
          <div className="form-group">
            <label>Contraseña</label>
            <input type="password" name="password" />
          </div>
          <button onClick={handleHomePageClick}>Agregar centro medico</button>
        </form>
      </div>
    </div>
  );
};

export default AddCmSysAdmin;