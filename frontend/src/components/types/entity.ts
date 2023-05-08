import { ReactElement } from "react";

export interface UpdateEntityProps<TCreation, TRead> {
  url: string;
  entityName: string;
  indexURL: string;
  transform(entity: TRead): TCreation;
  transformFormData?(model: TCreation): FormData;
  children(
    entity: TCreation,
    update: (entity: TCreation) => void
  ): ReactElement;
}


export interface EntityProps<T> {
  url: string;
  createURL: string;
  title: string;
  entityName: string;
  children(
    entities: T[],
    buttons: (updateUrl: string, id: number) => ReactElement
  ): ReactElement;
}