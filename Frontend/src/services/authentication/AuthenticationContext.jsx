import { useState, useEffect, createContext, useContext } from "react";
import PropTypes from "prop-types";
import jwt_decode from "jwt-decode";

export const AuthenticationContext = createContext();

const tokenValue = localStorage.getItem("token");

export const AuthenticationContextProvider = ({ children }) => {
  const [token, setToken] = useState(tokenValue);
  const [userInfo, setUserInfo] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      try {
        const decoded = jwt_decode(token);
        setUserInfo(decoded);
      } catch (error) {
        console.error("Error decoding JWT token:", error);
        setUserInfo(null);
      }
    } else {
      setUserInfo(null);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setToken(null);
    setUserInfo(null);
  };

  const handleLogin = (token) => {
    localStorage.setItem("token", token);
    setToken(token);
    const decoded = jwt_decode(token);
    setUserInfo(decoded);
  };

  const isAuthorized = (requiredRole) => {
    return userInfo && userInfo.role === requiredRole;
  };

  return (
    <AuthenticationContext.Provider
      value={{ token, userInfo, handleLogin, handleLogout, isAuthorized }}
    >
      {children}
    </AuthenticationContext.Provider>
  );
};

AuthenticationContextProvider.propTypes = {
  children: PropTypes.node,
};

export const useAuth = () => useContext(AuthenticationContext);
