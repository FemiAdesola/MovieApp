import React from "react";
import axios from "axios";

import CategoryForm from "./CategoryForm";
import { CreateCategoryDTO } from "../types/category";
import { urlCategories } from "../common/endpoint";
import { useNavigate } from "react-router-dom";


const CreateCategory = () => {
  const navigate = useNavigate();
  const create =  async (category: CreateCategoryDTO) => { 
    try {
      await axios.post(urlCategories, category)
      navigate('/categories');
    }
    catch (error) { console.log(error)}
  };
  
  return (
    <div className="container">
      <h3>Create Categories</h3>
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
