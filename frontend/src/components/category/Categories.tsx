import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import axios, { AxiosResponse } from "axios";
import { CategoryDTO } from "../types/category";
import { urlCategories } from "../common/endpoint";
import ResultList from "../utils/ResultList";
import Button from "../features/Button";

const Categories = () => {
  const [categories, setCategories] = useState<CategoryDTO[]>();

  useEffect(() => {
    axios.get(urlCategories)
      .then((response: AxiosResponse<CategoryDTO[]>) => {
        // console.log(response.data);
        setCategories(response.data);
    });
  }, [])
  return (
    <div>
      <h3>Categories</h3>
      <Link className="btn btn-primary" to="create">
        Create Categories
      </Link>
      <ResultList list={categories}>
        <table className=" table table-striped">
          <thead>
            <tr>
              <th>Options</th>
              <th>Name</th>
            </tr>
          </thead>
          <tbody>
            {categories?.map((category) => (
              <tr key={category.id}>
                <td>
                  <Link
                    className="btn btn-success"
                    to={`/categories/${category.id}`}
                  >
                    Update
                  </Link>
                  <Button className="btn btn-danger">Delete</Button>
                </td>
                <td>
                  <strong>{category.name}</strong>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </ResultList>
    </div>
  );
};

export default Categories;
