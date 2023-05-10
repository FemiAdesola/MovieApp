import { FormikHelpers } from "formik";
import { ActorMovieDTO } from "./actor";
import { CategoryDTO } from "./category";
import { CinemasDTO } from "./cinemas";

export interface IMovieDTO {
  id: number;
  title: string;
  poster: string;
  inCinemas: boolean;
  trailer: string;
  summary?: string;
  releaseDate: Date;
  categories: CategoryDTO[];
  movieCinemas: CinemasDTO[];
  actors: ActorMovieDTO[];
}

export interface IMovieListProps{
    movies? : IMovieDTO[];
}

export interface FrontPageProps {
  inCinemas?: IMovieDTO[];
  upcomingReleases?: IMovieDTO[];
}

export interface IFilterMoviesFormProps{
    title: string;
    categoryId: number;
    upcomingReleases: boolean;
    inCinemas: boolean;
    page: number;
    recordsPerPage: number;
}

export interface CreateMovieDTO {
  title: string;
  inCinemas: boolean;
  trailer: string;
  summary?: string;
  releaseDate?: Date;
  poster?: File;
  posterURL?: string;
  categoryIds?: number[];
  movieCinemaIds?: number[];
  actors?: ActorMovieDTO[];
}

export interface MovieFormProps {
  model: CreateMovieDTO;
  onSubmit(
    values: CreateMovieDTO,
    actions: FormikHelpers<CreateMovieDTO>
  ): void;
  selectedCategories: CategoryDTO[];
  nonSelectedCategories: CategoryDTO[];
  selectedMovieCinemas: CinemasDTO[];
  nonSelectedMovieCinemas: CinemasDTO[];
  selectedActors: ActorMovieDTO[];
}

export interface MoviesPostGetDTOProps {
  categories: CategoryDTO[];
  movieCinemas: CinemasDTO[];
}

export interface MoviePutGetDTO {
  movie: IMovieDTO;
  selectedCategories: CategoryDTO[];
  nonSelectedCategories: CategoryDTO[];
  selectedMovieCinemas: CinemasDTO[];
  nonSelectedMovieCinemas: CinemasDTO[];
  actors: ActorMovieDTO[];
}