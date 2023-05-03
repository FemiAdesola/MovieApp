import { FormikHelpers } from "formik";

export interface ActorDTO{
    id: number;
    name: string;
    biography: string;
    dateOfBirth: Date;
    picture: string;
}

export interface CreateActorDTO{
    name: string;
    dateOfBirth?: Date;
    picture?: File;
    pictureURL?: string;
    biography?: string;
}

export interface ActorMovieDTO {
    id: number;
    name: string;
    character: string;
    picture: string;
}

export interface ActorFormProps {
    model: CreateActorDTO;
    onSubmit(values: CreateActorDTO, action: FormikHelpers<CreateActorDTO>): void;
}