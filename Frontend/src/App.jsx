import { RouterProvider, createBrowserRouter } from "react-router-dom";
import HomePage from "./components/container/homePage/HomePage";
import Login from "./components/container/login/Login";
import RegisterPatient from "./components/container/registerPatients/RegisterPatient";
import TurnosPaciente from "./components/container/TurnosPacientes/TurnosPaciente";
import ShiftManagement from "./components/container/shiftManagement/ShiftManagement";
import PatientManagement from "./components/container/patientManagement/PatientManagement";
import ManagementMedicalCenters from "./components/container/ManagementMedicalCenters/ManagementMedicalCenters";
import AppointmentSelection from "./components/container/appointmentSelection/AppointmentSelection";
import { RoleProvider } from "./components/common/contextRole/ContextRole";
import AppointmentPatient from "./components/container/appointment_patient/Appointmnet_patient";

function App() {
  /*   let patientsList = [
    {
      name: "Pedro",
      lastName: "Martinez",
      dni: "24546375",
      mail: "PedroMartinez@gmail.com",
      password: "messi123",
      role: "patient",
    },
  ]; */
  /* let MedicList = [
    {
      Id: 1,
      Name: "John",
      LastName: "Doe",
      LicenseNumber: "12345",
      Specialties: [
        {
          Id: 1,
          Name: "Cardiology",
        },
        {
          Id: 2,
          Name: "Pediatrics",
        },
      ],
      Appointments: [
        {
          Id: 1,
          Date: "2023-06-01",
          PatientName: "Patient A",
        },
        {
          Id: 2,
          Date: "2023-06-03",
          PatientName: "Patient B",
        },
      ],
      WorkSchedules: [
        {
          Day: "Monday",
          StartTime: 9,
          EndTime: 17,
        },
        {
          Day: "Wednesday",
          StartTime: 9,
          EndTime: 17,
        },
      ],
    },
    {
      Id: 2,
      Name: "Jane",
      LastName: "Smith",
      LicenseNumber: "67890",
      Specialties: [
        {
          Id: 3,
          Name: "Dermatology",
        },
      ],
      Appointments: [
        {
          Id: 3,
          Date: "2023-06-02",
          PatientName: "Patient C",
        },
      ],
      WorkSchedules: [
        {
          Day: "Tuesday",
          StartTime: 10,
          EndTime: 18,
        },
        {
          Day: "Thursday",
          StartTime: 10,
          EndTime: 18,
        },
      ],
    },
    {
      Id: 3,
      Name: "Alice",
      LastName: "Johnson",
      LicenseNumber: "54321",
      Specialties: [
        {
          Id: 4,
          Name: "Neurology",
        },
        {
          Id: 5,
          Name: "Orthopedics",
        },
      ],
      Appointments: [
        {
          Id: 4,
          Date: "2023-06-05",
          PatientName: "Patient D",
        },
        {
          Id: 5,
          Date: "2023-06-07",
          PatientName: "Patient E",
        },
      ],
      WorkSchedules: [
        {
          Day: "Monday",
          StartTime: 8,
          EndTime: 16,
        },
        {
          Day: "Friday",
          StartTime: 8,
          EndTime: 16,
        },
      ],
    },
  ]; */

  const router = createBrowserRouter([
    { path: "/", element: <HomePage /> },
    { path: "/login", element: <Login /> },
    { path: "/registerPatient", element: <RegisterPatient /> },
    { path: "/TurnosPacientes", element: <TurnosPaciente /> },
    { path: "/GestionTurnos", element: <ShiftManagement /> },
    { path: "/GestionPacientes", element: <PatientManagement /> },
    { path: "/GestionCentroMedico", element: <ManagementMedicalCenters /> },
    { path: "/SeleccionarTurno", element: <AppointmentSelection /> },
    { path: "/AppointmentPatient", element: <AppointmentPatient /> },
  ]);

  return (
    <RoleProvider>
      <div className="d-flex flex-column align-items-center">
        {<RouterProvider router={router} />}
      </div>
    </RoleProvider>
  );
}

export default App;
