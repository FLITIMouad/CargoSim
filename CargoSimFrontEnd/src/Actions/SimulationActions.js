import axios from "axios"
import httpClient from "../services/HttpClientService"
import { clearAlert } from "../utils/HelperAlert"
const startSim=async (set)=>{
    try{
        console.log(httpClient.defaults)    
        const response = await httpClient.post("/order/sim/start",{})
        
        set({SimEnabled:true,Success:"simulation Started with Success"})
    }catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}

const stopSim=async (set)=>{
    try{
        const response = await httpClient.post("/order/sim/stop",{})
        set({SimEnabled:false,Success:"simulation stoped with Success"})
    }catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}



export {startSim,stopSim}