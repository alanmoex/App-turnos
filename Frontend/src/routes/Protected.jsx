import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { AuthenticationContext } from "../services/authentication/AuthenticationContext";
import PropTypes from "prop-types";

const Protected = ({ requiredRole }) => {
  const { userInfo, isAuthorized } = useContext(AuthenticationContext);
  if (!userInfo) {
    console.log("Usuario no logueado, volviendo a pagina de inicio de sesion.");
    return <Navigate to="/login" />;
  }

  if (requiredRole && !isAuthorized(requiredRole)) {
    console.log(
      `El usuario no tiene el rol ${requiredRole}. Redirigiendo a la home page.`
    );
    return <Navigate to="/" />;
  }

  console.log("Usuario autorizado, renderizando outlet");
  return <Outlet />;
};

Protected.propTypes = {
  requiredRole: PropTypes.string,
};

export default Protected;
