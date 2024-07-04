import MedicsList from "../../common/medicList/MedicsList";
import NavBar from "../../common/navBar/navBar";
import { Row } from "react-bootstrap";
import { useLocation } from "react-router-dom";

const AppointmentSelection = () => {
  const location = useLocation();
  const medics = location && location.state ? location.state.medicos : [];

  return (
    <>
      <Row className="container-fluid p-0 mb-2">
        <NavBar></NavBar>
      </Row>

      <MedicsList medics={medics}></MedicsList>
    </>
  );
};

export default AppointmentSelection;
