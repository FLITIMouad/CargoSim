import { createContext, useContext, useEffect } from "react";
const AuthContext = createContext();
const ProvideAuth = ({ children }) => {
    const {userInfo} = { userInfo: {name:"mouad"} }; // Adjust this to get the actual user info
    const auth = { user: userInfo };
    return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
  };
  
  
export const useAuth = ()=> {
    return useContext(AuthContext);
  }

  export default ProvideAuth;