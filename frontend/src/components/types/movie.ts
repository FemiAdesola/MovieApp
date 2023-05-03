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