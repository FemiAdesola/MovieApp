import React from 'react';
import { Link } from 'react-router-dom';

const Actors = () => {
    return (
      <div className="container">
        <h3>Actors</h3>
        <Link className="btn btn-primary" to="create">
          Create Actor
        </Link>
      </div>
    );
};

export default Actors;