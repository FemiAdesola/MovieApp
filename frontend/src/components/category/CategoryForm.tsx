import React from "react";
import { Form, Formik } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";

import Button from "../features/Button";
import TextField from "../features/form/TextField";
import { CategoryFormProps } from "../types/category";

const CategoryForm = (props:CategoryFormProps) => {
  return (
    <div className="container">
      <Formik
        initialValues={props.model}
        onSubmit={props.onSubmit}
        validationSchema={Yup.object({
          name: Yup.string()
            .required("This field is required")
            .max(50, "Max length is 50 characters")
            .firstLetterUppercase(),
        })}
      >
        {(formikProps) => (
          <Form>
            <TextField field="name" displayName="Name" />
            <Button disabled={formikProps.isSubmitting} type="submit">
              Save changes
            </Button>
            <Link className="btn btn-secondary" to="/categories">
              Cancle
            </Link>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default CategoryForm;
