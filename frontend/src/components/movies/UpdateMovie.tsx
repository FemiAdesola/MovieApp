import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios, { AxiosResponse } from "axios";

import MovieForm from "./MovieForm";
import { CreateMovieDTO, MoviePutGetDTO } from "../types/movie";
import { urlMovies } from "../common/endpoint";
import { convertMovieToFormData } from "../features/convertToFormData";
import DisplayError from "../utils/DisplayError";
import Loading from "../utils/Loading";

const UpdateMovie = () => {
  const navigate = useNavigate();
  const { id }: any = useParams();
  const [movie, setMovie] = useState<CreateMovieDTO>();
  const [moviePutGet, setMoviePutGet] = useState<MoviePutGetDTO>();
  const [errors, setErrors] = useState<string[]>([]);

  useEffect(() => {
    axios
      .get(`${urlMovies}/PutGet/${id}`)
      .then((response: AxiosResponse<MoviePutGetDTO>) => {
        const model: CreateMovieDTO = {
          title: response.data.movie.title,
          inCinemas: response.data.movie.inCinemas,
          trailer: response.data.movie.trailer,
          posterURL: response.data.movie.poster,
          summary: response.data.movie.summary,
          releaseDate: new Date(response.data.movie.releaseDate),
        };

        setMovie(model);
        setMoviePutGet(response.data);
      });
  }, [id]);

  const update = async (movieToEdit: CreateMovieDTO) => {
    try {
      const formData = convertMovieToFormData(movieToEdit);
      await axios({
        method: "put",
        url: `${urlMovies}/${id}`,
        data: formData,
        headers: { "Content-Type": "multipart/form-data" },
      });
      navigate(`/movies/${id}`);
    } catch (error) {
      setErrors(error.response.data);
    }
  };

  return (
    <div>
      <h3>Update movies</h3>
      <DisplayError errors={errors} />
      {movie && moviePutGet ? (
        <MovieForm
          model={movie}
          onSubmit={async (values) => await update(values)}
          selectedCategories={moviePutGet.selectedCategories}
          nonSelectedCategories={moviePutGet.nonSelectedCategories}
          selectedMovieCinemas={moviePutGet.selectedMovieCinemas}
          nonSelectedMovieCinemas={moviePutGet.nonSelectedMovieCinemas}
          selectedActors={moviePutGet.actors}
        />
      ) : (
        <Loading />
      )}
    </div>
  );
};

export default UpdateMovie;
