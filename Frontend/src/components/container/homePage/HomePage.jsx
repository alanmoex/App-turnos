import NavBar from "../../common/navBar/NavBar";
import { Col, Row } from "react-bootstrap";
import "./HomePage.css";

const HomePage = () => {
  return (
    <div className="container-fluid background-image">
      <Row>
        <NavBar></NavBar>
      </Row>
      <Row className="h-100">
        <Col className="d-flex align-items-center justify-content-end">
          <h1>MediCare</h1>
        </Col>
      </Row>
    </div>
  );
};

export default HomePage;
