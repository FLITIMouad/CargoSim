import connection from "../services/SignalrService";
import { clearAlert } from "../utils/HelperAlert";

const openConnection=(set)=>{
    try{
        connection.on('ordersHub', (message) => {
            const data=JSON.parse(message);
            set({Orders:[]})
            set({Orders:[...data]})
        });
    
    }
    catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}

const closeConnection=(set)=>{
    try{
        connection.off("ordersHub")
    }
    catch(ex){
        console.log(ex)
        set({Error:ex.message})
    }
    finally{
        clearAlert(set);
    }
}


export {openConnection,closeConnection}