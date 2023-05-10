import { CreateActorDTO } from "../types/actor";
import { CreateMovieDTO } from "../types/movie";


export const convertActorToFormData = (actor: CreateActorDTO): FormData => {
  const formData = new FormData();

  formData.append("name", actor.name);

  if (actor.biography) {
    formData.append("biography", actor.biography);
  }

  if (actor.dateOfBirth) {
    formData.append("dateOfBirth", formatDate(actor.dateOfBirth));
  }

  if (actor.image) {
    formData.append("image", actor.image);
  }

  return formData;
}

export const formatDate = (date: Date) =>{
  date = new Date(date);
  const format = new Intl.DateTimeFormat("en", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  });

    const [
        { value: month }, ,
        { value: day }, ,
        { value: year }] =
    format.formatToParts(date);

  return `${year}-${month}-${day}`;
}

export const convertMovieToFormData =(movie: CreateMovieDTO) =>{
  const formData = new FormData();

  formData.append("title", movie.title);

  if (movie.summary) {
    formData.append("summary", movie.summary);
  }

  formData.append("trailer", movie.trailer);
  formData.append("inCinemas", String(movie.inCinemas));

  if (movie.releaseDate) {
    formData.append("releaseDate", formatDate(movie.releaseDate));
  }

  if (movie.poster) {
    formData.append("poster", movie.poster);
  }

  formData.append("categoryIds", JSON.stringify(movie.categoryIds));
  formData.append("moviecinemaIds", JSON.stringify(movie.movieCinemaIds));
  formData.append("actors", JSON.stringify(movie.actors));

  return formData;
}