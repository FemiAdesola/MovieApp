import React, { useContext, useState } from "react";
import axios from "axios";

import { urlUsers } from "../common/endpoint";
import { AuthenticationDTO} from "../auth/auth";
import DisplayError from "../utils/DisplayError";
import AuthForm from "../auth/AuthForm";
import { useNavigate } from "react-router-dom";
import { getClaims, saveToken } from "../auth/Jwt";
import AuthenticationContext from "../auth/AuthenticationContext";
import { UserCredentialsDTO } from "../users/usersType";

const Register = () => {
  const navigate = useNavigate();
  const [errors, setErrors] = useState<string[]>([]);
  const { update } = useContext(AuthenticationContext);

  const register = async (credentials: UserCredentialsDTO) => {
    try {
      setErrors([]);
      const response = await axios.post<AuthenticationDTO>(
        `${urlUsers}/create`,
        credentials
      );
      saveToken(response.data);
      update(getClaims());
      navigate("/");
    } catch (error) {
      setErrors(error.response.data);
    }
  };
  return (
    <>
      <h3>Register</h3>
      <DisplayError errors={errors} />
      <AuthForm
        model={{ email: "", password: "" }}
        onSubmit={async (values) => await register(values)}
      />
    </>
  );
};

export default Register;
