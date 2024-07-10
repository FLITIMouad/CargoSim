import httpClient from "../services/HttpClientService"
import { clearAlert } from "../utils/HelperAlert"
const signIn=async (set,username,password)=>{
    try{
        const {data} = await httpClient.post("/user/login",{username,password})
        const result=JSON.stringify(data);
         localStorage.setItem("auth",result)
        set({userInfo:result,Success:"Login with Success"})
        location.pathname="/home";
       
    }catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}

const signOut=async (set)=>{
    try{
         await httpClient.post("/user/logout",{})
         localStorage.removeItem("auth")
        set({userInfo:null,Success:"Logout with Success"})
        location.pathname="/login";
    }catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}

export {signIn,signOut}