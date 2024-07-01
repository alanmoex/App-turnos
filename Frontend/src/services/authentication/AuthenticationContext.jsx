import { useState, useEffect, createContext } from "react";
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
  }, [token]);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setToken(null);
  };

  const handleLogin = (token) => {
    localStorage.setItem("token", token);
    setToken(token);
  };

  return (
    <AuthenticationContext.Provider
      value={{ token, userInfo, handleLogin, handleLogout }}
    >
      {children}
    </AuthenticationContext.Provider>
  );
};

AuthenticationContextProvider.propTypes = {
  children: PropTypes.node,
};
