import { Button, Nav, NavDropdown, NavItem, Navbar } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import logo from "../../../assets/logo.png";

const NavBar = () => {
  const navigate = useNavigate();

  const handleRegisterClick = () => {
    navigate("/registerPatient");
  };

  const handleLoginClick = () => {
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
            <NavItem className="me-2">
              <Button onClick={handleRegisterClick}>REGISTRARSE</Button>
            </NavItem>
            <NavItem className="me-2">
              <Button onClick={handleLoginClick}>INICIAR SESIÓN</Button>
            </NavItem>
            <NavDropdown title="USUARIO" id="basic-nav-dropdown">
              <NavDropdown.Item href="#action/3.1">
                Cerrar Sesión
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
};

export default NavBar;
