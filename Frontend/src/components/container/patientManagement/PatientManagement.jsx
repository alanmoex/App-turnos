import { useState } from "react";
import "./PatientManagement.css";

const PatientManagement = () => {
  const [patients, setPatients] = useState([
    {
      paciente: "Juan Pérez",
      dni: "40878964",
      mail: "juanperez@gmail.com",
      estado: "Confirmado",
    },
    {
      paciente: "Carlos Zapata",
      dni: "12456879",
      mail: "carloszapata@gmail.com",
      estado: "Cancelado",
    },
    {
      paciente: "Jorge Martínez",
      dni: "35648975",
      mail: "jorgemartinez@gmail.com",
      estado: "Cancelado",
    },
    {
      paciente: "Laura Fernández",
      dni: "34568712",
      mail: "laurafernandez@gmail.com",
      estado: "Confirmado",
    },
    {
      paciente: "Miguel Cervantes",
      dni: "36547894",
      mail: "miguelcervantes@gmail.com",
      estado: "Confirmado",
    },
  ]);

  const [editIndex, setEditIndex] = useState(null);
  const [editText, setEditText] = useState({});

  const handleEditClick = (index) => {
    setEditIndex(index);
    setEditText({ ...patients[index] });
  };

  const handleSaveClick = (index) => {
    const newPatients = [...patients];
    newPatients[index] = editText;
    setPatients(newPatients);
    setEditIndex(null);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditText((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const removePatient = (index) => {
    const updatedPatients = patients.filter((_, i) => i !== index);
    setPatients(updatedPatients);
  };

  return (
    <div className="container">
      <h1>Gestión de Pacientes</h1>
      <table>
        <thead>
          <tr>
            <th>Paciente</th>
            <th>DNI</th>
            <th>Mail</th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {patients.map((patient, index) => (
            <tr key={index}>
              {editIndex === index ? (
                <>
                  <td>
                    <input
                      type="text"
                      name="paciente"
                      value={editText.paciente}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type="text"
                      name="dni"
                      value={editText.dni}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type="text"
                      name="mail"
                      value={editText.mail}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type="text"
                      name="estado"
                      value={editText.estado}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <button
                      className="guardar"
                      onClick={() => handleSaveClick(index)}
                    >
                      Guardar
                    </button>
                    <button
                      className="cancelar"
                      onClick={() => setEditIndex(null)}
                    >
                      Cancelar
                    </button>
                  </td>
                </>
              ) : (
                <>
                  <td>{patient.paciente}</td>
                  <td>{patient.dni}</td>
                  <td>{patient.mail}</td>
                  <td>{patient.estado}</td>
                  <td>
                    <button
                      className="editar"
                      onClick={() => handleEditClick(index)}
                    >
                      Editar
                    </button>
                    <button
                      className="eliminar"
                      onClick={() => removePatient(index)}
                    >
                      Eliminar
                    </button>
                  </td>
                </>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default PatientManagement;
