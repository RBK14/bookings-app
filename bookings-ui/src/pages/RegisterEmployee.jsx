import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

import { CircularProgress, Container } from "@mui/material";

import RegistrationForm from "../components/RegistrationForm";

const RegisterEmployee = () => {
  const { token } = useParams();
  const navigate = useNavigate();

  const [employeeEmail, setEmployeeEmail] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkToken = async () => {
      try {
        const res = await axios.get(
          `https://localhost:7129/api/auth/tokens/${token}`
        );

        if (res.data && res.data.email) {
          setEmployeeEmail(res.data.email);
        } else {
          navigate("/errors");
        }
      } catch (err) {
        console.error("Błąd walidacji tokena:", err.response?.data || err.message);
        navigate("/errors");
      } finally {
        setLoading(false);
      }
    };

    checkToken();
  }, [token, navigate]);

  if (loading) {
    return (
      <Container sx={{ display: "flex", justifyContent: "center", mt: 4 }}>
        <CircularProgress />
      </Container>
    );
  }

  return <RegistrationForm defaultEmail={employeeEmail} token={token}/>;
};

export default RegisterEmployee;
