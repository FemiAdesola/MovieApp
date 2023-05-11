import React, { useState } from "react";
import { AuthenticationDTO, UserCredentialsDTO } from "./auth";
import axios from "axios";
import { urlUsers } from "../common/endpoint";
import { useNavigate } from "react-router-dom";
import DisplayError from "../utils/DisplayError";
import AuthForm from "./AuthForm";

const Login = () => {
  const navigate = useNavigate();
  const [errors, setErrors] = useState<string[]>([]);
  const login = async (credentials: UserCredentialsDTO) => {
    try {
      setErrors([]);
      const response = await axios.post<AuthenticationDTO>(
        `${urlUsers}/login`,
        credentials
      );
        console.log(response.data)
    //   saveToken(response.data);
    //   update(getClaims());
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
