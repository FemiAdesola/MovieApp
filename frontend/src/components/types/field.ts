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