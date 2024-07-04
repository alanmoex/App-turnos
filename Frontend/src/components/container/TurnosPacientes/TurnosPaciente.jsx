import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";
import { useEffect, useState } from "react";
import "./TurnosPaciente.css";
import { API_BASE_URL } from "../../../api";
import { useNavigate } from "react-router-dom";

function TurnosPacientes() {
  const navigate = useNavigate();
  const [centrosMedicos, setCentrosMedicos] = useState([]);
  const [especialidades, setEspecialidades] = useState([]);
  const [centroMedicoSeleccionado, setCentroMedicoSeleccionado] = useState("");
  const [especialidadSeleccionada, setEspecialidadSeleccionada] = useState("");
  const [medicos, setMedicos] = useState([]);

  useEffect(() => {
    const fetchCentrosMedicos = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/MedicalCenter`);
        if (!response.ok) {
          throw new Error("Error al obtener los centros médicos");
        }
        const data = await response.json();
        setCentrosMedicos(data);
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchCentrosMedicos();
  }, []);

  const handleCentroMedicoChange = async (event) => {
    const selectedCentroMedico = event.target.value;
    setCentroMedicoSeleccionado(selectedCentroMedico);

    try {
      const response = await fetch(`${API_BASE_URL}/Medic`);
      if (!response.ok) {
        throw new Error("Error al obtener los médicos");
      }
      const data = await response.json();
      setMedicos(data);
    } catch (error) {
      console.error("Error:", error);
    }
  };

  useEffect(() => {
    if (centroMedicoSeleccionado && medicos.length > 0) {
      const medicosFiltrados = medicos.filter(
        (medico) =>
          medico.medicalCenter.id === parseInt(centroMedicoSeleccionado)
      );
      const especialidadesUnicas = Array.from(
        new Set(
          medicosFiltrados.flatMap((medico) =>
            medico.specialties.map((especialidad) => especialidad.name)
          )
        )
      );
      setEspecialidades(especialidadesUnicas);
    }
  }, [centroMedicoSeleccionado, medicos]);

  const handleSubmit = (event) => {
    event.preventDefault();

    if (!centroMedicoSeleccionado || !especialidadSeleccionada) {
      alert("Por favor, selecciona un centro médico y una especialidad.");
      return;
    }

    const medicosFiltrados = medicos.filter(
      (medico) =>
        medico.medicalCenter.id === parseInt(centroMedicoSeleccionado) &&
        medico.specialties.some(
          (especialidad) => especialidad.name === especialidadSeleccionada
        )
    );
    navigate("/SeleccionarTurno", { state: { medicos: medicosFiltrados } });
  };

  return (
    <>
      <div className="search-page">
        <div className="container">
          <Form className="search-box" onSubmit={handleSubmit}>
            <Form.Select
              size="lg"
              className="centroMedic"
              onChange={handleCentroMedicoChange}
            >
              <option>Seleccione un centro médico</option>
              {centrosMedicos.map((centro) => (
                <option key={centro.id} value={centro.id}>
                  {centro.name}
                </option>
              ))}
            </Form.Select>
            <br />
            <Form.Select
              size="lg"
              className="Especiality"
              onChange={(e) => setEspecialidadSeleccionada(e.target.value)}
            >
              <option>Seleccione una especialidad</option>
              {especialidades.map((especialidad, index) => (
                <option key={index} value={especialidad}>
                  {especialidad}
                </option>
              ))}
            </Form.Select>
            <br />
            <Button variant="primary" type="submit" className="button">
              Buscar
            </Button>
          </Form>
        </div>
      </div>
    </>
  );
}

export default TurnosPacientes;
