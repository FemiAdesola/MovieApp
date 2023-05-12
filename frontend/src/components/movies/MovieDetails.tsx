import React, { useEffect, useState } from 'react';
import axios, { AxiosResponse } from 'axios';
import { Link, useParams } from 'react-router-dom';

import { urlMovies, urlRatings } from "../common/endpoint";
import { IMovieDTO } from '../types/movie';
import Loading from '../utils/Loading';
import generateEmbeddedVideoURL from '../features/generateEmbeddedVideoURL';
import ReactMarkdown from 'react-markdown';
import { CoordinateDTO } from '../types/map';
import Map from '../utils/Map';
import Ratings from '../ratings/Ratings';
import Swal from 'sweetalert2';


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

   const transformCoordinates = (): CoordinateDTO[] => {
    if (movie?.movieCinemas) {
        const coordinates = movie.movieCinemas.map((movieCinemas) => {
          return {
            lat: movieCinemas.latitude,
            lng: movieCinemas.longitude,
            name: movieCinemas.name,
          } as CoordinateDTO;
        });

        return coordinates;
    }

    return [];
    }

  const handleRate = (rate: number) => {
    axios.post(urlRatings, { rating: rate, movieId: id }).then(() => {
      Swal.fire({ icon: "success", title: "Rating received" });
    });
  };

    return movie ? (
      <>
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
        | {movie.releaseDate.toDateString()} {" "}
        | Your vote:<Ratings
          maximumValue={5}
          selectedValue={movie.userVote}
          onChange={handleRate}
        /> | Average rating: {movie.averageVote}
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
        {movie.summary ? (
          <div style={{ marginTop: "1rem" }}>
            <h3>Summary</h3>
            <div>
              <ReactMarkdown>{movie.summary}</ReactMarkdown>
            </div>
          </div>
        ) : null}
        {movie.actors && movie.actors.length > 0 ? (
          <div style={{ marginTop: "1rem" }}>
            <h3>Actors</h3>
            <div style={{ display: "flex", flexDirection: "column" }}>
              {movie.actors?.map((actor) => (
                <div key={actor.id} style={{ marginBottom: "2px" }}>
                  <img
                    alt="img"
                    src={actor.image}
                    style={{ width: "50px", verticalAlign: "middle" }}
                  />
                  <span
                    style={{
                      display: "inline-block",
                      width: "200px",
                      marginLeft: "1rem",
                    }}
                  >
                    {actor.name}
                  </span>
                  <span
                    style={{
                      display: "inline-block",
                      width: "45px",
                    }}
                  >
                    ...
                  </span>
                  <span>{actor.character}</span>
                </div>
              ))}
            </div>
          </div>
        ) : null}
        {movie.movieCinemas && movie.movieCinemas.length > 0 ? (
          <div>
            <h2>Showing on</h2>
            <Map coordinates={transformCoordinates()} readOnly={true} />
          </div>
        ) : null}
      </>
    ) : (
      <Loading />
    );
};

export default MovieDetails;