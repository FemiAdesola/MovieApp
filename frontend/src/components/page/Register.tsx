import React, { useState } from "react";
import axios from "axios";

import { urlUsers } from "../common/endpoint";
import { AuthenticationDTO, UserCredentialsDTO } from "../auth/auth";
import DisplayError from "../utils/DisplayError";
import AuthForm from "../auth/AuthForm";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const navigate = useNavigate();
  const [errors, setErrors] = useState<string[]>([]);
  //  const { update } = useContext(AuthenticationContext);

  const register = async (credentials: UserCredentialsDTO) => {
    try {
      setErrors([]);
        const response = await axios.post<AuthenticationDTO>(
          `${urlUsers}/create`,
          credentials
        );
        console.log(response.data)
  
    //   saveToken(response.data);
    //   update(getClaims());
    //   navigate("/");
    } catch (error) {
      setErrors(error.response.data);
    }
  };
    return (
      <div>
          <h3>Register</h3>
          <DisplayError errors={errors} />
          <AuthForm
            model={{ email: "", password: "" }}
            onSubmit={async (values) => await register(values)}
          />
      </div>
    );
};

export default Register;
