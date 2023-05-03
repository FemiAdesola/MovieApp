import React from 'react';

import CinemasForm from './CinemasForm';

const UpdateCinema = () => {
    return (
      <div>
        <h3>create Cinemas</h3>
        <CinemasForm
          model={{
            name: "Esppo",
            latitude: 60.201864684278185,
            longitude: 24.65446675201668,
          }}
          onSubmit={(values) => console.log(values)}
        />
      </div>
    );
};

export default UpdateCinema;