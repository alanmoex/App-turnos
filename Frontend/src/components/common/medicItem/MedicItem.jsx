import PropTypes from "prop-types";
import Card from "react-bootstrap/Card";
import MedicPic from "../../../assets/silueta-doctor.jpg";
import { Button, Col, Form, Row } from "react-bootstrap";
import { API_BASE_URL } from "../../../api";
import { useContext, useState } from "react";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";

const MedicItem = ({
  id,
  name,
  lastName,
  licenseNumber,
  specialties,
  medicalCenterId,
}) => {
  const { userInfo } = useContext(AuthenticationContext);
  const [selectedDate, setSelectedDate] = useState("");
  const [selectedTime, setSelectedTime] = useState("");

  const handleDateChange = (event) => {
    setSelectedDate(event.target.value);
  };

  const handleTimeChange = (event) => {
    setSelectedTime(event.target.value);
  };

  const handleCreateAppointment = async (event) => {
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
      medicId: id,
      patientId: parseInt(userInfo.sub),
      medicalCenterId: medicalCenterId,
    };

    try {
      const response = await fetch(`${API_BASE_URL}/Appointments`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(appointmentData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Error al crear el turno");
      }

      alert("Turno creado exitosamente");
    } catch (error) {
      console.error("Error:", error);
      alert("Error al crear el turno: " + error.message);
    }
  };

  const specialtiesMapped = specialties.map((s) => (
    <h3 key={s.id}>{s.name}</h3>
  ));

  return (
    <>
      <Card style={{ width: "72rem" }} className="m-5">
        <Row className="align-items-center">
          <Col xs={4} className="d-flex justify-content-center">
            <div className="p-2 bg-dark">
              <Card.Img variant="top" src={MedicPic} />
            </div>
          </Col>
          <Col xs={4}>
            <Card.Body>
              <h2>{name}</h2>
              <h2>{lastName}</h2>
              <h3>Nº de licencia: {licenseNumber}</h3>
              <h2>Especialidad/es:</h2>
              {specialtiesMapped}
            </Card.Body>
          </Col>
          <Col xs={4}>
            <Card.Body>
              <Form onSubmit={handleCreateAppointment}>
                <Form.Group controlId="formDate">
                  <Form.Label>Selecciona un día:</Form.Label>
                  <Form.Control as="select" onChange={handleDateChange}>
                    <option value="">Selecciona...</option>
                    {[...Array(5)].map((_, index) => {
                      const date = new Date();
                      date.setDate(date.getDate() + 1 + index);
                      const dateString = date.toISOString().slice(0, 10);
                      return (
                        <option key={dateString} value={dateString}>
                          {date.toLocaleDateString()}
                        </option>
                      );
                    })}
                  </Form.Control>
                </Form.Group>
                <Form.Group controlId="formTime">
                  <Form.Label>Selecciona un horario:</Form.Label>
                  <Form.Control as="select" onChange={handleTimeChange}>
                    <option value="">Selecciona...</option>
                    {Array.from({ length: 10 }, (_, i) => 9 + i).map((hour) => (
                      <option
                        key={hour}
                        value={`${hour}:00`}
                      >{`${hour}:00`}</option>
                    ))}
                  </Form.Control>
                </Form.Group>
                <Button type="submit">Crear Turno</Button>
              </Form>
            </Card.Body>
          </Col>
        </Row>
      </Card>
    </>
  );
};

MedicItem.propTypes = {
  id: PropTypes.number.isRequired,
  name: PropTypes.string.isRequired,
  lastName: PropTypes.string.isRequired,
  licenseNumber: PropTypes.string.isRequired,
  specialties: PropTypes.array.isRequired,
  medicalCenterId: PropTypes.number,
};

export default MedicItem;
