import { useEffect, useState } from "react";
import { Form, Container } from "react-bootstrap";
import DashBoardTable from "../../common/dashBoardTable/DashBoardTable";
import useApi from "../../../custom/useApi";

const DashBoard = () => {
  const [selectedEntity, setSelectedEntity] = useState("AdminMC");
  const { data, loading, error, setEntity, updateEntity, deleteEntity } =
    useApi("AdminMC");

  const mapData = (entity, data) => {
    switch (entity) {
      case "AdminMC":
        return data.map((adminMC) => ({
          ...adminMC,
          medicalCenter: adminMC.medicalCenter
            ? adminMC.medicalCenter.name
            : "",
        }));
      case "Appointments":
        return data.map((appointment) => ({
          ...appointment,
          date: new Date(appointment.appointmentDateTime).toLocaleDateString(),
          time: new Date(appointment.appointmentDateTime).toLocaleTimeString(),
          patientName: appointment.patient
            ? `${appointment.patient.name} ${appointment.patient.lastName}`
            : "No Patient",
          medicName: appointment.medic
            ? `${appointment.medic.name} ${appointment.medic.lastName}`
            : "No Medic",
          specialties:
            appointment.medic && appointment.medic.specialties
              ? appointment.medic.specialties
                  .map((specialty) => specialty.name)
                  .join(", ")
              : "",
          isCancelled: appointment.isCancelled ? "Cancelado" : "Activo",
        }));
      case "Medic":
        return data.map((medic) => ({
          name: medic.name,
          lastName: medic.lastName,
          licenseNumber: medic.licenseNumber,
          specialties: medic.specialties
            ? medic.specialties.map((specialty) => specialty.name).join(", ")
            : "",
          medicalCenter: medic.medicalCenter ? medic.medicalCenter.name : "",
        }));
      case "MedicalCenter":
        return data.map((center) => ({
          id: center.id,
          name: center.name,
        }));
      case "Patient":
        return data.map((patient) => ({
          id: patient.id,
          name: patient.name,
          lastName: patient.lastName,
          email: patient.email,
          password: patient.password,
        }));
      case "Specialty":
        return data.map((specialty) => ({
          id: specialty.id,
          name: specialty.name,
        }));
      case "SysAdmin":
        return data.map((admin) => ({
          id: admin.id,
          name: admin.name,
          email: admin.email,
          password: admin.password,
        }));
      case "WorkSchedule":
        return data.map((schedule) => ({
          id: schedule.id,
          day: schedule.day,
          startTime: schedule.startTime,
          endTime: schedule.endTime,
        }));
      default:
        return data;
    }
  };

  const handleSave = async (index, updatedEntity) => {
    const entityId = updatedEntity.id;
    const updatedEntityData =
      selectedEntity === "Appointments"
        ? {
            ...updatedEntity,
            appointmentDateTime: new Date(
              `${updatedEntity.date}T${updatedEntity.time}`
            ).toISOString(),
          }
        : updatedEntity;
    await updateEntity(entityId, updatedEntityData);
  };

  const handleDelete = async (id) => {
    await deleteEntity(id);
  };

  useEffect(() => {
    setEntity(selectedEntity);
  }, [selectedEntity, setEntity]);

  const columns = {
    AdminMC: [
      { Header: "Name", accessor: "name", editable: true },
      { Header: "Email", accessor: "email", editable: true },
      { Header: "Contraseña", accessor: "password", editable: true },
      { Header: "Medical Center", accessor: "medicalCenter", editable: false },
    ],
    Appointments: [
      { Header: "Fecha", accessor: "date", editable: true },
      { Header: "Hora", accessor: "time", editable: true },
      { Header: "Paciente", accessor: "patientName", editable: false },
      { Header: "Médico", accessor: "medicName", editable: false },
      { Header: "Especialidad", accessor: "specialties", editable: false },
      { Header: "Estado", accessor: "isCancelled", editable: false },
    ],
    Medic: [
      { Header: "Nombre", accessor: "name", editable: true },
      { Header: "Apellido", accessor: "lastName", editable: true },
      { Header: "Nº de Licencia", accessor: "licenseNumber", editable: true },
      { Header: "Especialidad", accessor: "specialties", editable: false },
      { Header: "Centro Médico", accessor: "medicalCenter", editable: false },
    ],
    MedicalCenter: [{ Header: "Name", accessor: "name", editable: true }],
    Patient: [
      { Header: "Name", accessor: "name", editable: true },
      { Header: "Last Name", accessor: "lastName", editable: true },
      { Header: "Email", accessor: "email", editable: true },
      { Header: "Password", accessor: "password", editable: true },
    ],
    Specialty: [{ Header: "Name", accessor: "name", editable: true }],
    SysAdmin: [
      { Header: "Name", accessor: "name", editable: true },
      { Header: "Email", accessor: "email", editable: true },
      { Header: "Password", accessor: "password", editable: true },
    ],
    WorkSchedule: [
      { Header: "Day", accessor: "day", editable: true },
      { Header: "Start Time", accessor: "startTime", editable: true },
      { Header: "End Time", accessor: "endTime", editable: true },
    ],
  };

  return (
    <Container className="mt-4">
      <Form>
        <Form.Group controlId="entitySelect">
          <Form.Control
            as="select"
            value={selectedEntity}
            onChange={(e) => setSelectedEntity(e.target.value)}
          >
            <option value="AdminMC">Admins de Centros Médicos</option>
            <option value="Appointments">Turnos</option>
            <option value="Medic">Médicos</option>
            <option value="MedicalCenter">Centros Médicos</option>
            <option value="Patient">Pacientes</option>
            <option value="Specialty">Especialidades</option>
            <option value="SysAdmin">Administradores del Sistema</option>
            <option value="WorkSchedule">Horarios de Trabajo</option>
          </Form.Control>
        </Form.Group>
      </Form>
      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div>Error: {error}</div>
      ) : (
        <DashBoardTable
          columns={columns[selectedEntity]}
          data={mapData(selectedEntity, data)}
          onSave={handleSave}
          onDelete={handleDelete}
        />
      )}
    </Container>
  );
};

export default DashBoard;
