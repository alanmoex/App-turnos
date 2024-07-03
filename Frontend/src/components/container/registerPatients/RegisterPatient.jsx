import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./RegisterPatient.css";
import { API_BASE_URL } from "../../../api";

const RegisterPatient = () => {
  const navigate = useNavigate();
  const [userType, setUserType] = useState("Patient");
  const [name, setName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [medicalCenterId, setMedicalCenterId] = useState("");

  const handleHomePageClick = () => {
    navigate("/");
  };

  const changeNameHandler = (event) => {
    setName(event.target.value);
  };

  const changeLastNameHandler = (event) => {
    setLastName(event.target.value);
  };

  const changeEmailHandler = (event) => {
    setEmail(event.target.value);
  };

  const changePasswordHandler = (event) => {
    setPassword(event.target.value);
  };

  const changeMedicalCenterIdHandler = (event) => {
    setMedicalCenterId(event.target.value);
  };

  const changeUserType = (event) => {
    setUserType(event.target.value);
  };

  useEffect(() => {}, [userType]);

  const handleSubmit = async (event) => {
    event.preventDefault();

    let newUser = {};
    switch (userType) {
      case "AdminMC":
        newUser = {
          name: name,
          email: email,
          password: password,
          medicalCenterId: parseInt(medicalCenterId),
        };
        break;
      case "Patient":
        newUser = {
          name: name,
          lastName: lastName,
          email: email,
          password: password,
        };
        break;
      case "SysAdmin":
        newUser = {
          name: name,
          email: email,
          password: password,
        };
        break;
      default:
        break;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/${userType}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newUser),
      });
      if (!response.ok) throw new Error(`Error al crear ${userType}`);
      handleHomePageClick();
    } catch (error) {
      console.error("Error al crear usuario:", error);
    }
  };

  return (
    <div className="registro-container">
      <div className="registro-form">
        <h2>Registrarse</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Tipo de Usuario</label>
            <select value={userType} onChange={changeUserType}>
              <option value="Patient">Paciente</option>
              <option value="AdminMC">Administrador de Centro Médico</option>
              <option value="SysAdmin">Administrador de Sistema</option>
            </select>
          </div>
          <div className="form-group">
            <label>Nombre</label>
            <input
              type="text"
              name="nombre"
              value={name}
              onChange={changeNameHandler}
            />
          </div>
          {userType === "Patient" && (
            <div className="form-group">
              <label>Apellido</label>
              <input
                type="text"
                name="apellido"
                value={lastName}
                onChange={changeLastNameHandler}
              />
            </div>
          )}
          {userType === "AdminMC" && (
            <div className="form-group">
              <label>Centro Médico ID</label>
              <input
                type="number"
                name="medicalCenterId"
                value={medicalCenterId}
                onChange={changeMedicalCenterIdHandler}
              />
            </div>
          )}
          <div className="form-group">
            <label>Email</label>
            <input
              type="email"
              name="email"
              value={email}
              onChange={changeEmailHandler}
            />
          </div>
          <div className="form-group">
            <label>Contraseña</label>
            <input
              type="password"
              name="password"
              value={password}
              onChange={changePasswordHandler}
            />
          </div>
          <button type="submit">Registrarse</button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPatient;
