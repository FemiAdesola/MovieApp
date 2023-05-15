import {  RouterProvider } from "react-router-dom";
import { router } from "./components/utils/Router";

import configureValidations from "./components/common/validation";
import { useEffect, useState } from "react";
import { Claim } from "./components/auth/auth";
import AuthenticationContext from "./components/auth/AuthenticationContext";
import { getClaims } from "./components/auth/Jwt";
import configureInterceptor from "./components/auth/HttpInterceptor";


configureValidations();
configureInterceptor();

const App = () => {
  const [claims, setClaims] = useState<Claim[]>([]);

  useEffect(() => {
    setClaims(getClaims());
  }, []);

  const isAdmin = () => {
    return (
      claims.findIndex(
        (claim) => claim.name === "role" && claim.value === "admin"
      ) > -1
    );
  };

  return (
    <AuthenticationContext.Provider value={{ claims, update: setClaims }}>
      {/* {router.routes && !isAdmin() ? (
        <>You are not allowed to see ths page</>
      ) : ( */}
      <RouterProvider router={router} />
       {/* )}  */}
    </AuthenticationContext.Provider>
  );
}

export default App;

