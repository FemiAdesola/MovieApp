import React from 'react';
import { Claim } from './auth';

const AuthenticationContext = React.createContext<{
  claims: Claim[];
  update(claims: Claim[]): void;
}>({ claims: [], update: () => {} });

export default AuthenticationContext;