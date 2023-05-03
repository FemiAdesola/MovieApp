import { RouterProvider } from "react-router-dom";
import { router } from "./components/utils/Router";
import configureValidations from "./components/common/validation";

configureValidations();

const App = () => {
  return <RouterProvider router={router} />;
};

export default App;
