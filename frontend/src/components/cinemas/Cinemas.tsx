import React from 'react';
import { Link } from 'react-router-dom';

const Cinemas = () => {
    return (
      <div className="container">
        <h3>Cinemas</h3>
        <Link className="bt btn-primary" to="create">
          Create cinemas
        </Link>
      </div>
    );
};

export default Cinemas;