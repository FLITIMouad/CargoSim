const clearAlert=(set)=>{
    setTimeout(()=>{
        set({Success:"",Error:""})
    },4000)
}

export {clearAlert};