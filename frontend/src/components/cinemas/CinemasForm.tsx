import React from 'react';
import * as Yup from "yup";
import { Form, Formik } from "formik";

import { CinemasFormProps } from '../types/cinemas';
import TextField from '../features/form/TextField';
import Button from '../features/Button';
import { Link } from 'react-router-dom';
import MapField from '../features/form/MapField';
import { CoordinateDTO } from '../types/map';

const CinemasForm = (props: CinemasFormProps) => {
   const formCoordinates = (
   ): CoordinateDTO[] | undefined => {
     if (props.model.latitude && props.model.longitude) {
       const response: CoordinateDTO = {
         lat: props.model.latitude,
         lng: props.model.longitude,
       };
       return [response];
     }

     return undefined;
   };

    return (
      <Formik
        initialValues={props.model}
        onSubmit={props.onSubmit}
        validationSchema={Yup.object({
          name: Yup.string()
            .required("This field is required")
            .firstLetterUppercase(),
        })}
      >
        {(formikProps) => (
          <Form>
            <TextField displayName="Name" field="name" />

            <div style={{ marginBottom: "1rem" }}>
              <MapField
                latField="latitude"
                lngField="longitude"
                coordinates={formCoordinates()}
              />
          
            </div>

            <Button disabled={formikProps.isSubmitting} type="submit">
              Save Changes
            </Button>
            <Link className="btn btn-secondary" to="/cinemas">
              Cancel
            </Link>
          </Form>
        )}
      </Formik>
    );
};

export default CinemasForm;