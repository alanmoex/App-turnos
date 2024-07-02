import { RouterProvider, createBrowserRouter } from "react-router-dom";
import HomePage from "./components/container/homePage/HomePage";
import Login from "./components/container/login/Login";
import RegisterPatient from "./components/container/registerPatients/RegisterPatient";
import TurnosPaciente from "./components/container/TurnosPacientes/TurnosPaciente";
import ShiftManagement from "./components/container/shiftManagement/ShiftManagement";
import PatientManagement from "./components/container/patientManagement/PatientManagement";
import ManagementMedicalCenters from "./components/container/ManagementMedicalCenters/ManagementMedicalCenters";
import AppointmentSelection from "./components/container/appointmentSelection/AppointmentSelection";
import AppointmentPatient from "./components/container/appointment_patient/Appointmnet_patient";
import { AuthenticationContextProvider } from "./services/authentication/AuthenticationContext";
import DashBoard from "./components/container/dashBoard/DashBoard";

function App() {
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
    { path: "/DashBoard", element: <DashBoard /> },
  ]);

  return (
    <AuthenticationContextProvider>
      <div className="d-flex flex-column align-items-center">
        {<RouterProvider router={router} />}
      </div>
    </AuthenticationContextProvider>
  );
}

export default App;
