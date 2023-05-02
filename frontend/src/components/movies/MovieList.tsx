import React from "react";

import { IMovieListProps } from "../types/movie";
import SingleMovie from "./SingleMovie";
import css from "./Movies.module.css";
import ResultList from "../utils/ResultList";

const MovieList = (props: IMovieListProps) => {
  return <ResultList list={props.movies}>
    <div className={css.movieList}>
      {props.movies?.map((movie) => (
        <SingleMovie {...movie} key={movie.id} />
      ))}
    </div>
  </ResultList>;
};

export default MovieList;
