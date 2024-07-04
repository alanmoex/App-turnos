import Navbar from "../../common/navBar/navBar";
import { Col, Row } from "react-bootstrap";
import "./HomePage.css";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";
import { useContext } from "react";

const HomePage = () => {
  const { userInfo } = useContext(AuthenticationContext);
  console.log(userInfo);
  return (
    <div className="container-fluid background-image">
      <Row>
        <Navbar></Navbar>
      </Row>
      <Row className="h-50">
        <Col className="d-flex align-items-end justify-content-end pe-5 pb-3 me-5">
          <h1>MediCare</h1>
        </Col>
      </Row>
      <Row className="h-50">
        <Col className="d-flex align-items-start justify-content-end pe-5 me-5">
          <h2>Expertos en el cuidado de tu salud</h2>
        </Col>
      </Row>
    </div>
  );
};

export default HomePage;
