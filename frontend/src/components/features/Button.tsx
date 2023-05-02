import React from 'react';
import { ButtonProps } from '../types/button';

const Button = (props: ButtonProps) => {
  return (
    <button
      type={props.type}
      disabled={props.disabled}
      className={props.className}
      onClick={props.onClick}
    >
      {props.children}
    </button>
  );
};

Button.defaultProps = {
  type: "button",
  disabled: false,
  className: "btn btn-primary",
};

export default Button;