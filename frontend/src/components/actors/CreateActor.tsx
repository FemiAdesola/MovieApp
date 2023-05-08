import React, { useState } from 'react';
import ActorForm from './ActorForm';
import { CreateActorDTO } from '../types/actor';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { convertActorToFormData } from '../features/convertToFormData';
import { urlActors } from '../common/endpoint';

const CreateActor = () => {
    const [errors, setErrors] = useState<string[]>([]);
    const navigate = useNavigate();

   const create = async (actor: CreateActorDTO) => {
     try {
       const formData = convertActorToFormData(actor);

       await axios({
         method: "post",
         url: urlActors,
         data: formData,
         headers: { "Content-Type": "multipart/form-data" },
       });
       navigate("/actors");
     } catch (error) {
       if (error && error.response) {
         setErrors(error.response.data);
       }
     }
   };
    return (
      <div className="container">
        <h3>Create actors</h3>
        {/* <DisplayErrors errors={errors} /> */}
        <ActorForm
          model={{ name: "", dateOfBirth: undefined }}
          onSubmit={async (values) => await create(values)}
        />
      </div>
    );
};

export default CreateActor;
