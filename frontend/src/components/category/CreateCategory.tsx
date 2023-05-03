import React from 'react';
import { Form, Formik } from "formik";
import { Link } from "react-router-dom";
import *  as Yup from "yup"

import Button from '../features/Button';
import TextField from '../features/form/TextField';


const CreateCategory = () => {
    return (
      <div className="container">
        <h3>Create</h3>
        <Formik
          initialValues={{ name: "" }}
          onSubmit={(value) => {
            console.log(value);
          }}
          validationSchema={Yup.object({
            name: Yup.string()
              .required("This field is required")
              .max(50, "Max length is 50 characters")
              .firstLetterUppercase(),
          })}
        >
          <Form>
            <TextField field="name" displayName="Name" />
            <Button type="submit">Save changes</Button>
            <Link className="btn btn-secondary" to="/categories">
              Cancle
            </Link>
          </Form>
        </Formik>
      </div>
    );
};

export default CreateCategory;