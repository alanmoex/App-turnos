import PropTypes from "prop-types";
import MedicItem from "../medicItem/MedicItem";

const MedicsList = ({ medics }) => {
  const medicsMapped = medics.map((m) => (
    <MedicItem
      key={m.id}
      id={m.id}
      name={m.name}
      lastName={m.lastName}
      licenseNumber={m.licenseNumber}
      specialties={m.specialties}
      workSchedules={m.workSchedules}
      appointments={m.appointments}
      medicalCenterId={m.medicalCenter.id}
    ></MedicItem>
  ));

  return (
    <>
      <div className="border border-dark p-2">
        {medicsMapped.length > 0 ? (
          medicsMapped
        ) : (
          <p>No se encontraron medicos.</p>
        )}
      </div>
    </>
  );
};

MedicsList.propTypes = {
  medics: PropTypes.array,
};

export default MedicsList;
