import React, { useEffect, useState } from "react";
import { IFilterMoviesFormProps, IMovieDTO } from "../types/movie";
import { Field, Form, Formik } from "formik";
import { CategoryDTO } from "../types/category";
import Button from "../features/Button";
import axios, { AxiosResponse } from "axios";
import { urlCategories, urlMovies } from "../common/endpoint";
import { useLocation, useNavigate } from "react-router-dom";
import MovieList from "./MovieList";
import Pagination from "../utils/Pagination";

const FilterMovies = () => {
  const navigate = useNavigate();
  const [categories, setCategories] = useState<CategoryDTO[]>([]);
  const [movies, setMovies] = useState<IMovieDTO[]>([]);
  const query = new URLSearchParams(useLocation().search);
  const [totalAmountOfPages, setTotalAmountOfPages] = useState(0);

  const initialState: IFilterMoviesFormProps = {
    title: "",
    categoryId: 0,
    upcomingReleases: false,
    inCinemas: false,
    page: 1,
    recordsPerPage: 10,
  };

  useEffect(() => {
    axios
      .get(`${urlCategories}/all`)
      .then((response: AxiosResponse<CategoryDTO[]>) => {
        setCategories(response.data);
      });
  }, []);

  useEffect(() => {
    if (query.get("title")) {
      initialState.title = query.get("title")!;
    }

    if (query.get("categoryId")) {
      initialState.categoryId = parseInt(query.get("categoryId")!, 10);
    }

    if (query.get("upcomingReleases")) {
      initialState.upcomingReleases = true;
    }

    if (query.get("inCinemas")) {
      initialState.inCinemas = true;
    }

    if (query.get("page")) {
      initialState.page = parseInt(query.get("page")!, 10);
    }

    searchMovies(initialState);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const searchMovies = (values: IFilterMoviesFormProps) => {
    modifyURL(values);
    axios
      .get(`${urlMovies}/filter`, { params: values })
      .then((response: AxiosResponse<IMovieDTO[]>) => {
        const list = parseInt(response.headers["totalamountofrecords"], 10);
        setTotalAmountOfPages(Math.ceil(list / values.recordsPerPage));
        setMovies(response.data);
      });
  };

  const modifyURL = (values: IFilterMoviesFormProps) => {
    const queryStrings: string[] = [];

    if (values.title) {
      queryStrings.push(`title=${values.title}`);
    }

    if (values.categoryId !== 0) {
      queryStrings.push(`categoryId=${values.categoryId}`);
    }

    if (values.upcomingReleases) {
      queryStrings.push(`upcomingReleases=${values.upcomingReleases}`);
    }

    if (values.inCinemas) {
      queryStrings.push(`inCinemas=${values.inCinemas}`);
    }

    queryStrings.push(`page=${values.page}`);
    navigate(`/movies/filter?${queryStrings.join("&")}`);
    //  navigate(`/movies/filter?${queryStrings.join("&")}`);
  };

  return (
    <>
      <h3>Filter movies</h3>
      <Formik
        initialValues={initialState}
        onSubmit={(values) => {
          values.page = 1;
          searchMovies(values);
        }}
      >
        {(formikProps) => (
          <>
            <Form>
              <div className="row gx-3 align-items-center mb-3">
                <div className="col-auto">
                  <input
                    type="text"
                    className="form-control"
                    id="title"
                    placeholder="Title of the movie"
                    {...formikProps.getFieldProps("title")}
                  />
                </div>
                <div className="col-auto">
                  <select
                    className="form-select"
                    {...formikProps.getFieldProps("categoryId")}
                  >
                    <option value="0">--Choose a category--</option>
                    {categories.map((category) => (
                      <option key={category.id} value={category.id}>
                        {category.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="col-auto">
                  <div className="form-check">
                    <Field
                      className="form-check-input"
                      id="upcomingReleases"
                      name="upcomingReleases"
                      type="checkbox"
                    />
                    <label
                      className="form-check-label"
                      htmlFor="upcomingReleases"
                    >
                      Upcoming Releases
                    </label>
                  </div>
                </div>
                <div className="col-auto">
                  <div className="form-check">
                    <Field
                      className="form-check-input"
                      id="inCinemas"
                      name="inCinemas"
                      type="checkbox"
                    />
                    <label className="form-check-label" htmlFor="inCinemas">
                      In Cinemas
                    </label>
                  </div>
                </div>
                <div className="col-auto">
                  <Button
                    className="btn btn-primary"
                    onClick={() => formikProps.submitForm()}
                  >
                    Filter
                  </Button>
                  <Button
                    className="btn btn-danger ms-3"
                    onClick={() => {
                      formikProps.setValues(initialState);
                      searchMovies(initialState);
                    }}
                  >
                    Clear
                  </Button>
                </div>
              </div>
            </Form>
            <MovieList movies={movies} />
            <Pagination
              totalAmountOfPages={totalAmountOfPages}
              currentPage={formikProps.values.page}
              onChange={(newPage) => {
                formikProps.values.page = newPage;
                searchMovies(formikProps.values);
              }}
              radio={1}
            />
          </>
        )}
      </Formik>
    </>
  );
};

export default FilterMovies;
