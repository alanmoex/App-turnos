import "./AddDoctor.css";
import { useState, useContext, useEffect } from "react";
import { API_BASE_URL } from "../../../api";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";

const AddDoctor = () => {
  const [name, setName] = useState("");
  const [lastName, setLastName] = useState("");
  const [licenseNumber, setLicenseNumber] = useState("");
  const [specialties, setSpecialties] = useState("");
  const [medicalCenterId, setMedicalCenterId] = useState(null);
  const { userInfo } = useContext(AuthenticationContext);

  useEffect(() => {
    const fetchMedicalCenterId = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/AdminMC/${userInfo.sub}`);
        if (!response.ok) throw new Error("Error fetching medical center data");
        const data = await response.json();
        setMedicalCenterId(data.medicalCenter.id);
      } catch (error) {
        console.error("Error fetching medical center data:", error);
      }
    };

    if (userInfo && userInfo.sub) {
      fetchMedicalCenterId();
    }
  }, [userInfo]);

  const changeNameHandler = (event) => {
    setName(event.target.value);
  };

  const changeLastNameHandler = (event) => {
    setLastName(event.target.value);
  };

  const changeLicenseNumberHandler = (event) => {
    setLicenseNumber(event.target.value);
  };

  const changeSpecialtiesHandler = (event) => {
    setSpecialties(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const specialtiesArray = specialties.split(",").map(Number);

    const newMedic = {
      name: name,
      lastName: lastName,
      licenseNumber: licenseNumber,
      medicalCenterId: medicalCenterId,
      specialties: specialtiesArray,
    };
    console.log(newMedic);

    try {
      const response = await fetch(`${API_BASE_URL}/Medic`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newMedic),
      });
      if (!response.ok) throw new Error("Error al crear el medico");
      alert("Medico agregado correctamente");
    } catch (error) {
      console.error("Error al crear usuario:", error);
    }
  };

  return (
    <div className="registro-container">
      <div className="registro-form">
        <h2>Ingresar datos del nuevo doctor</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Nombre</label>
            <input
              type="text"
              name="nombre"
              value={name}
              onChange={changeNameHandler}
            />
          </div>
          <div className="form-group">
            <label>Apellido</label>
            <input
              type="text"
              name="apellido"
              value={lastName}
              onChange={changeLastNameHandler}
            />
          </div>
          <div className="form-group">
            <label>Especialidades</label>
            <input
              type="text"
              name="especialidades"
              value={specialties}
              onChange={changeSpecialtiesHandler}
            />
          </div>
          <div className="form-group">
            <label>N° de matricula</label>
            <input
              type="text"
              name="matricula"
              value={licenseNumber}
              onChange={changeLicenseNumberHandler}
            />
          </div>
          <button type="submit" disabled={medicalCenterId === null}>
            Agregar medico
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddDoctor;
