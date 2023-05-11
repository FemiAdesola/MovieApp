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