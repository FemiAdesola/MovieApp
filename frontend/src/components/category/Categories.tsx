import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import axios, { AxiosResponse } from "axios";
import { CategoryDTO } from "../types/category";
import { urlCategories } from "../common/endpoint";
import ResultList from "../utils/ResultList";
import Button from "../features/Button";
import Pagination from "../utils/Pagination";
import RecordsPerPageSelection from "../utils/RecordsPerPageSelection";

const Categories = () => {
  const [categories, setCategories] = useState<CategoryDTO[]>();
  const [totalAmountOfPages, setTotalAmountOfPages] = useState(0);
  const [recordsPerPage, setRecordsPerPage] = useState(5);
  const [page, setPage] = useState(1);

  useEffect(() => {
    axios.get(urlCategories, {
      params: {page, recordsPerPage}
    })
      .then((response: AxiosResponse<CategoryDTO[]>) => {
        // console.log(response.data);
        const totalAmountOfRecords = parseInt(
          response.headers["totalamountofrecords"], 20
        );
        setTotalAmountOfPages(Math.ceil(totalAmountOfRecords / recordsPerPage));
        setCategories(response.data);
    });
  }, [page, recordsPerPage])
  return (
    <div>
      <h3>Categories</h3>
      <Link className="btn btn-primary" to="create">
        Create Categories
      </Link>
      <RecordsPerPageSelection
        onChange={amountOfRecords => {
          setPage(1)
          setRecordsPerPage(amountOfRecords);
        }}
      />
      <Pagination
        currentPage={page}
        totalAmountOfPages={totalAmountOfPages}
        onChange={(newPage) => setPage(newPage)}
        radio={1}
      />
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
