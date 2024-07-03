import { Button, Card, Container } from 'react-bootstrap'
import MapComponent from '../components/MapComponent'
import { useEffect, useState } from 'react';
import connection from '../services/SignalrService';
const Home = () => {
  const [messages, setMessages] = useState([]);
  const [message, setMessage] = useState('');
  
  useEffect(() => {
    connection.on('ReceiveMessage', (message) => {
        setMessages(messages => [...messages, JSON.parse(message)]);
        console.log(message)
    });

    return () => {
        connection.off('ReceiveMessage');
    };
}, []);

const sendMessage = () => {
  connection.invoke('SendMessage', message)
      .catch(err => console.error(err.toString()));
};
  return (
    <Container className='mt-5 main'>  
    <Container>
    <Card className="mb-5" style={{ width: '18rem' }}>
      <Card.Header>
      <Card.Title>Simulation</Card.Title>
      </Card.Header>
      <Card.Body >
        <Button variant="primary">Start</Button>
        <Button className='ml-3' variant="primary">Stop</Button>
      </Card.Body>
    </Card>
    </Container>
      <MapComponent/>
    </Container>
  )
}

export default Home