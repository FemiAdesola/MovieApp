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
