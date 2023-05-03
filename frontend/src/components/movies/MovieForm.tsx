import React, { useState } from 'react';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';

import { MovieFormProps } from "../types/movie";
import TextField from '../features/form/TextField';
import DateField from '../features/form/DateField';
import ImageField from '../features/form/ImageField';
import Button from '../features/Button';
import { Link } from 'react-router-dom';
import CheckBoxField from '../features/form/CheckBoxField';

const MovieForm = (props: MovieFormProps) => {
   
    return (
      <Formik
        initialValues={props.model}
        onSubmit={props.onSubmit}
        validationSchema={Yup.object({
          title: Yup.string()
            .required("This field is required")
            .firstLetterUppercase(),
        })}
      >
        {(formikProps) => (
          <Form>
            <TextField displayName="Title" field="title" />
            <CheckBoxField displayName="In Cinemas" field="inCinemas" />
            <TextField displayName="Trailer" field="trailer" />
            <DateField displayName="Release Date" field="releaseDate" />
            <ImageField
              displayName="Poster"
              field="poster"
              imageURL={props.model.posterURL}
            />
            <Button disabled={formikProps.isSubmitting} type="submit">
              Save Changes
            </Button>
            <Link className="btn btn-secondary" to="/">
              Cancel
            </Link>
          </Form>
        )}
      </Formik>
    );
};

export default MovieForm;