import React from 'react';

import CinemasForm from './CinemasForm';

const CreateCinema = () => {
    return (
      <div>
        <h3>create Cinemas</h3>
        <CinemasForm
          model={{ name: '' }}
          onSubmit={values=> console.log(values)}
        />
      </div>
    );
};

export default CreateCinema;