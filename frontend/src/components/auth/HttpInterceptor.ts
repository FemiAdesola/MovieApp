import axios from "axios";
import { getToken } from "./Jwt";

export default function configureInterceptor() {
  axios.interceptors.request.use(
   (config) =>{
      const token = getToken();

      if (token) {
        config.headers.Authorization = `bearer ${token}`;
      }

      return config;
    },
    (error) =>{
      return Promise.reject(error);
    }
  );
}
