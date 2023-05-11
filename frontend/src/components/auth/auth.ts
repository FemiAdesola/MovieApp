import { FormikHelpers } from "formik";
import { ReactElement } from "react";

export interface AuthorizedProps {
  authorized: ReactElement;
  notAuthorized?: ReactElement;
  role?: string;
}

export interface Claim{
    name: string;
    value: string;
}

export interface AuthFormProps {
  model: UserCredentialsDTO;
  onSubmit(
    values: UserCredentialsDTO,
    actions: FormikHelpers<UserCredentialsDTO>
  ): void;
}

export interface UserCredentialsDTO {
  email: string;
  password: string;
}

export interface AuthenticationDTO {
  token: string;
  expiration: Date;
}

export interface UserDTO {
  id: string;
  email: string;
}