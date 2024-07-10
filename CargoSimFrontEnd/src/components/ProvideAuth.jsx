import { createContext, useContext, useEffect } from "react";
import useStore from "../Store/ZustandStore";
import connection from "../services/SignalrService";
const AuthContext = createContext();
const ProvideAuth = ({ children }) => {
    const {UserInfo} = useStore((state)=>state); 
    const auth = UserInfo ? { user: {userName:UserInfo?.userName} }:null;
    useEffect(()=>{
      if(!auth)
      {
        connection.off('ordersHub');
      }
    },[auth])
    return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
  };
  
  
export const useAuth = ()=> {
    return useContext(AuthContext);
  }

  export default ProvideAuth;