import React from 'react';
import MovieForm from './MovieForm';
import { CategoryDTO } from '../types/category';
import { CinemasDTO } from '../types/cinemas';

const UpdateMovie = () => {
   const nonSelectedCategories: CategoryDTO[] = [
     { id: 2, name: "Drama" },
   ];
    const selectedCategories: CategoryDTO[] = [
      { id: 1, name: "Comedy" },
    ];
  
   const nonSelectedMovieCinemas: CinemasDTO[] = [
     { id: 2, name: "Helsinki" },
   ];
   const selectedMovieCinemas: CinemasDTO[] = [
     { id: 1, name: "Espoo" },

   ];
    return (
      <div>
        <h3>Update movies</h3>
        <MovieForm
          model={{
            title: "Toy movies",
            inCinemas: true,
            trailer: "http",
            releaseDate: new Date("2019-01-01T00:00:00"),
          }}
          onSubmit={(values) => console.log(values)}
          selectedCategories={selectedCategories}
          nonSelectedCategories={nonSelectedCategories}
          selectedMovieCinemas={selectedMovieCinemas}
          nonSelectedMovieCinemas={nonSelectedMovieCinemas}
          selectedActors={[]}
        />
      </div>
    );
};

export default UpdateMovie;