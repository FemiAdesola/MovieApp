import React from 'react';
import ActorForm from './ActorForm';
import UpdateEntity from '../utils/UpdateEntity';
import { ActorDTO, CreateActorDTO } from '../types/actor';
import { urlActors } from '../common/endpoint';
import { convertActorToFormData } from '../features/convertToFormData';

const UpdateActor = () => {
  const transform = (actor: ActorDTO): CreateActorDTO => {
    return {
      name: actor.name,
      imageURL: actor.image,
      biography: actor.biography,
      dateOfBirth: new Date(actor.dateOfBirth),
    };
  };

  return (
    <div className="container">
      <UpdateEntity<CreateActorDTO, ActorDTO>
        url={urlActors}
        indexURL="/actors"
        entityName="Actor"
        transformFormData={convertActorToFormData}
        transform={transform}
      >
        {(entity, update) => (
          <ActorForm
            model={entity}
            onSubmit={async (values) => await update(values)}
          />
        )}
      </UpdateEntity>
    </div>
  );
};

export default UpdateActor;