import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import NavBarComponent from './components/NavBarComponent';
import Home from './pages/Home';
import Login from './pages/Login';
import "bootstrap/scss/bootstrap.scss";
import ProvideAuth, { useAuth } from './components/ProvideAuth';
import AlertComponent from './components/AlertComponent';



const App=() => {

  return (
    <ProvideAuth>
      <AlertComponent />
      <Router>
        <NavBarComponent />
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route
            path="/home"
            element={
              <PrivateRoute>
                <Home />
              </PrivateRoute>
            }
          />
          <Route path="*" element={<Navigate to="/login" />} />
        </Routes>
      </Router>
    </ProvideAuth>
  );
}


const PrivateRoute = ({ children }) => {
  let auth = useAuth();
  if (!auth) {
    return (
      <Navigate to="/login" />
    );
  }
  return children;
};

export default App;
