import { useState } from "react";
import "./ManagementMedicalCenters.css";

const ManagementMedicalCenters = () => {
  const [medicCenters, setMedicCenters] = useState([
    {
      medicCenter: "Sanatorio Oroño",
      user: "oroño457",
      password: "1234567",
    },
    {
      medicCenter: "Sanatorio Británico",
      user: "britanico34556",
      password: "789654",
    },
    {
      medicCenter: "Sanatorio Delta",
      user: "delta789",
      password: "444546",
    },
    {
      medicCenter: "Clínica Gallo",
      user: "gallo1243",
      password: "99969",
    },
    {
      medicCenter: "Hospital de Niño",
      user: "niños1445",
      password: "12039777777",
    },
  ]);

  const [editIndex, setEditIndex] = useState(null);
  const [editText, setEditText] = useState({});
  const [showPassword, setShowPassword] = useState(false);

  const handleEditClick = (index) => {
    setEditIndex(index);
    setEditText({ ...medicCenters[index] });
    setShowPassword(true);
  };

  const handleSaveClick = (index) => {
    const updatedCenters = [...medicCenters];
    updatedCenters[index] = editText;
    setMedicCenters(updatedCenters);
    setEditIndex(null);
    setShowPassword(false);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditText((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const removeCenter = (index) => {
    const updatedCenters = medicCenters.filter((_, i) => i !== index);
    setMedicCenters(updatedCenters);
  };

  return (
    <div className="container">
      <h1>Centros Médicos</h1>
      <table>
        <thead>
          <tr>
            <th>Centro Médico</th>
            <th>Usuario</th>
            <th>Contraseña</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {medicCenters.map((medicCenter, index) => (
            <tr key={index}>
              {editIndex === index ? (
                <>
                  <td>
                    <input
                      type="text"
                      name="medicCenter" // Nombre debe ser "medicCenter"
                      value={editText.medicCenter}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type="text"
                      name="user" // Nombre debe ser "user"
                      value={editText.user}
                      onChange={handleInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type={showPassword ? "text" : "password"}
                      name="password" // Nombre debe ser "password"
                      value={editText.password}
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
                  <td>{medicCenter.medicCenter}</td>
                  <td>{medicCenter.user}</td>
                  <td>******</td> {/* Mostrar asteriscos para la contraseña */}
                  <td>
                    <button
                      className="editar"
                      onClick={() => handleEditClick(index)}
                    >
                      Editar
                    </button>
                    <button
                      className="eliminar"
                      onClick={() => removeCenter(index)}
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

export default ManagementMedicalCenters;
