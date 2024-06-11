import App from "../../../App";
import PropTypes from "prop-types";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";

const MedicItem = ({
  id,
  name,
  lastName,
  licenseNumber,
  specialties,
  workSchedules,
  appointments,
}) => {
  const specialtiesMapped = specialties.map((s) => <h3>{s.Name}</h3>);

  const workSchedulesMapped = workSchedules.map((w) => <h3>{w.Day}</h3>);

  return (
    <>
      <Card style={{ width: "18rem" }}>
        <Card.Img variant="left" src="" />
        <Card.Body>
          <Card.Text>
            <h2>{name}</h2>
            <h2>{lastName}</h2>
            <h3>{licenseNumber}</h3>
            {specialtiesMapped}
            <div className="d-flex">{workSchedulesMapped}</div>
          </Card.Text>
        </Card.Body>
      </Card>
    </>
  );
};

export default MedicItem;

MedicItem.PropTypes = {
  id: PropTypes.number,
  name: PropTypes.string,
  lastName: PropTypes.string,
  licenseNumber: PropTypes.string,
  specialties: PropTypes.array,
  workSchedules: PropTypes.array,
  appointments: PropTypes.array,
};
