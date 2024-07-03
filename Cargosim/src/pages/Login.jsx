import React, { useEffect } from 'react'
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import "../assets/login.scss"
import { useAuth } from '../components/ProvideAuth';
import { useNavigate } from 'react-router-dom';
const Login = () => {
    let auth = useAuth();
    const navigate = useNavigate();
    useEffect(()=>{
        if(auth.user)
            {
                navigate("/home");
            }
    },[])
    const handleLogin = (event) => {
      event.preventDefault();
      // Handle login logic here
      console.log("Logging in...");
    };
  return (
    <Container fluid="md" className="d-flex align-items-center justify-content-center min-vh-100">
    <Row className="login-container">
      <Col>
        <h2 className="text-center mb-4">Login</h2>
        <Form onSubmit={handleLogin}>
          <Form.Group controlId="formBasicEmail" className="mb-3">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="email" placeholder="Enter email" required />
          </Form.Group>

          <Form.Group controlId="formBasicPassword" className="mb-3">
            <Form.Label>Password</Form.Label>
            <Form.Control type="password" placeholder="Password" required />
          </Form.Group>

          <Button variant="primary" type="submit" className="w-100">
            Login
          </Button>
        </Form>
      </Col>
    </Row>
  </Container>
  )
}

export default Login