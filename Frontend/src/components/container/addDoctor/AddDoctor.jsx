import { useNavigate } from "react-router-dom";
import "./AddDoctor.css";
const AddDoctor = () => {
  const navigate = useNavigate();

  const handleHomePageClick = () => {
    navigate("/");
  };

  return (
    <div className="registro-container">
      <div className="registro-form">
        <h2>Ingresar datos del nuevo doctor</h2>
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
            <label>Genero</label>
            <input type="text" name="genero" />
          </div>
          <div className="form-group">
            <label>Especialidad</label>
            <input type="email" name="Especialidad" />
          </div>
          <div className="form-group">
            <label>N° de matricula</label>
            <input type="text" name="matricula" />
          </div>
          <button onClick={handleHomePageClick}>Agregar medico</button>
        </form>
      </div>
    </div>
  );
};

export default AddDoctor;
