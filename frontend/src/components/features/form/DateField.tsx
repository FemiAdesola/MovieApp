import React from 'react';

import { useFormikContext } from 'formik';
import { DateFieldProps } from '../../types/field';

const DateField = (props: DateFieldProps) => {
    const { values, validateForm, touched, errors } = useFormikContext<any>();
    return (
      <div className="mb-3">
        <label htmlFor={props.field}>{props.displayName}</label>

        <input
          type="date"
          className="form-control"
          id={props.field}
          name={props.field}
          defaultValue={values[props.field]?.toLocaleDateString("en-FI")}
          onChange={(e) => {
            const date = new Date(e.currentTarget.value + "T00:00:00");
            values[props.field] = date;
            validateForm();
          }}
        />
        {touched[props.field] && errors[props.field] ? (
          <div className="text-danger">{errors[props.field]?.toString()}</div>
        ) : null}
      </div>
    );
};

export default DateField;