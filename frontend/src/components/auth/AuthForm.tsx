import React from 'react';
import * as Yup from 'yup';
import { Form, Formik } from 'formik';

import { AuthFormProps } from './auth';
import TextField from '../features/form/TextField';
import Button from '../features/Button';
import { Link } from 'react-router-dom';


const AuthForm = (props: AuthFormProps) => {
    return (
        <Formik
            initialValues={props.model}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                email: Yup.string().required('This field is required')
                    .email('You have to insert a valid email'),
                password: Yup.string().required('This field is required')
            })}
        >
            {formikProps => (
                <Form>
                    <TextField displayName="Email" field="email" />
                    <TextField displayName="Password" field="password" type="password" />

                    <Button disabled={formikProps.isSubmitting} type="submit">Send</Button>
                    <Link className="btn btn-secondary" to="/">Cancel</Link>
                </Form>
            )}
        </Formik>
    )
}
export default AuthForm;