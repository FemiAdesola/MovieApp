import React, { useState } from 'react';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';

import { MovieFormProps } from "../types/movie";
import TextField from '../features/form/TextField';
import DateField from '../features/form/DateField';
import ImageField from '../features/form/ImageField';
import MarkdownField from '../features/form/MarkdownField';
import { ActorMovieDTO } from '../types/actor';
import Button from '../features/Button';
import { Link } from 'react-router-dom';
import CheckBoxField from '../features/form/CheckBoxField';
import MuiltipleSelectorField from '../features/form/MuiltipleSelectorField';
import { MultipleSelectorModel } from '../types/field';
import TypeAheadActorField from '../features/form/TypeAheadActorField';

const MovieForm = (props: MovieFormProps) => {
    const [selectedCategories, setSelectedCategories] = useState(
      mapToModel(props.selectedCategories)
    );
    const [nonSelectedCategories, setNonSelectedCategories] = useState(
      mapToModel(props.nonSelectedCategories)
    );

    const [selectedMovieCinemas, setSelectedMovieCinemas] = useState(
      mapToModel(props.selectedMovieCinemas)
    );
    const [nonSelectedMovieCinemas, setNonSelectedMovieCinemas] = useState(
      mapToModel(props.nonSelectedMovieCinemas)
    );

  const [selectedActors, setSelectedActors] = useState(props.selectedActors);

    function mapToModel(
      items: { id: number; name: string }[]
    ): MultipleSelectorModel[] {
      return items.map((item) => {
        return { key: item.id, value: item.name };
      });
    }
    return (
      <Formik
        initialValues={props.model}
        onSubmit={(values, actions) => {
          values.categoryIds = selectedCategories.map((item) => item.key);
          values.cinemasIds = selectedMovieCinemas.map((item) => item.key);
          values.actors = selectedActors;
          props.onSubmit(values, actions);
        }}
        // onSubmit={props.onSubmit}
        validationSchema={Yup.object({
          title: Yup.string()
            .required("This field is required")
            .firstLetterUppercase(),
        })}
      >
        {(formikProps) => (
          <Form>
            <TextField displayName="Title" field="title" />
            <CheckBoxField displayName="In Cinemas" field="inCinemas" />
            <TextField displayName="Trailer" field="trailer" />
            <DateField displayName="Release Date" field="releaseDate" />
            <ImageField
              displayName="Poster"
              field="poster"
              imageURL={props.model.posterURL}
            />

            {/* <MarkdownField displayName="Summary" field="summary" /> */}

            <MuiltipleSelectorField
              displayName="Categories"
              nonSelected={nonSelectedCategories}
              selected={selectedCategories}
              onChange={(selected, nonSelected) => {
                setSelectedCategories(selected);
                setNonSelectedCategories(nonSelected);
              }}
            />

            <MuiltipleSelectorField
              displayName="Movie Cinemas"
              nonSelected={nonSelectedMovieCinemas}
              selected={selectedMovieCinemas}
              onChange={(selected, nonSelected) => {
                setSelectedMovieCinemas(selected);
                setNonSelectedMovieCinemas(nonSelected);
              }}
            />

            <TypeAheadActorField
              displayName="Actors"
              actors={selectedActors}
              onAdd={(actors) => {
                setSelectedActors(actors);
              }}
              onRemove={(actor) => {
                const actors = selectedActors.filter((x) => x !== actor);
                setSelectedActors(actors);
              }}
              listUI={(actor: ActorMovieDTO) => (
                <>
                  {actor.name} /{" "}
                  <input
                    placeholder="Character"
                    type="text"
                    value={actor.character}
                    onChange={(e) => {
                      const index = selectedActors.findIndex(
                        (x) => x.id === actor.id
                      );

                      const actors = [...selectedActors];
                      actors[index].character = e.currentTarget.value;
                      setSelectedActors(actors);
                    }}
                  />
                </>
               )}
            /> 

            <Button disabled={formikProps.isSubmitting} type="submit">
              Save Changes
            </Button>
            <Link className="btn btn-secondary" to="/">
              Cancel
            </Link>
          </Form>
        )}
      </Formik>
    );
};

export default MovieForm;