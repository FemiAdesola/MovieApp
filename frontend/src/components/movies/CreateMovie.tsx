import React from 'react';
import MovieForm from './MovieForm';

const CreateMovie = () => {
    return (
        <div>
            <h3>Create movies</h3>
            <MovieForm
                model={{ title: '', inCinemas: false, trailer: '' }}
                onSubmit={values=>console.log(values)}
            />
        </div>
    );
};

export default CreateMovie;