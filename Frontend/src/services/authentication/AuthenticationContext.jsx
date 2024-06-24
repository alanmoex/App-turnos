import { useState, createContext } from "react";
import PropTypes from "prop-types";
import jwt_decode from "jwt-decode";

export const AuthenticationContext = createContext();

const tokenValue = JSON.parse(localStorage.getItem("token"));

export const AuthenticationContextProvider = ({ children }) => {
  const [token, setToken] = useState(tokenValue);
  const [userInfo, setUserInfo] = useState(null);

  useEffect(() => {
    if (token) {
      const decoded = jwt_decode(token);
      setUserInfo(decoded);
    } else {
      setUserInfo(null);
    }
  }, [token]);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setToken(null);
  };

  const handleLogin = (email, role) => {
    const tokenData = { email, role };
    localStorage.setItem("token", JSON.stringify({ email }));
    setToken({ email });
  };

  return (
    <AuthenticationContext.Provider value={{ token, handleLogin, handleLogout }}>
      {children}
    </AuthenticationContext.Provider>
  );
};

AuthenticationContextProvider.propTypes = {
  children: PropTypes.node,
};
