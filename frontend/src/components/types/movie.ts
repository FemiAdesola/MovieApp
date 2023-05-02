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