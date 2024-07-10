import { create } from 'zustand'
import { startSim, stopSim } from '../Actions/SimulationActions'
import { signIn, signOut } from '../Actions/UserActions'
import { closeConnection, openConnection } from '../Actions/OrderAction'

const useStore = create((set) => ({
  /******* Globals States ***** */
  Error:null,
  Success:null,

/******** Order States ***********/
  Orders: [],
  OpenConnection:()=>openConnection(set),
  CloseConnection:()=>closeConnection(set),
  StartSim:()=>startSim(set),
  StopSim: ()=>stopSim(set),
  SimEnabled:false,

  /********* Auth states ********/
  UserInfo:JSON.parse(localStorage.getItem("auth")),
  SignIn:(username,password)=>signIn(set,username,password),
  SignOut:()=>signOut(set)
}))

export default useStore