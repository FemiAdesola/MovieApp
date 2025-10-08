// import React, { useEffect, useState } from "react";
// import MovieForm from "./MovieForm";
// import { CategoryDTO } from "../types/category";
// import { CinemasDTO } from "../types/cinemas";
// import axios, { AxiosResponse } from "axios";
// import { urlMovies } from "../common/endpoint";
// import { CreateMovieDTO, MoviesPostGetDTOProps } from "../types/movie";
// import Loading from "../utils/Loading";
// import { convertMovieToFormData } from "../features/convertToFormData";
// import { useNavigate } from "react-router-dom";
// import DisplayError from "../utils/DisplayError";

// const CreateMovie = () => {
//   const navigate = useNavigate();
//   const [nonSelectedCategories, setNonSelectedCategories] = useState<
//     CategoryDTO[]
//   >([]);

//   const [nonSelectedMovieCinemas, setNonSelectedMovieCinemas] = useState<
//     CinemasDTO[]
//   >([]);
//   const [loading, setLoading] = useState(true);
//   const [errors, setErrors] = useState<string[]>([]);

//   useEffect(() => {
//     axios
//       .get(`${urlMovies}/postget`)
//       .then((response: AxiosResponse<MoviesPostGetDTOProps>) => {
//         setNonSelectedCategories(response.data.categories);
//         setNonSelectedMovieCinemas(response.data.movieCinemas);
//         setLoading(false);
//       });
//   }, []);

//   const create = async (movie: CreateMovieDTO) => {
//     try {
//       const formData = convertMovieToFormData(movie);
//       const response = await axios({
//         method: "post",
//         url: urlMovies,
//         data: formData,
//         headers: { "Content-Type": "multipart/form-data" },
//       });

//       navigate(`/movies/${response.data}`);
//     } catch (error) {
//       setErrors(error.response.data);
//     }
//   };

//   return (
//     <>
//       <h3>Create movies</h3>
//       <DisplayError errors={errors} />
//       {loading ? (
//         <Loading />
//       ) : (
//         <MovieForm
//           model={{ title: "", inCinemas: false, trailer: "" }}
//           onSubmit={async (values) => await create(values)}
//           selectedCategories={[]}
//           nonSelectedCategories={nonSelectedCategories}
//           selectedMovieCinemas={[]}
//           nonSelectedMovieCinemas={nonSelectedMovieCinemas}
//           selectedActors={[]}
//         />
//       )}
//     </>
//   );
// };

// export default CreateMovie;

import React, { useEffect, useState } from "react";
import MovieForm from "./MovieForm";
import { CategoryDTO } from "../types/category";
import { CinemasDTO } from "../types/cinemas";
import axios, { AxiosResponse } from "axios";
import { urlMovies } from "../common/endpoint";
import { CreateMovieDTO, MoviesPostGetDTOProps } from "../types/movie";
import Loading from "../utils/Loading";
import { convertMovieToFormData } from "../features/convertToFormData";
import { useNavigate } from "react-router-dom";
import DisplayError from "../utils/DisplayError";

const CreateMovie = () => {
  const navigate = useNavigate();
  const [nonSelectedCategories, setNonSelectedCategories] = useState<CategoryDTO[]>([]);
  const [nonSelectedMovieCinemas, setNonSelectedMovieCinemas] = useState<CinemasDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [errors, setErrors] = useState<string[]>([]);

  const token = localStorage.getItem("token"); // ✅ Get JWT token from login

  useEffect(() => {
    const fetchPostGetData = async () => {
      try {
        const response: AxiosResponse<MoviesPostGetDTOProps> = await axios.get(
          `${urlMovies}/postget`,
          {
            headers: {
              Authorization: `Bearer ${token}`, // ✅ Attach JWT
            },
          }
        );
        setNonSelectedCategories(response.data.categories);
        setNonSelectedMovieCinemas(response.data.movieCinemas);
        setLoading(false);
      } catch (err: any) {
        console.error("Failed to fetch post-get data:", err.response?.status, err.response?.data);
        setErrors(["You are not authorized to view this data."]);
        setLoading(false);
      }
    };

    fetchPostGetData();
  }, [token]);

  const create = async (movie: CreateMovieDTO) => {
    try {
      const formData = convertMovieToFormData(movie);
      const response = await axios.post(urlMovies, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${token}`, // ✅ Attach JWT here too
        },
      });

      navigate(`/movies/${response.data}`);
    } catch (err: any) {
      console.error("Failed to create movie:", err.response?.status, err.response?.data);
      setErrors(err.response?.data || ["An unexpected error occurred."]);
    }
  };

  return (
    <>
      <h3>Create movies</h3>
      <DisplayError errors={errors} />
      {loading ? (
        <Loading />
      ) : (
        <MovieForm
          model={{ title: "", inCinemas: false, trailer: "" }}
          onSubmit={async (values) => await create(values)}
          selectedCategories={[]}
          nonSelectedCategories={nonSelectedCategories}
          selectedMovieCinemas={[]}
          nonSelectedMovieCinemas={nonSelectedMovieCinemas}
          selectedActors={[]}
        />
      )}
    </>
  );
};

export default CreateMovie;

