import { RouterProvider, createBrowserRouter } from "react-router-dom";
import HomePage from "./components/container/homePage/HomePage";
import Login from "./components/container/login/Login";
import RegisterPatient from "./components/container/registerPatients/RegisterPatient";
import TurnosPaciente from "./components/container/TurnosPacientes/TurnosPaciente";

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

  const router = createBrowserRouter([
    { path: "/", element: <HomePage /> },
    { path: "/login", element: <Login /> },
    { path: "/registerPatient", element: <RegisterPatient /> },
    { path: "/TurnosPacientes", element: <TurnosPaciente /> },
  ]);

  return (
    <div className="d-flex flex-column align-items-center">
      {<RouterProvider router={router} />}
    </div>
  );
}

export default App;
