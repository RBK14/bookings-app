import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Container from '@mui/material/Container'
import Paper from '@mui/material/Paper'
import Typography from '@mui/material/Typography'
import { useEffect, useState } from 'react'

const AccountVerification = ( {email} ) => {
  const [timer, setTimer] = useState(0);

  useEffect(() => {
    let interval;
    if (timer > 0) {
      interval = setInterval(() => {
        setTimer((prev) => prev - 1);
      }, 1000);
    }
    return () => clearInterval(interval);
  }, [timer]);

  const handleResend = () => {
    if (timer === 0) {
      // TODO: onResend(email);
      setTimer(60);
    }
  };

  return (
    <Container maxWidth='sm'>
      <Paper elevation={10} sx={{ marginTop: 8, padding: 2 }}>
        <Box sx={{display : 'flex', flexDirection: 'column', gap: 2}}>
          <Typography
            variant='h5'
            align='center'
            sx={{fontWeight: 'bold'}}
          >
            Weryfikacja konta
          </Typography>
          
          <Box sx={{ textAlign: 'center', mt: 2 }}>
            <Typography sx={{ mb: 1.2 }}>
              Na adres <strong>{email}</strong> został wysłany link aktywacyjny do Twojego konta.
            </Typography>
            <Typography sx={{ mb: 1.2 }}>
              Prosimy sprawdzić swoją skrzynkę odbiorczą i kliknąć w link, aby zakończyć proces rejestracji.
            </Typography>
            <Typography sx={{ mb: 1.2 }}>
              Jeśli nie widzisz wiadomości, sprawdź folder SPAM lub niechciane wiadomości.
            </Typography>
            <Typography>
              Pamiętaj, że link jest ważny tylko przez ograniczony czas.
            </Typography>
          </Box>
        </Box>

        <Box sx={{display: 'flex', justifyContent: 'center', mt: 4}}>
          <Button 
          variant="contained" 
          onClick={handleResend} 
          disabled={timer > 0}
        >
          {timer > 0 ? `Wyślij ponownie (${timer}s)` : 'Wyślij link ponownie'}
        </Button>
        </Box>
      </Paper>
    </Container>
  )
}

export default AccountVerification