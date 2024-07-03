import { useContext } from "react";
import { Button, Nav, NavDropdown, NavItem, Navbar } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import logo from "../../../assets/logo.png";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";

const NavBar = () => {
  const navigate = useNavigate();
  const { userInfo } = useContext(AuthenticationContext);
  const { handleLogout } = useContext(AuthenticationContext);
  const role = userInfo?.role || "Guest";
  const name = userInfo?.given_name;
  const handleRegisterClick = () => {
    navigate("/registerPatient");
  };

  const handleLoginClick = () => {
    navigate("/login");
  };

  const handleLogoutClick = () => {
    navigate("/login");
    handleLogout();
  };

  return (
    <Navbar
      bg="primary"
      data-bs-theme="dark"
      expand="lg"
      className="bg-body-tertiary"
    >
      <div className="container-fluid">
        <Navbar.Brand href="/">
          <img
            src={logo}
            alt="Logo"
            width="30"
            height="30"
            className="d-inline-block align-top me-2"
          />
          MediCare
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto align-items-center">
            {role === "Guest" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={handleRegisterClick}>REGISTRARSE</Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={handleLoginClick}>INICIAR SESIÓN</Button>
                </NavItem>
              </>
            )}
            {role === "Patient" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/AppointmentPatient")}>
                    Mis Citas
                  </Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/TurnosPacientes")}>
                    Reservar Turno
                  </Button>
                </NavItem>
              </>
            )}
            {role === "AdminMC" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/PanelDeControlCM")}>
                    Panel de Administración
                  </Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/AgregarMedico")}>
                    Agregar Medico
                  </Button>
                </NavItem>
              </>
            )}
            {role === "SysAdmin" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/PanelDeControl")}>
                    Panel de Administración
                  </Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/RegisterUsers")}>
                    Crear Usuarios
                  </Button>
                </NavItem>
              </>
            )}
            {role !== "guest" && (
              <NavDropdown
                title={name}
                id="basic-nav-dropdown"
                align={{ lg: "end" }}
              >
                <NavDropdown.Item onClick={handleLogoutClick}>
                  Cerrar Sesión
                </NavDropdown.Item>
              </NavDropdown>
            )}
          </Nav>
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
};

export default NavBar;
