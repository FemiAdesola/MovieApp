import React, { useContext } from "react";
import { IMovieDTO } from "../types/movie";
import css from './Movies.module.css'
import { Link } from "react-router-dom";
import Button from "../features/Button";
import ConfirmMessage from "../utils/ConfirmMessage";
import { urlMovies } from "../common/endpoint";
import axios from "axios";
import AlertContext from "../features/AlertContext";

const SingleMovie = (props: IMovieDTO) => {
  const buildLink = () => `/movies/${props.id}`;
  const customAlert = useContext(AlertContext);
  
  const  deleteMovieHandler = () =>{
    axios.delete(`${urlMovies}/${props.id}`).then(() => {
      customAlert();
    });
  }
  return (
    <div className={css.div}>
      <Link to={buildLink()}>
        <img alt="Poster" src={props.poster} />
      </Link>
      <p>
        <Link to={buildLink()}>{props.title}</Link>
      </p>
      <div>
        <Link
          style={{ marginRight: "1rem" }}
          className="btn btn-info"
          to={`/movies/update/${props.id}`}
        >
          Update
        </Link>
        <Button
          onClick={() => ConfirmMessage(() => deleteMovieHandler())}
          className="btn btn-danger"
        >
          Delete
        </Button>
      </div>
    </div>
  );
};

export default SingleMovie;
