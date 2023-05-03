import React from "react";
import { Form, Formik } from "formik";
import * as Yup from "yup";
import { ActorFormProps } from "../types/actor";

import TextField from "../features/form/TextField";
import Button from "../features/Button";
import { Link } from "react-router-dom";
import DateField from "../features/form/DateField";
import ImageField from "../features/form/ImageField";
import MarkdownField from "../features/form/MarkdownField";

const ActorForm = (props: ActorFormProps) => {
  return (
    <Formik
      initialValues={props.model}
      onSubmit={props.onSubmit}
      validationSchema={Yup.object({
        name: Yup.string()
          .required("This field is required")
          .firstLetterUppercase(),
        dateOfBirth: Yup.date().nullable().required("This field is required"),
      })}
    >
      {(formikProps) => (
        <Form>
          <TextField displayName="Name" field="name" />
          <DateField displayName="Date of Birth" field="dateOfBirth" />
          <ImageField
            displayName="Image"
            field="image"
            imageURL={props.model.imageURL}
          />
          <MarkdownField displayName="Biography" field="biography" />
          <Button disabled={formikProps.isSubmitting} type="submit">
            Save Changes
          </Button>
          <Link to="/actors" className="btn btn-secondary">
            Cancel
          </Link>
        </Form>
      )}
    </Formik>
  );
};

export default ActorForm;
