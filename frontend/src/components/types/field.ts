import { ReactElement } from "react";
import { ActorMovieDTO } from "./actor";
import { CoordinateDTO } from "./map";

export interface TextFieldProps {
    field: string
    displayName: string
}

export interface DateFieldProps{
    field: string;
    displayName: string;
}

export interface ImageFieldProps {
    displayName: string;
    imageURL: string;
    field: string;
}

export interface MarkdownFieldProps{
    displayName: string;
    field: string;
}

export interface MapFieldProps {
  coordinates: CoordinateDTO[];
  latField: string;
  lngField: string;
}

export interface CheckBoxFieldProps {
  displayName: string;
  field: string;
}

export interface MultipleSelectorFieldProps {
  displayName: string;
  selected: MultipleSelectorModel[];
  nonSelected: MultipleSelectorModel[];
  onChange(
    selected: MultipleSelectorModel[],
    nonSelected: MultipleSelectorModel[]
  ): void;
}

export interface MultipleSelectorModel {
  key: number;
  value: string;
}

export interface TypeAheadActorsFieldProps {
  displayName: string;
  actors: ActorMovieDTO[];
  onAdd(actors: ActorMovieDTO[]): void;
  onRemove(actor: ActorMovieDTO): void;
  listUI(actor: ActorMovieDTO): ReactElement;
}