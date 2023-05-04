import React from 'react';
import MovieForm from './MovieForm';
import { CategoryDTO } from '../types/category';
import { CinemasDTO } from '../types/cinemas';

const CreateMovie = () => {
    const nonSelectedCategories: CategoryDTO[] = [
      { id: 1, name: "Comedy" },
      { id: 2, name: "Drama" }
    ];
     const nonSelectedMovieCinemas: CinemasDTO[] = [
       { id: 1, name: "Espoo" },
       { id: 2, name: "Helsinki" },
     ];
    
    return (
      <div>
        <h3>Create movies</h3>
        <MovieForm
          model={{ title: "", inCinemas: false, trailer: "" }}
          onSubmit={(values) => console.log(values)}
          selectedCategories={[]}
          nonSelectedCategories={nonSelectedCategories}
          selectedMovieCinemas={[]}
          nonSelectedMovieCinemas={nonSelectedMovieCinemas}
          selectedActors={[]}
        />
      </div>
    );
};

export default CreateMovie;