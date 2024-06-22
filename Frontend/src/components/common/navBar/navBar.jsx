import { useContext } from "react";
import { Button, Nav, NavDropdown, NavItem, Navbar } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import logo from "../../../assets/logo.png";
import { RoleContext } from "../contextRole/ContextRole";

const NavBar = () => {
  const navigate = useNavigate();
  const { role, setRole } = useContext(RoleContext);

  const handleRegisterClick = () => {
    navigate("/registerPatient");
  };

  const handleLoginClick = () => {
    navigate("/login");
  };

  const handleLogoutClick = () => {
    setRole("guest");
    navigate("/login");
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
            {role === "guest" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={handleRegisterClick}>REGISTRARSE</Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={handleLoginClick}>INICIAR SESIÓN</Button>
                </NavItem>
              </>
            )}
            {role === "patient" && (
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
            {role === "centralmedic" && (
              <>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/admin")}>
                    Panel de Administración
                  </Button>
                </NavItem>
                <NavItem className="me-2">
                  <Button onClick={() => navigate("/manage-users")}>
                    Gestionar Usuarios
                  </Button>
                </NavItem>
              </>
            )}
            {role !== "guest" && (
              <NavDropdown title="USUARIO" id="basic-nav-dropdown">
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
