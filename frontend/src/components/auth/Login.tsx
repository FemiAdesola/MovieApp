import React, { useContext, useState } from "react";
import { AuthenticationDTO, UserCredentialsDTO } from "./auth";
import axios from "axios";
import { urlUsers } from "../common/endpoint";
import { useNavigate } from "react-router-dom";
import DisplayError from "../utils/DisplayError";
import AuthForm from "./AuthForm";
import { getClaims, saveToken } from "./Jwt";
import AuthenticationContext from "./AuthenticationContext";

const Login = () => {
  const navigate = useNavigate();
  const [errors, setErrors] = useState<string[]>([]);
  const { update } = useContext(AuthenticationContext);
  
  const login = async (credentials: UserCredentialsDTO) => {
    try {
      setErrors([]);
      const response = await axios.post<AuthenticationDTO>(
        `${urlUsers}/login`,
        credentials
      );
      saveToken(response.data);
      update(getClaims()); //
      navigate("/");
    } catch (error) {
      setErrors(error.response.data);
    }
  };
  return (
    <div>
      <DisplayError errors={errors} />
      <AuthForm
        model={{ email: "", password: "" }}
        onSubmit={async (values) => await login(values)}
      />
    </div>
  );
};

export default Login;
