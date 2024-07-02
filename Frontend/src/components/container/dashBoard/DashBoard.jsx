import { useEffect, useState } from "react";
import { Form, Container } from "react-bootstrap";
import { API_BASE_URL } from "../../../api";
import DashBoardTable from "../../common/dashBoardTable/DashBoardTable";

const DashBoard = () => {
  const [selectedEntity, setSelectedEntity] = useState("AdminMC");
  const [data, setData] = useState([]);

  const mapData = (entity, data) => {
    switch (entity) {
      case "AdminMC":
        return data.map((item) => ({
          ...item,
          medicalCenter: item.medicalCenter ? item.medicalCenter.name : "",
        }));
      case "Appointments":
        return data.map((item) => ({
          ...item,
          date: new Date(item.appointmentDateTime).toLocaleDateString(),
          time: new Date(item.appointmentDateTime).toLocaleTimeString(),
          patientName: `${item.patient.name} ${item.patient.lastName}`,
          medicName: `${item.medic.name} ${item.medic.lastName}`,
          specialties: item.medic.specialties
            .map((specialty) => specialty.name)
            .join(", "),
          isCancelled: item.isCancelled ? "Cancelled" : "Active",
        }));
      case "Medic":
        return data.map((medico) => ({
          nombre: medico.name,
          apellido: medico.lastName,
          numeroLicencia: medico.licenseNumber,
          especialidades: medico.specialties
            .map((specialty) => specialty.name)
            .join(", "),
          centroMedico: medico.medicalCenter ? medico.medicalCenter.name : "",
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

  useEffect(() => {
    fetch(`${API_BASE_URL}/${selectedEntity}`, {
      headers: {
        accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const mappedData = mapData(selectedEntity, data);
        setData(mappedData);
      })
      .catch((error) => console.log(error));
  }, [selectedEntity]);

  const removeEntity = async (id) => {
    try {
      const response = await fetch(`${API_BASE_URL}/${selectedEntity}/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (response.ok) {
        const updatedData = data.filter((item) => item.id !== id);
        setData(updatedData);
      } else {
        alert(`Error al eliminar ${selectedEntity}`);
      }
    } catch (error) {
      console.error("Error al eliminar", error);
      alert("Error de conexión");
    }
  };

  const saveEntity = async (index, updatedEntity) => {
    const url = `${API_BASE_URL}/${selectedEntity}/${updatedEntity.id}`;
    const body =
      selectedEntity === "Appointments"
        ? JSON.stringify({
            appointmentDateTime: new Date(
              `${updatedEntity.date}T${updatedEntity.time}`
            ).toISOString(),
          })
        : JSON.stringify(updatedEntity);

    try {
      const response = await fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body,
      });
      if (response.ok) {
        const updatedData = [...data];
        updatedData[index] = updatedEntity;
        setData(updatedData);
      } else {
        alert(`Error al actualizar el ${selectedEntity}`);
      }
    } catch (error) {
      console.error("Error al actualizar", error);
      alert("Error de conexión");
    }
  };

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
      { Header: "Nombre", accessor: "nombre", editable: true },
      { Header: "Apellido", accessor: "apellido", editable: true },
      { Header: "Nº de Licencia", accessor: "numeroLicencia", editable: true },
      { Header: "Especialidad", accessor: "especialidades", editable: false },
      { Header: "Centro Médico", accessor: "centroMedico", editable: false },
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
            {/* Agrega aquí otras opciones de entidad según sea necesario */}
          </Form.Control>
        </Form.Group>
      </Form>
      <DashBoardTable
        columns={columns[selectedEntity]}
        data={data}
        onSave={saveEntity}
        onDelete={removeEntity}
      />
    </Container>
  );
};

export default DashBoard;
