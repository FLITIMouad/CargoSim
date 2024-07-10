import React, { useEffect } from "react";
import { Container, Nav, NavDropdown, Navbar } from "react-bootstrap";
import { useAuth } from "./ProvideAuth";
import useStore from "../Store/ZustandStore";

const NavBarComponent = () => {
  let auth = useAuth();
  const {SignOut} =useStore(state=>state);
  useEffect(() => {
  }, []);
  return (
    <Navbar collapseOnSelect expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand href="#home">Cargo Sim</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
          </Nav>
          {auth ? (
            <Nav>
              <Nav.Link >{auth.user?.userName}</Nav.Link>
              <Nav.Link eventKey={2} onClick={()=>SignOut()}>
                Logout
              </Nav.Link>
            </Nav>
          ) : null}
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavBarComponent;
