import React from 'react';
import { DisplayErrorsProps } from '../types/features';

const DisplayError = (props: DisplayErrorsProps) => {
  return (
    <>
      {props.errors ? (
        <ul style={{color:"red"}}>
          {props.errors.map((error, i) => (
            <li key={i}>{error}</li>
          ))}
        </ul>
      ) : null}
    </>
  );
};

export default DisplayError;