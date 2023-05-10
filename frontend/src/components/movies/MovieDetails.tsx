import React, { useEffect, useState } from 'react';
import axios, { AxiosResponse } from 'axios';
import { Link, useParams } from 'react-router-dom';

import { urlMovies } from "../common/endpoint";
import { IMovieDTO } from '../types/movie';
import Loading from '../utils/Loading';
import generateEmbeddedVideoURL from '../features/generateEmbeddedVideoURL';

const MovieDetails = () => {
    const { id }: any = useParams();
    const [movie, setMovie] = useState<IMovieDTO>();
    
    useEffect(() => {
      axios
        .get(`${urlMovies}/${id}`)
        .then((response: AxiosResponse<IMovieDTO>) => {
          response.data.releaseDate = new Date(response.data.releaseDate);
          setMovie(response.data);
        });
    }, [id]);
    return movie ? (
      <div>
        <h2>
          {movie.title} ({movie.releaseDate.getFullYear()})
        </h2>
        {movie.categories?.map((category) => (
          <Link
            key={category.id}
            style={{ marginRight: "5px" }}
            className="btn btn-primary btn-sm rounded-pill"
            to={`/movies/filter?categoryId=${category.id}`}
          >
            {category.name}
          </Link>
        ))}{" "}
        | {movie.releaseDate.toDateString()}
        <div style={{ display: "flex", marginTop: "1rem" }}>
          <span style={{ display: "inline-block", marginRight: "1rem" }}>
            <img
              src={movie.poster}
              style={{ width: "225px", height: "315px" }}
              alt="poster"
            />
          </span>
          {movie.trailer ? (
            <div>
              <iframe
                title="youtube-trailer"
                width="560"
                height="315"
                src={generateEmbeddedVideoURL(movie.trailer)}
                frameBorder={0}
                allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen
              ></iframe>
            </div>
          ) : null}
        </div>
      </div>
    ) : (
      <Loading />
    );
};

export default MovieDetails;