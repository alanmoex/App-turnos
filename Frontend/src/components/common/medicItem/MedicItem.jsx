import PropTypes from "prop-types";
import Card from "react-bootstrap/Card";
import MedicPic from "../../../assets/silueta-doctor.jpg";
import { Col, Row } from "react-bootstrap";

const MedicItem = ({ name, lastName, licenseNumber, specialties }) => {
  console.log(specialties);
  const specialtiesMapped = specialties.map((s) => (
    <h3 key={s.id}>{s.name}</h3>
  ));

  return (
    <>
      <Card style={{ width: "72rem" }} className="m-5">
        <Row className="align-items-center">
          <Col xs={4} className="d-flex justify-content-center">
            <div className="p-2 bg-dark">
              <Card.Img variant="top" src={MedicPic} />
            </div>
          </Col>
          <Col xs={8}>
            <Card.Body>
              <h2>{name}</h2>
              <h2>{lastName}</h2>
              <h3>Nº de licencia: {licenseNumber}</h3>
              <h2>Especialidad/es:</h2>
              {specialtiesMapped}
            </Card.Body>
          </Col>
        </Row>
      </Card>
    </>
  );
};

MedicItem.propTypes = {
  name: PropTypes.string,
  lastName: PropTypes.string,
  licenseNumber: PropTypes.string,
  specialties: PropTypes.array,
};

export default MedicItem;
