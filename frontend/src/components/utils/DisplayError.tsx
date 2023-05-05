import React from 'react';
import { DisplayErrorsProps } from '../types/features';

const DisplayError = (props: DisplayErrorsProps) => {
  return (
    <div>
      {props.errors ? (
        <ul style={{color:"red"}}>
          {props.errors.map((error, i) => (
            <li key={i}>{error}</li>
          ))}
        </ul>
      ) : null}
    </div>
  );
};

export default DisplayError;