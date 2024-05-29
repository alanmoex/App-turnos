import { Button, Nav, NavDropdown, Navbar } from "react-bootstrap";

const NavBar = () => {
  return (
    <Navbar
      bg="primary"
      data-bs-theme="dark"
      expand="lg"
      className="bg-body-tertiary"
    >
      <div className="container-fluid">
        <Navbar.Brand href="#home">MediCare</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto align-items-center">
            <Nav.Link href="#home">
              <Button>REGISTRARSE</Button>
            </Nav.Link>
            <Nav.Link href="#link">
              <Button>INICIAR SESIÓN</Button>
            </Nav.Link>
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
