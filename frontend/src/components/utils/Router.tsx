import { createBrowserRouter} from "react-router-dom";

import Layout from "../page/Layout";
import Home from "../page/Home";
import Categories from "../category/Categories";
import UpdateCategory from "../category/UpdateCategory";
import CreateCategory from "../category/CreateCategory";
import Actors from "../actors/Actors";
import UpdateActor from "../actors/UpdateActor";
import CreateActor from "../actors/CreateActor";
import Cinemas from "../cinemas/Cinemas";
import UpdateCinema from "../cinemas/UpdateCinema";
import CreateCinema from "../cinemas/CreateCinema";
import UpdateMovie from "../movies/UpdateMovie";
import CreateMovie from "../movies/CreateMovie";
import FilterMovies from "../movies/FilterMovies";
import NotFound from "./NotFound";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,

    children: [
      { path: "/", element: <Home /> },

      { path: "categories", element: <Categories /> },
      { path: "categories/update/:id", element: <UpdateCategory /> },
      { path: "categories/create", element: <CreateCategory /> },

      { path: "actors", element: <Actors /> },
      { path: "actors/update/:id", element: <UpdateActor /> },
      { path: "actors/create", element: <CreateActor /> },

      { path: "cinemas", element: <Cinemas /> },
      { path: "cinemas/update/:id", element: <UpdateCinema /> },
      { path: "cinemas/create", element: <CreateCinema /> },

      { path: "movies/filter", element: <FilterMovies /> },
      { path: "movies/update/:id", element: <UpdateMovie /> },
      { path: "movies/create", element: <CreateMovie /> },

      { path: "*", element: <NotFound /> },
    ],
  },
]);
