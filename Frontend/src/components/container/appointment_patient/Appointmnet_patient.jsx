import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import Image from "react-bootstrap/Image";
import NavBar from "../../common/navBar/navBar";
import "./Appointment_patient.css"; // Asegúrate de tener este archivo CSS

function AppointmentPatient() {
  return (
    <div className="page-container">
      <NavBar />

      <div className="content-container">
        <Card className="appointment-card">
          <div className="d-flex">
            <Image
              src="https://via.placeholder.com/100"
              rounded
              className="doctor-image"
            />
            <div className="info-section">
              <div className="d-flex justify-content-between align-items-start">
                <div className="doctor-info">
                  <p>Apellido</p>
                  <p>Nombre</p>
                  <p>Especialidad</p>
                  <p>Matrícula</p>
                </div>
                <div className="appointment-time">
                  <p>Lunes 01/1</p>
                  <Button variant="outline-dark">9:30</Button>
                </div>
              </div>
            </div>
          </div>
        </Card>
      </div>
    </div>
  );
}

export default AppointmentPatient;
