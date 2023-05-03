import { FormikHelpers } from "formik";
import { ActorMovieDTO } from "./actor";
import { CategoryDTO } from "./category";
import { CinemasDTO } from "./cinemas";

export interface IMovieDTO
{
    id: number;
    title: string;
    poster: string;
}


export interface IMovieListProps{
    movies? : IMovieDTO[];
}

export interface FrontPageProps {
    inTheaters?: IMovieDTO[];
    upcomingMovies?: IMovieDTO[];
}

export interface IFilterMoviesFormProps{
    title: string;
    categoryId: number;
    upcomingReleases: boolean;
    inCinemas: boolean;
    page: number;
    recordsPerPage: number;
}

export interface CreateMovieDTO{
    title: string;
    inCinemas: boolean;
    trailer: string;
    releaseDate?: Date;
    poster?: File;
    posterURL?: string;


}
export interface MovieFormProps {
  model: CreateMovieDTO;
  onSubmit(
    values: CreateMovieDTO,
    actions: FormikHelpers<CreateMovieDTO>
  ): void;
}