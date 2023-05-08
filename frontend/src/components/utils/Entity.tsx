import React, { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { Link } from "react-router-dom";

import { EntityProps } from "../types/entity";
import DisplayError from "./DisplayError";
import RecordsPerPageSelection from "./RecordsPerPageSelection";
import Pagination from "./Pagination";
import ResultList from "./ResultList";
import Button from "../features/Button";
import customConfirm from "./ConfirmMessage"

const Entity = <T extends object>(props:EntityProps<T>) =>{
  const [entities, setEntities] = useState<T[]>();
  const [totalAmountOfPages, setTotalAmountOfPages] = useState(0);
  const [recordsPerPage, setRecordsPerPage] = useState(5);
  const [page, setPage] = useState(1);
  const [errors, setErrors] = useState();

  useEffect(() => {
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page, recordsPerPage]);

  const loadData = () => {
    axios
      .get(props.url, {
        params: { page, recordsPerPage },
      })
      .then((response: AxiosResponse<T[]>) => {
        const totalAmountOfRecords = parseInt(
          response.headers["totalamountofrecords"],
          20
        );
        setTotalAmountOfPages(Math.ceil(totalAmountOfRecords / recordsPerPage));
        setEntities(response.data);
      });
  };

  const deleteHandler = async (id: number) => {
    try {
      await axios.delete(`${props.url}/${id}`);
      loadData();
    } catch (err) {
      if (err && err.response) {
        setErrors(err.response.data);
      }
    }
  };

  const buttons = (updateUrl: string, id: number) => (
    <>
      <Link className="btn btn-success" to={updateUrl}>
        Edit
      </Link>

      <Button
        onClick={() => customConfirm(() => deleteHandler(id))}
        className="btn btn-danger"
      >
        Delete
      </Button>
    </>
  );

  return (
    <div>
      <h3>{props.title}</h3>
      <DisplayError errors={errors} />
      <Link className="btn btn-primary" to={props.createURL}>
        Create {props.entityName}
      </Link>
      <RecordsPerPageSelection
        onChange={(amountOfRecords) => {
          setPage(1);
          setRecordsPerPage(amountOfRecords);
        }}
      />
      <Pagination
        currentPage={page}
        totalAmountOfPages={totalAmountOfPages}
        onChange={(newPage) => setPage(newPage)}
        radio={1}
      />
      <ResultList list={entities}>
        <table className="table table-striped">
          {props.children(entities!, buttons)}
        </table>
      </ResultList>
    </div>
  );
};

export default Entity;

