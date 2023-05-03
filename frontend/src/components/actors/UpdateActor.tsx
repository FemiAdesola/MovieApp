import React from 'react';
import ActorForm from './ActorForm';

const UpdateActor = () => {
    return (
      <div className="container">
        <h3>Update actors</h3>
        <ActorForm
          model={{ name: "Check ok", dateOfBirth: new Date('1996-06-01T:00:00:00') }}
          onSubmit={(values) => console.log(values)}
        />
      </div>
    );
};

export default UpdateActor;