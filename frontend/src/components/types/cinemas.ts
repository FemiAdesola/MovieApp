import { FormikHelpers } from "formik";

export interface CreateCinemaDTO {
    name: string;
    latitude?: number;
    longitude?: number;
}

export interface CinemasDTO {
    id: number;
    name: string;
    latitude?: number;
    longitude?: number;
}

export interface CinemasFormProps {
    model: CreateCinemaDTO;
    onSubmit(values: CreateCinemaDTO, actions: FormikHelpers<CreateCinemaDTO>): void;
}
