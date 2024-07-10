import { Button, Card, Container } from "react-bootstrap";
import MapComponent from "../components/MapComponent";
import { useEffect, useState } from "react";
import connection from "../services/SignalrService";
import useStore from "../Store/ZustandStore";
import CardsComponent from "../components/CardsComponent";

const Home = () => {
  const { StartSim, StopSim, OpenConnection, CloseConnection, Orders } =
    useStore((state) => state);
  useEffect(() => {
    //console.log(Orders)
    OpenConnection();
    return () => {
      CloseConnection();
    };
  }, []);

  return (
    <Container className="mt-5 main">
      <Container>
        <Card className="mb-5" style={{ width: "18rem" }}>
          <Card.Header>
            <Card.Title>Simulation</Card.Title>
          </Card.Header>
          <Card.Body>
            <Button variant="success" onClick={() => StartSim()}>
              Start
            </Button>
            <Button className="ml-3" variant="danger" onClick={() => StopSim()}>
              Stop
            </Button>
          </Card.Body>
        </Card>
      </Container>
      <Container style={{overflow:"scroll",display:"flex", gap:4}}>
        {[...Orders].map((order, index) => (
          <CardsComponent order={order} key={index} />
        ))}
      </Container>
      <MapComponent />
    </Container>
  );
};

export default Home;
