import axios from 'axios';
import { useState } from 'react';
import { toast } from 'react-toastify';

import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Link from '@mui/material/Link';
import MailLockOutlined from '@mui/icons-material/MailLockOutlined';
import Paper from '@mui/material/Paper';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';


const RegistrationForm = ({ onSuccess, defaultEmail, token }) => {
  const [show, setShow] = useState({
    password: false,
    confirmPassword: false,
  });

  const handleClickShow = (field) => {
    setShow((prev) => ({ ...prev, [field]: !prev[field] }));
  };
  
  const handleMouseDownPassword = (e) => {
    e.preventDefault();
  };
  
  const handleMouseUpPassword = (e) => {
    e.preventDefault();
  };
  
  const [errors, setErrors] = useState({});

  const validate = (formValues) => {
    const newErrors = {};

    // Walidacja imienia
    if (!formValues.firstName) {
      newErrors.firstName = "Imię jest wymagane.";
    } else if (formValues.firstName.length > 100) {
      newErrors.firstName = "Imię nie może być dłuższe niż 100 znaków.";
    }

    // Walidacja nazwiska
    if (!formValues.lastName) {
      newErrors.lastName = "Nazwisko jest wymagane.";
    } else if (formValues.lastName.length > 100) {
      newErrors.lastName = "Nazwisko nie może być dłuższe niż 100 znaków.";
    }

    // Walidacja adresu email
    if (!formValues.email) {
      newErrors.email = "Adres email jest wymagany.";
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formValues.email)) {
      newErrors.email = "Nieprawidłowy adres email.";
    } else if (formValues.email.length > 255) {
      newErrors.email = "Adres email nie może być dłuższy niż 255 znaków.";
    }

    // Walidacja numeru telefonu
    if (!formValues.phone) {
      newErrors.phone = "Numer telefonu jest wymagany.";
    } else if (!/^[0-9]{7,12}$/.test(formValues.phone)) {
      newErrors.phone = "Nieprawidłowy numer telefonu.";
    }

    // Walidacja hasła
    if (!formValues.password) {
      newErrors.password = "Hasło jest wymagane.";
    } else {
      if (formValues.password.length < 8) {
        newErrors.password = "Hasło musi mieć co najmniej 8 znaków.";
      }
      if (!/[A-Z]/.test(formValues.password)) {
        newErrors.password = "Hasło musi zawierać co najmniej jedną wielką literę.";
      }
      if (!/[a-z]/.test(formValues.password)) {
        newErrors.password = "Hasło musi zawierać co najmniej jedną małą literę.";
      }
      if (!/[0-9]/.test(formValues.password)) {
        newErrors.password = "Hasło musi zawierać co najmniej jedną cyfrę.";
      }
      if (!/[^a-zA-Z0-9]/.test(formValues.password)) {
        newErrors.password = "Hasło musi zawierać znak specjalny.";
      }
    }

    // Walidacja potwierzenia hasła
    if (!formValues.confirmPassword) {
      newErrors.confirmPassword = "Potwierdzenie nowego hasła jest wymagane.";
    } else if (formValues.confirmPassword !== formValues.password) {
      newErrors.confirmPassword = "Potwierdzenie hasła musi być identyczne z hasłem.";
    }

    return newErrors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Odczytanie danych z formularza
    const data = new FormData(e.currentTarget);
    const formValues = Object.fromEntries(data.entries());

    // Napisanie emaila z formularza, jeżeli jest podany (tworzenie konta pracownika)
    if (defaultEmail)
      formValues.email = defaultEmail;

    const newErrors = validate(formValues);
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }
    setErrors({});

    // Wybranie odpowiedniego route dla API
    const URL = (defaultEmail)
      ? `https://localhost:7129/api/auth/register-employee/${token}`
      : 'https://localhost:7129/api/auth/register-client';

    const payload = {
      ...formValues,
      phone: `${formValues.prefix}${formValues.phone}`,
    };

    try {
      const res = await axios.post(
        URL,
        payload,
        { headers: { 'Content-Type': 'application/json' } }
      );

      const { id: userId } = res.data

      // Zwrócenie userId i maila gdy wymagane
      if(onSuccess)
        onSuccess(userId, formValues.email);
      
    } catch (err) {
      // Pobranie wiadomości błędu
      const message = err.response?.data?.title || err.message;
      toast.error(message);
    }
  };

  return (
    <Container maxWidth="xs">
      <Paper elevation={10} sx={{ marginTop: 8, padding: 2 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1, mb: 4 }}>
          <Avatar sx={{ mx: 'auto', bgcolor: 'primary.main' }}>
            <MailLockOutlined />
          </Avatar>
          <Typography variant="subtitle1" align="center">
            {defaultEmail ? "Utwórz konto dla pracownika." : "Utwórz konto, aby umówić się na wizytę."}
          </Typography>
        </Box>

        <Box
          component="form"
          onSubmit={handleSubmit}
          noValidate
          sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}
        >
          <Box
            sx={{
              display: 'flex',
              flexDirection: { xs: 'column', sm: 'row' },
              gap: 2,
            }}
          >
            <TextField
              label="Imię"
              name="firstName"
              fullWidth
              size="small"
              error={!!errors.firstName}
              helperText={errors.firstName}
            />
            <TextField
              label="Nazwisko"
              name="lastName"
              fullWidth
              size="small"
              error={!!errors.lastName}
              helperText={errors.lastName}
            />
          </Box>

          <TextField
            label="Email"
            type="email"
            name="email"
            defaultValue={defaultEmail || ""}
            disabled={!!defaultEmail}
            fullWidth
            size="small"
            error={!!errors.email}
            helperText={errors.email}
          />

          <Box
            sx={{
              display: 'flex',
              flexDirection: { xs: 'column', sm: 'row' },
              gap: 2,
            }}
          >
            <TextField
              label="Prefiks"
              name="prefix"
              defaultValue="+48"
              size="small"
              sx={{ maxWidth: '80px' }}
            />
            <TextField
              label="Numer telefonu"
              name="phone"
              fullWidth
              size="small"
              error={!!errors.phone}
              helperText={errors.phone}
            />
          </Box>

          <TextField
            label="Hasło"
            name="password"
            type={show.password ? 'text' : 'password'}
            fullWidth
            size="small"
            sx={{ mt: 2 }}
            slotProps={{
              input: {
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label={
                        show.password ? 'Ukryj hasło' : 'Pokaż hasło'
                      }
                      onClick={() => handleClickShow('password')}
                      onMouseDown={handleMouseDownPassword}
                      onMouseUp={handleMouseUpPassword}
                      edge="end"
                    >
                      {show.password ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              },
            }}
            error={!!errors.password}
            helperText={errors.password}
          />

          <TextField
            label="Powtórz hasło"
            name="confirmPassword"
            type={show.confirmPassword ? 'text' : 'password'}
            fullWidth
            size="small"
            slotProps={{
              input: {
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label={
                        show.confirmPassword
                          ? 'Ukryj hasło'
                          : 'Pokaż hasło'
                      }
                      onClick={() => handleClickShow('confirmPassword')}
                      onMouseDown={handleMouseDownPassword}
                      onMouseUp={handleMouseUpPassword}
                      edge="end"
                    >
                      {show.confirmPassword ? (
                        <VisibilityOff />
                      ) : (
                        <Visibility />
                      )}
                    </IconButton>
                  </InputAdornment>
                ),
              },
            }}
            error={!!errors.confirmPassword}
            helperText={errors.confirmPassword}
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            sx={{ mt: 2 }}
          >
            Zarejestruj się
          </Button>
        </Box>

        <Typography
          variant="body1"
          align="right"
          color="textSecondary"
          sx={{ fontSize: 12, fontWeight: 'light', mt: 1 }}
        >
          Masz już konto?{' '}
          <Link href="#" color="primary" underline="none" noWrap>
            Zaloguj się
          </Link>
        </Typography>
      </Paper>
    </Container>
  );
};

export default RegistrationForm;