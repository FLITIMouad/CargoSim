import React, { useEffect, useState } from "react";
import { Alert } from "react-bootstrap";
import useStore from "../Store/ZustandStore";

const AlertComponent = () => {
  const { Error, Success } = useStore((state) => state);
  const [variant, SetVariant] = useState("");
  const [enabled, SetEnabled] = useState(false);
  useEffect(() => {
    SetEnabled(Error || Success)
     if(Error && !Success)
     {
        SetVariant("danger")
     }else{
        SetVariant("success")
     }
  }, [Error, Success]);
  return (
    <>
      {enabled ? (
        <Alert key={variant} variant={variant}>
            {Error && !Success ? Error : Success}
        </Alert>
      ) : null}
    </>
  );
};

export default AlertComponent;
