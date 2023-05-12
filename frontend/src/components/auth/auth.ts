import { FormikHelpers } from "formik";
import { ReactElement } from "react";
import { UserCredentialsDTO } from "../users/usersType";


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

export interface AuthenticationDTO {
  token: string;
  expiration: Date;
}
