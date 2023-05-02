import React, { useEffect, useState } from 'react';
import { FrontPageProps } from '../types/movie';
import MovieList from '../movies/MovieList';

const Home = () => {
     const [movies, setMovies] = useState<FrontPageProps>();

     useEffect(() => {
       const loading = setTimeout(() => {
         return setMovies({
           inTheaters: [
             {
               id: 1,
               title: "Spider-Man: Far From Home",
               poster:
                 "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
             },
             {
               id: 2,
               title: "Spider-Man: Far From Home",
               poster:
                 "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
             },
           ],
           upcomingMovies: [
             {
               id: 3,
               title: "Spider-Man: Far From Home",
               poster:
                 "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
             },
             {
               id: 4,
               title: "Spider-Man: Far From Home",
               poster:
                 "https://upload.wikimedia.org/wikipedia/en/b/bd/Spider-Man_Far_From_Home_poster.jpg",
             },
           ],
         });
       }, 1000);

       return () => clearTimeout(loading);
     }, []);
    return (
      <div className="container">
        <h3> Movies</h3>
        <MovieList movies={movies?.inTheaters} />
        <h3>Upcoming movies</h3>
        <MovieList movies={movies?.upcomingMovies} />
      </div>
    );
};

export default Home;