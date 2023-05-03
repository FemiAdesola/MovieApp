import { FormikHelpers } from "formik";

export interface ActorDTO{
    id: number;
    name: string;
    biography: string;
    dateOfBirth: Date;
    image: string;
}

export interface CreateActorDTO{
    name: string;
    dateOfBirth?: Date;
    image?: File;
    imageURL?: string;
    biography?: string;
}

export interface ActorMovieDTO {
    id: number;
    name: string;
    character: string;
    image: string;
}

export interface ActorFormProps {
    model: CreateActorDTO;
    onSubmit(values: CreateActorDTO, action: FormikHelpers<CreateActorDTO>): void;
}