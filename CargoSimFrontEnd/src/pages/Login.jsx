import { useEffect } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import "../assets/login.scss";
import { useAuth } from "../components/ProvideAuth";
import { useNavigate } from "react-router-dom";
import useStore from "../Store/ZustandStore";
const Login = () => {
  let auth = useAuth();
  const { SignIn } = useStore((state) => state);
  const navigate = useNavigate();

  useEffect(() => {
    if (auth) {
      navigate("/home");
    }
  }, [navigate, auth]);

  const handleLogin = (event) => {
    event.preventDefault();
    const data = new FormData(event.target);
    const userName = data.get("username");
    const password = data.get("password");
    SignIn(userName, password);
  };
  return (
    <Container
      fluid="md"
      className="d-flex align-items-center justify-content-center min-vh-100"
    >
      <Row className="login-container">
        <Col>
          <h2 className="text-center mb-4">Login</h2>
          <Form onSubmit={handleLogin}>
            <Form.Group controlId="formBasicEmail" className="mb-3">
              <Form.Label>User Name</Form.Label>
              <Form.Control
                type="username"
                name="username"
                placeholder="Enter username"
                required
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword" className="mb-3">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                name="password"
                placeholder="Password"
                required
              />
            </Form.Group>

            <Button variant="primary" type="submit" className="w-100">
              Login
            </Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default Login;
