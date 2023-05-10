import React from "react";
import { IMovieDTO } from "../types/movie";
import css from './Movies.module.css'
import { Link } from "react-router-dom";

const SingleMovie = (props: IMovieDTO) => {
  const buildLink = () => `movies/${props.id}`;
  return (
    <div className={css.div}>
      <Link to={buildLink()}>
        <img alt="Poster" src={props.poster} />
      </Link>
      <p>
        <Link to={buildLink()}>{props.title}</Link>
      </p>
    </div>
  );
};

export default SingleMovie;
