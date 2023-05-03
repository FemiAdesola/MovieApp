import { FormikHelpers } from "formik";

export interface CreateCategoryDTO {
    name: string;
}

export interface CategoryDTO {
    id: number;
    name: string;
}

export interface CategoryFormProps{
    model: CreateCategoryDTO;
    onSubmit(value:CreateCategoryDTO, action: FormikHelpers<CreateCategoryDTO>): void;
}