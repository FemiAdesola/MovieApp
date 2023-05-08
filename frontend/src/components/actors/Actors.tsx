import React from 'react';
import { Link } from 'react-router-dom';
import Entity from '../utils/Entity';
import { ActorDTO } from '../types/actor';
import { urlActors } from '../common/endpoint';

const Actors = () => {
    return (
      <>
        <Entity<ActorDTO>
          url={urlActors}
          createURL="/actors/create"
          title="Actors"
          entityName="Actor"
        >
          {(actors, buttons) => (
            <>
              <thead>
                <tr>
                  <th></th>
                  <th>Name</th>
                </tr>
              </thead>
              <tbody>
                {actors?.map((actor) => (
                  <tr key={actor.id}>
                    <td>{buttons(`/actors/update/${actor.id}`, actor.id)}</td>
                    <td>{actor.name}</td>
                  </tr>
                ))}
              </tbody>
            </>
          )}
        </Entity>
      </>
    );
};

export default Actors;