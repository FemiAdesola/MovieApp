import React, { useEffect, useState } from 'react';
import MovieForm from './MovieForm';
import { CategoryDTO } from '../types/category';
import { CinemasDTO } from '../types/cinemas';
import axios, { AxiosResponse } from 'axios';
import { urlMovies } from '../common/endpoint';
import { MoviesPostGetDTOProps } from '../types/movie';
import Loading from '../utils/Loading';

const CreateMovie = () => {
  const [nonSelectedCategories, setNonSelectedCategories] =
    useState<CategoryDTO[]>([])
  
    const [nonSelectedMovieCinemas, setNonSelectedMovieCinemas] = useState<
      CinemasDTO[]
      >([]);
  const [loading, setLoading] = useState(true);
  
  useEffect(() => {
    axios.get(`${urlMovies}/postget`)
      .then((response: AxiosResponse<MoviesPostGetDTOProps>) => {
        setNonSelectedCategories(response.data.categories);
        setNonSelectedMovieCinemas(response.data.movieCinemas);
        setLoading(false);
    })
  }, [])
    
    return (
      <div>
        <h3>Create movies</h3>
        {loading ? (
          <Loading />
        ) : (
          <MovieForm
            model={{ title: "", inCinemas: false, trailer: "" }}
            onSubmit={(values) => console.log(values)}
            selectedCategories={[]}
            nonSelectedCategories={nonSelectedCategories}
            selectedMovieCinemas={[]}
            nonSelectedMovieCinemas={nonSelectedMovieCinemas}
            selectedActors={[]}
          />
        )}
      </div>
    );
};

export default CreateMovie;