import { RouterProvider, createBrowserRouter } from "react-router-dom";
import HomePage from "./components/container/homePage/HomePage";
import Login from "./components/container/login/Login";
import RegisterPatient from "./components/container/registerPatients/RegisterPatient";
import TurnosPaciente from "./components/container/TurnosPacientes/TurnosPaciente";
import AppointmentSelection from "./components/container/appointmentSelection/AppointmentSelection";
import AppointmentPatient from "./components/container/appointment_patient/Appointmnet_patient";
import DashBoard from "./components/container/dashBoard/DashBoard";
import DashBoardAdminMC from "./components/container/dashBoardAdminMC/DashBoardAdminMC";
import AddDoctor from "./components/container/addDoctor/AddDoctor";
import Protected from "./routes/Protected";
import { AuthenticationContextProvider } from "./services/authentication/AuthenticationContext";
import NotFound from "./routes/NotFound";

function App() {
  const router = createBrowserRouter([
    { path: "/", element: <HomePage /> },
    { path: "/login", element: <Login /> },
    {
      path: "/",
      element: <Protected requiredRole="Patient" />,
      children: [
        {
          path: "/TurnosPacientes",
          element: <TurnosPaciente />,
        },
        {
          path: "/SeleccionarTurno",
          element: <AppointmentSelection />,
        },
        {
          path: "/AppointmentPatient",
          element: <AppointmentPatient />,
        },
      ],
    },
    {
      path: "/",
      element: <Protected requiredRole="AdminMC" />,
      children: [
        {
          path: "/PanelDeControlCM",
          element: <DashBoardAdminMC />,
        },
        {
          path: "/AgregarMedico",
          element: <AddDoctor />,
        },
      ],
    },
    {
      path: "/",
      element: <Protected requiredRole="SysAdmin" />,
      children: [
        {
          path: "/PanelDeControl",
          element: <DashBoard />,
        },
        {
          path: "/RegisterUsers",
          element: <RegisterPatient />,
        },
      ],
    },
    {
      path: "*",
      element: <NotFound />,
    },
  ]);

  return (
    <AuthenticationContextProvider>
      <div className="d-flex flex-column align-items-center">
        <RouterProvider router={router} />
      </div>
    </AuthenticationContextProvider>
  );
}

export default App;
