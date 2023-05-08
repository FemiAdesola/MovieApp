import { CreateActorDTO } from "../types/actor";


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