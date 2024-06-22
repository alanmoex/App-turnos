import { useEffect, useState } from "react";
import MedicsList from "../../common/medicList/MedicsList";
import { API_BASE_URL } from "../../../api";
import NavBar from "../../common/navBar/navBar";
import { Row } from "react-bootstrap";

const AppointmentSelection = () => {
  const [medics, setMedics] = useState([]);

  useEffect(() => {
    fetch(`${API_BASE_URL}/Medic`, {
      headers: {
        accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setMedics(data))
      .catch((error) => console.log(error));
  }, []);
  return (
    <>
      <Row className="container-fluid p-0 mb-2">
        <NavBar></NavBar>
      </Row>

      <MedicsList medics={medics}></MedicsList>
    </>
  );
};

AppointmentSelection.propTypes = {};

export default AppointmentSelection;
