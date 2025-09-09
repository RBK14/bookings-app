import { useState } from 'react';
import RegistrationForm from '../components/RegistrationForm';
import AccountVerification from '../components/AccountVerification';

const RegisterClient = () => {
  const [step, setStep] = useState('form');
  const [email, setEmail] = useState('');

  const handleSuccess = (userEmail) => {
    setEmail(userEmail);
    setStep('verify');
  };

  return (
    <div>
      {step === 'form' && <RegistrationForm onSuccess={handleSuccess} />}
      {step === 'verify' && <AccountVerification email={email} />}
    </div>
  );
};

export default RegisterClient;
