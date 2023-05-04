import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import axios, { AxiosResponse } from "axios";
import { CategoryDTO } from "../types/category";
import { urlCategories } from "../common/endpoint";

const Categories = () => {
  useEffect(() => {
    axios.get(urlCategories)
      .then((response: AxiosResponse<CategoryDTO[]>) => {
      console.log(response.data);
    });
  }, [])
  return (
    <div className="container">
      <h3>Categories</h3>
      <Link className="btn btn-primary" to="create">
        Create Category
      </Link>
    </div>
  );
};

export default Categories;
