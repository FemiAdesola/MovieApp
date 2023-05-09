import React from 'react';
import { Link } from 'react-router-dom';
import { CinemasDTO } from '../types/cinemas';
import { urlMovieCinemas } from '../common/endpoint';
import Entity from '../utils/Entity';

const Cinemas = () => {
    return (
      <div className="container">
        <h3>Cinemas</h3>
        <Entity<CinemasDTO>
          url={urlMovieCinemas}
          createURL="/cinemas/create"
          title="Cinemas"
          entityName="Movie Cinemas"
        >
          {(cinemas, buttons) => (
            <>
              <thead>
                <tr>
                  <th></th>
                  <th>Name</th>
                </tr>
              </thead>
              <tbody>
                {cinemas?.map((cinema) => (
                  <tr key={cinema.id}>
                    <td>
                      {buttons(`/cinemas/update/${cinema.id}`, cinema.id)}
                    </td>
                    <td>{cinema.name}</td>
                  </tr>
                ))}
              </tbody>
            </>
          )}
        </Entity>

        <Link className="btn btn-primary" to="create">
          Create cinemas
        </Link>
      </div>
    );
};

export default Cinemas;