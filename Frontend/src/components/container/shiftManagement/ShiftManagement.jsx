import { useState } from "react";
import "./ShiftManagement.css";

const ShiftManagement = () => {
  const [turnos, setTurnos] = useState([
    {
      fecha: "2024-05-10",
      hora: "09:00",
      paciente: "Juan Pérez",
      medico: "Dr. Garcia",
      especialidad: "Cardiología",
      estado: "Confirmado",
    },
    {
      fecha: "2024-05-10",
      hora: "10:30",
      paciente: "Carlos Zapata",
      medico: "Dra. Martínez",
      especialidad: "Traumatólogo",
      estado: "Cancelado",
    },
    {
      fecha: "2024-05-10",
      hora: "11:30",
      paciente: "Jorge Martínez",
      medico: "Dr. González",
      especialidad: "Oncología",
      estado: "Cancelado",
    },
    {
      fecha: "2024-05-10",
      hora: "12:30",
      paciente: "Laura Fernández",
      medico: "Dra. Hernández",
      especialidad: "Cardiología",
      estado: "Confirmado",
    },
    {
      fecha: "2024-05-10",
      hora: "13:00",
      paciente: "Miguel Cervantes",
      medico: "Dr. Maldonado",
      especialidad: "Ginecología",
      estado: "Confirmado",
    },
  ]);

  const [editIndex, setEditIndex] = useState(null);
  const [editText, setEditText] = useState({});

  const handleEditClick = (index) => {
    setEditIndex(index);
    setEditText({ ...turnos[index] });
  };

  const handleSaveClick = (index) => {
    const newShifts = [...turnos];
    newShifts[index] = editText;
    setTurnos(newShifts);
    setEditIndex(null);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditText((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const removeShiftsHandle = (index) => {
    const remTurnos = turnos.filter((_, i) => i !== index);
    setTurnos(remTurnos);
  };
  return (
    <>
      <div className="container">
        <h1>Centro Médico</h1>
        <table>
          <thead>
            <tr>
              <th>Fecha</th>
              <th>Hora</th>
              <th>Paciente</th>
              <th>Medico</th>
              <th>Especialidad</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {turnos.map((turno, index) => (
              <tr key={index}>
                {editIndex === index ? (
                  <>
                    <td>
                      <input
                        type="text"
                        name="fecha"
                        value={editText.fecha}
                        onChange={handleInputChange}
                      />
                    </td>
                    <td>
                      <input
                        type="text"
                        name="hora"
                        value={editText.hora}
                        onChange={handleInputChange}
                      />
                    </td>
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
                        name="medico"
                        value={editText.medico}
                        onChange={handleInputChange}
                      />
                    </td>
                    <td>
                      <input
                        type="text"
                        name="especialidad"
                        value={editText.especialidad}
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
                    <td>{turno.fecha}</td>
                    <td>{turno.hora}</td>
                    <td>{turno.paciente}</td>
                    <td>{turno.medico}</td>
                    <td>{turno.especialidad}</td>
                    <td>{turno.estado}</td>
                    <td>
                      <button
                        className="editar"
                        onClick={() => handleEditClick(index)}
                      >
                        Editar
                      </button>
                      <button
                        className="cancelar"
                        onClick={() => removeShiftsHandle(index)}
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
    </>
  );
};

export default ShiftManagement;
