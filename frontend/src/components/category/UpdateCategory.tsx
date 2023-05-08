import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import CategoryForm from './CategoryForm';
import axios from 'axios';
import { urlCategories } from '../common/endpoint';
import { AxiosResponse } from 'axios';
import { CreateCategoryDTO } from '../types/category';
import Loading from '../utils/Loading';
import DisplayError from '../utils/DisplayError';

const UpdateCategory = () => {
  const { id }: any = useParams();
    const navigate = useNavigate();
  const [category, setCategory] = useState<CreateCategoryDTO>();
  const [errors, setErrors] = useState();
  
  useEffect(() => {
    axios
      .get(`${urlCategories}/${id}`)
      .then((response: AxiosResponse<CreateCategoryDTO>) => {
        setCategory(response.data);
      });
  }, [id]);

  const update = async (updateCategory: CreateCategoryDTO) => {
    try {
      await axios.put(`${urlCategories}/${id}`, updateCategory)
        navigate("/categories");
    }
    catch (err)
    {
      if (err && err.response) {
        setErrors(err.response.data);
      }
    }
  };

    return (
      <div className="container">
        <h3>Update Categories</h3>
        <DisplayError errors={errors}/>
        {category ?
          <CategoryForm
            model={category}
            onSubmit={async (value) => {
              await update(value);
            }}
          /> : <Loading />}
      </div>
    );
};

export default UpdateCategory;