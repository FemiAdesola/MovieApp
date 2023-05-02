import React from "react";
import { IMovieDTO } from "../types/movie";
import css from './Movies.module.css'

const SingleMovie = (props: IMovieDTO) => {
  const buildLink = () => `{/movie/${props.id}`;
  return (
    <div className={css.div}>
      <a href={buildLink()}>
        <img alt="Poster" src={props.poster} />
      </a>
      <p>
        <a href={buildLink()}>{props.title}</a>
      </p>
    </div>
  );
};

export default SingleMovie;
