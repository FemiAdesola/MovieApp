import { Field } from 'formik';
import React from 'react';

import { CheckBoxFieldProps } from '../../types/field';

const CheckBoxField = (props: CheckBoxFieldProps) => {
  return (
    <div className="mb-3 form-check">
      <Field
        className="form-check-input"
        id={props.field}
        name={props.field}
        type="checkbox"
      />
      <label htmlFor={props.field}>{props.displayName}</label>
    </div>
  );
};

export default CheckBoxField;