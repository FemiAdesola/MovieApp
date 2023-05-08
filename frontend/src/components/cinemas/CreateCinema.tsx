import React, { useState } from 'react';
import axios from "axios";

import CinemasForm from './CinemasForm';
import { useNavigate } from 'react-router-dom';
import { urlMovieCinemas } from '../common/endpoint';
import { CreateCinemaDTO } from '../types/cinemas';
import DisplayError from "../utils/DisplayError";

const CreateCinema = () => {
  const [errors, setErrors] = useState<string[]>([]);
  const navigate = useNavigate();
  
  const create = async (movieCinema: CreateCinemaDTO) => {
    try {
      await axios.post(urlMovieCinemas, movieCinema);
      navigate("/cinemas");
    } catch (error) {
      if (error && error.response) {
        setErrors(error.response.data);
      }
    }
  };
    return (
      <div>
        <h3>create Cinemas</h3>

        {/* <DisplayError errors={errors} /> */}
        <CinemasForm
          model={{ name: "" }}
          onSubmit={async (values) => await create(values)}
        />
      </div>
    );
};

export default CreateCinema;