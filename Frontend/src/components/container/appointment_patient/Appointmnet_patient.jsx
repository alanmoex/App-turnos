import NavBar from "../../common/navBar/navBar";
import "./Appointment_patient.css";
import { Card, Container, Row, Button, Modal, Form } from "react-bootstrap";
import { useState, useEffect, useContext } from "react";
import { API_BASE_URL } from "../../../api";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";

function AppointmentPatient() {
  const [turnos, setTurnos] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [selectedTurno, setSelectedTurno] = useState(null);
  const [selectedDate, setSelectedDate] = useState("");
  const [selectedTime, setSelectedTime] = useState("");
  const { userInfo } = useContext(AuthenticationContext);

  useEffect(() => {
    const fetchAppointments = async () => {
      if (userInfo && userInfo.sub) {
        try {
          const response = await fetch(
            `${API_BASE_URL}/Appointments/GetAppointmentsByPatient?patientId=${userInfo.sub}`,
            {
              headers: {
                accept: "application/json",
              },
            }
          );
          if (!response.ok) {
            throw new Error("Error al obtener las citas");
          }
          const data = await response.json();
          setTurnos(data);
        } catch (error) {
          console.error("Error:", error);
        }
      }
    };

    fetchAppointments();
  }, [userInfo, showModal]);

  const formatDateTime = (dateTime) => {
    const date = new Date(dateTime);
    const options = { year: "numeric", month: "long", day: "numeric" };
    const formattedDate = date.toLocaleDateString(undefined, options);
    const formattedTime = date.toLocaleTimeString(undefined, {
      hour: "2-digit",
      minute: "2-digit",
    });
    return `${formattedDate} a las ${formattedTime}`;
  };

  const handleShowModal = (turno) => {
    setSelectedTurno(turno);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedDate("");
    setSelectedTime("");
  };

  const handleDateChange = (event) => {
    setSelectedDate(event.target.value);
  };

  const handleTimeChange = (event) => {
    setSelectedTime(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!selectedDate || !selectedTime) {
      alert("Por favor selecciona un día y horario válido.");
      return;
    }

    const appointmentDateTime = new Date(selectedDate);
    appointmentDateTime.setHours(
      selectedTime.slice(0, 2),
      selectedTime.slice(3),
      0,
      0
    );

    const appointmentData = {
      appointmentDateTime: appointmentDateTime.toISOString(),
    };

    try {
      const response = await fetch(
        `${API_BASE_URL}/Appointments/${selectedTurno.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(appointmentData),
        }
      );

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Error al modificar el turno");
      }

      alert("Turno modificado exitosamente");
      handleCloseModal();
    } catch (error) {
      console.error("Error:", error);
      alert("Error al modificar el turno: " + error.message);
    }
  };

  const turnosMapped = turnos.map((turno) => (
    <Card key={turno.id} className="mb-3">
      <Card.Header>
        <strong>{formatDateTime(turno.appointmentDateTime)}</strong>
      </Card.Header>
      <Card.Body>
        <Card.Title>
          {turno.medic.name} {turno.medic.lastName}
        </Card.Title>
        <Card.Subtitle className="mb-2 text-muted">
          {turno.medic.specialties
            .map((specialty) => specialty.name)
            .join(", ")}
        </Card.Subtitle>
        <Card.Text>
          <strong>Centro Médico:</strong> {turno.medic.medicalCenter.name}
          <br />
          <strong>Paciente:</strong> {turno.patient.name}{" "}
          {turno.patient.lastName}
        </Card.Text>
        <Button variant="primary" onClick={() => handleShowModal(turno)}>
          Modificar Turno
        </Button>
      </Card.Body>
      <Card.Footer>
        {turno.isCancelled ? (
          <span className="text-danger">Cancelado</span>
        ) : (
          <span className="text-success">Confirmado</span>
        )}
      </Card.Footer>
    </Card>
  ));

  return (
    <>
      <Row className="container-fluid p-0 mb-2">
        <NavBar></NavBar>
      </Row>
      <Container>
        {turnos.length > 0 ? turnosMapped : <p>No hay turnos disponibles</p>}
      </Container>

      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>Modificar Turno</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={handleSubmit}>
            <Form.Group controlId="formDate">
              <Form.Label>Selecciona un día:</Form.Label>
              <Form.Control
                type="date"
                value={selectedDate}
                onChange={handleDateChange}
                min={new Date().toISOString().slice(0, 10)}
                required
              />
            </Form.Group>
            <Form.Group controlId="formTime">
              <Form.Label>Selecciona un horario:</Form.Label>
              <Form.Control
                as="select"
                value={selectedTime}
                onChange={handleTimeChange}
                required
              >
                <option value="">Selecciona...</option>
                {Array.from({ length: 10 }, (_, i) => 9 + i).map((hour) => (
                  <option
                    key={hour}
                    value={`${hour}:00`}
                  >{`${hour}:00`}</option>
                ))}
              </Form.Control>
            </Form.Group>
            <Button variant="primary" type="submit">
              Guardar Cambios
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </>
  );
}

export default AppointmentPatient;
