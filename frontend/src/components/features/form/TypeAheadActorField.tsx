import React, { useState } from "react";

import { TypeAheadActorsFieldProps } from "../../types/field";
import { AsyncTypeahead } from "react-bootstrap-typeahead";
import { ActorMovieDTO } from '../../types/actor';
import { urlActors } from "../../common/endpoint";
import axios from "axios";
import { AxiosResponse } from 'axios';

const TypeAheadActorField = (props: TypeAheadActorsFieldProps) => {
  
  const [actors, setActors] = useState<ActorMovieDTO[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  
  const handleSearch = (query: string) => {
    setIsLoading(true)
    axios.get(`${urlActors}/searchByName/${query}`)
      .then((respons: AxiosResponse<ActorMovieDTO[]>)=> {
        setActors(respons.data);
        setIsLoading(false);
      })
   }


  const selected: ActorMovieDTO[] = [];
   const [draggedElement, setDraggedElement] = useState<
     ActorMovieDTO | undefined
     >(undefined);
  
  const handleDragStart=(actor: ActorMovieDTO) =>{
    setDraggedElement(actor);
  }

const handleDragOver=(actor: ActorMovieDTO) =>{
    if (!draggedElement) {
      return;
    }

    if (actor.id !== draggedElement.id) {
      const draggedElementIndex = props.actors.findIndex(
        (x) => x.id === draggedElement.id
      );
      const actorDragIndex = props.actors.findIndex((x) => x.id === actor.id);

      const actors = [...props.actors];
      actors[actorDragIndex] = draggedElement;
      actors[draggedElementIndex] = actor;
      props.onAdd(actors);
    }
}
  
  return (
    <div className="mb-3">
      <label>{props.displayName}</label>
      <AsyncTypeahead
        id="typeahead"
        onChange={(actors) => {
          // @ts-ignore
          if (props.actors.findIndex((x) => x.id === actors[0].id) === -1) {
            // @ts-ignore
            actors[0].character = '';
            // @ts-ignore
            props.onAdd([...props.actors, actors[0]]);
          }
        }}
        options={actors}
        // @ts-ignore
        labelKey={(actor) => actor.name}
        filterBy={() => true}
        isLoading={isLoading}
        onSearch={handleSearch}
        placeholder="Write the name of the actor here..."
        flip={true}
        selected={selected}
        renderMenuItemChildren={(actor) => (
          <div>
            <img
              src={(actor as any).image}
              alt="actor"
              style={{
                height: "65px",
                marginRight: "10px",
                width: "65px",
              }}
            />
            <span>{(actor as any).name}</span>
          </div>
        )}
      />
      <ul className="list-group">
        {props.actors.map((actor) => (
          <li
            key={actor.id}
            draggable={true}
            onDragStart={() => handleDragStart(actor)}
            onDragOver={() => handleDragOver(actor)}
            className="list-group-item list-group-item-action"
          >
            {props.listUI(actor)}
            <span
              className="badge badge-primary badge-pill pointer text-dark"
              style={{ marginLeft: "0.5rem" }}
              onClick={() => props.onRemove(actor)}
            >
              X
            </span>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TypeAheadActorField;
