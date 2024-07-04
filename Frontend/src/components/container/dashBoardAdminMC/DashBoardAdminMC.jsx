import { useEffect, useState, useContext } from "react";
import { Container } from "react-bootstrap";
import DashBoardTable from "../../common/dashBoardTable/DashBoardTable";
import useApi from "../../../custom/useApi";
import { API_BASE_URL } from "../../../api";
import { AuthenticationContext } from "../../../services/authentication/AuthenticationContext";

const DashBoardAdminMC = () => {
  const [medicalCenterId, setMedicalCenterId] = useState(null);
  const { userInfo } = useContext(AuthenticationContext);
  const { data, loading, error, setEntity, updateEntity, deleteEntity } =
    useApi("Medic");

  useEffect(() => {
    const fetchMedicalCenterId = async () => {
      try {
        const response = await fetch(
          `${API_BASE_URL}/AdminMC/GetById/${userInfo.sub}`
        );
        if (!response.ok) throw new Error("Error fetching medical center data");
        const data = await response.json();
        setMedicalCenterId(data.medicalCenter.id);
      } catch (error) {
        console.error("Error fetching medical center data:", error);
      }
    };

    if (userInfo && userInfo.sub) {
      fetchMedicalCenterId();
    }
  }, [userInfo]);

  useEffect(() => {
    setEntity("Medic");
  }, [setEntity]);

  const filteredData = medicalCenterId
    ? data.filter((medic) => medic.medicalCenter.id === medicalCenterId)
    : [];

  const mapData = (data) => {
    return data.map((medic) => ({
      id: medic.id,
      name: medic.name,
      lastName: medic.lastName,
      licenseNumber: medic.licenseNumber,
      specialties: medic.specialties
        ? medic.specialties.map((specialty) => specialty.name).join(", ")
        : "",
      medicalCenter: medic.medicalCenter ? medic.medicalCenter.name : "",
    }));
  };

  const handleSave = async (index, updatedEntity) => {
    const entityId = updatedEntity.id;
    await updateEntity(entityId, updatedEntity);
  };

  const handleDelete = async (id) => {
    await deleteEntity(id);
  };

  const columns = [
    { Header: "Nombre", accessor: "name", editable: true },
    { Header: "Apellido", accessor: "lastName", editable: true },
    { Header: "Nº de Licencia", accessor: "licenseNumber", editable: true },
    { Header: "Especialidad", accessor: "specialties", editable: false },
    { Header: "Centro Médico", accessor: "medicalCenter", editable: false },
  ];

  return (
    <Container className="mt-4">
      {loading ? (
        <div>Cargando...</div>
      ) : error ? (
        <div>El servicio está caído, intente más tarde</div>
      ) : (
        <DashBoardTable
          columns={columns}
          data={mapData(filteredData)}
          onSave={handleSave}
          onDelete={handleDelete}
        />
      )}
    </Container>
  );
};

export default DashBoardAdminMC;
