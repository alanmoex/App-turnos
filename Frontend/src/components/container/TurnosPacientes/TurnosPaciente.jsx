import Form from "react-bootstrap/Form";
import { Button } from "react-bootstrap";
import { useState } from "react";
import "./TurnosPaciente.css";

function TurnosPacientes() {
  const [centroMedico, setCentroMedico] = useState("");
  const [especialidades, setEspecialidades] = useState("");

  const centroMedicHandler = (event) => {
    setCentroMedico(event.target.value);
  };

  const especialidadesHandler = (event) => {
    setEspecialidades(event.target.value);
  };
  return (
    <>
      <div className="search-page">
        <div className="container">
          <Form className="search-box">
            <Form.Select size="lg" className="centroMedic">
              <option onChange={centroMedicHandler}>Centros médicos</option>
              {centroMedico}
            </Form.Select>
            <br />
            <Form.Select size="lg" className="Especiality">
              <option onChange={especialidadesHandler}>Especialidad</option>
              {especialidades}
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
