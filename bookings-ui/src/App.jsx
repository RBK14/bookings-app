import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import RegisterClient from './pages/RegisterClient';
import RegisterEmployee from './pages/RegisterEmployee';
import Home from './pages/Home';

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <Router>
      <Routes>
        {/* Strona startowa */}
        <Route path="/" element={<Home />} />

        {/* Rejestracja klienta */}
        <Route path="/register" element={<RegisterClient />} />

        {/* Rejestracja pracownika */}
        <Route path="/register/:token" element={<RegisterEmployee />} />
      </Routes>

    <ToastContainer
        position="top-center"
        autoClose={3000}
        hideProgressBar
        newestOnTop
        closeOnClick
        pauseOnHover
        draggable
      />

    </Router>
  );
}

export default App;
