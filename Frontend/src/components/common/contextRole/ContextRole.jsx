// RoleContext.js
import { createContext, useState } from "react";
import PropTypes from "prop-types";

// Crear el contexto
const RoleContext = createContext();

// Crear un proveedor de contexto
const RoleProvider = ({ children }) => {
  const [role, setRole] = useState("guest"); // Estado inicial del rol

  return (
    <RoleContext.Provider value={{ role, setRole }}>
      {children}
    </RoleContext.Provider>
  );
};

// Definir las PropTypes para RoleProvider
RoleProvider.propTypes = {
  children: PropTypes.node.isRequired,
};

export { RoleContext, RoleProvider };
