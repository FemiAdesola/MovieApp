import React from 'react';
import MovieForm from './MovieForm';

const UpdateMovie = () => {
    return (
      <div>
        <h3>Update movies</h3>
        <MovieForm
                model={{
                    title: "Toy movies",
                    inCinemas: true,
                    trailer: "http",
                    releaseDate: new Date('2019-01-01T00:00:00')
                }}
          onSubmit={(values) => console.log(values)}
        />
      </div>
    );
};

export default UpdateMovie;