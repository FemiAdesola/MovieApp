import React from "react";
import { IFilterMoviesFormProps} from "../types/movie";
import { Field, Form, Formik } from "formik";
import { CategoryDTO } from "../types/category";
import Button from "../features/Button";

const FilterMovies = () => {
    const categories: CategoryDTO[] = [
      { id: 1, name: "Drama" },
      { id: 2, name: "Comedy" },
      { id: 3, name: "Warmovies"}
    ];

  const initialState: IFilterMoviesFormProps = {
    title: "",
    categoryId: 0,
    upcomingReleases: false,
    inCinemas: false,
    page: 1,
    recordsPerPage: 10,
  };

  return (
    <div className="container">
      <h3>Filter movies</h3>
      <Formik
        initialValues={initialState}
        onSubmit={(values) => {
          console.log(values);
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
                    }}
                  >
                    Clear
                  </Button>
                </div>
              </div>
            </Form>
          </>
        )}
      </Formik>
    </div>
  );
};

export default FilterMovies;
