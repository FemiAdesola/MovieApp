import React, { useEffect, useState } from "react";
import { FrontPageProps } from "../types/movie";
import MovieList from "../movies/MovieList";
import axios, { AxiosResponse } from "axios";
import { urlMovies } from "../common/endpoint";
import AlertContext from "../features/AlertContext";
import Authorized from "../auth/Authorized";

const Home = () => {
  const [movies, setMovies] = useState<FrontPageProps>();

  useEffect(() => {
    loadMoviesData();
  }, []);

  const loadMoviesData = () => {
    axios.get(urlMovies).then((response: AxiosResponse<FrontPageProps>) => {
      setMovies(response.data);
    });
  };

  return (
    <div className="container">
      <AlertContext.Provider
        value={() => {
          loadMoviesData();
        }}
      >
        <Authorized
          authorized={<>You are authorized</>}
          notAuthorized={<>You are  are not authorized</>}
          role="admin"
        />

        <h3> Movies</h3>
        <MovieList movies={movies?.inCinemas} />
        <h3>Upcoming movies</h3>
        <MovieList movies={movies?.upcomingReleases} />
      </AlertContext.Provider>
    </div>
  );
};

export default Home;
