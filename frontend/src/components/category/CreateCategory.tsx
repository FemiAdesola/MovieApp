import React, { useState } from "react";
import axios from "axios";

import CategoryForm from "./CategoryForm";
import { CreateCategoryDTO } from "../types/category";
import { urlCategories } from "../common/endpoint";
import { useNavigate } from "react-router-dom";
import DisplayError from '../utils/DisplayError';


const CreateCategory = () => {
  const [errors, setErrors] = useState<string[]>([]);
  const navigate = useNavigate();
  const create =  async (category: CreateCategoryDTO) => { 
    try {
      await axios.post(urlCategories, category)
      navigate('/categories');
    }
    catch (error) {
      if (error && error.response) {
        setErrors(error.response.data)
      }
    }
  };
  
  return (
    <div className="container">
      <h3>Create Categories</h3>
      <DisplayError errors={errors}/>
      <CategoryForm
        model={{ name: "" }}
        onSubmit={async (value) => {
          await create(value);
        }}
      />
    </div>
  );
};

export default CreateCategory;
