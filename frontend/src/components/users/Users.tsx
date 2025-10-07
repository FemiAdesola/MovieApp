// import React from 'react';
// import { urlUsers } from '../common/endpoint';
// import axios from 'axios';
// import Swal from 'sweetalert2';
// import Entity from '../utils/Entity';
// import Button from '../features/Button';
// import { UserDTO } from './usersType';
// import ConfirmMessage from '../utils/ConfirmMessage';



// const Users = () => {
//     const makeAdmin = async (id: string) =>{
//       await doAdmin(`${urlUsers}/makeAdmin`, id);
//     }

//    const removeAdmin = async (id: string) => {
//      await doAdmin(`${urlUsers}/removeAdmin`, id);
//    };

// const doAdmin = async (url: string, id: string) => {
//   await axios.post(url, JSON.stringify(id), {
//     headers: { "Content-Type": "application/json" },
//   });

//   Swal.fire({
//     title: "Success",
//     text: "Operation done correctly",
//     icon: "success",
//   });
// };

//     return (
//         <Entity<UserDTO>
//             title="Users"
//             url={`${urlUsers}/listUsers`}>
//         {(users) => (
//           <>
//             <thead>
//               <tr>
//                 <th></th>
//                 <th>Email</th>
//               </tr>
//             </thead>
//             <tbody>
//               {users?.map((user) => (
//                 <tr key={user.id}>
//                   <td>
//                     <Button
//                       onClick={() =>
//                         ConfirmMessage(
//                           () => makeAdmin(user.id),
//                           `Do you wish to make ${user.email} an admin?`,
//                           "Yes, do it"
//                         )
//                       }
//                     >
//                       Make Admin
//                     </Button>

//                     <Button
//                       className="btn btn-danger ms-2"
//                       onClick={() =>
//                         ConfirmMessage(
//                           () => removeAdmin(user.id),
//                           `Do you wish to remove ${user.email} as an admin?`,
//                           "Yes, do it"
//                         )
//                       }
//                     >
//                       Remove Admin
//                     </Button>
//                   </td>
//                   <td>{user.email}</td>
//                 </tr>
//               ))}
//             </tbody>
//           </>
//         )}
//       </Entity>
//     );
// };

// export default Users;

import React, { useState } from "react";
import axios from "axios";
import Swal from "sweetalert2";
import Entity from "../utils/Entity";
import Button from "../features/Button";
import { urlUsers } from "../common/endpoint";
import { UserDTO } from "./usersType";

const Users = () => {
  const [loadingId, setLoadingId] = useState<string | null>(null);

  const handleAdminAction = async (id: string, action: "make" | "remove") => {
    const url =
      action === "make" ? `${urlUsers}/makeAdmin` : `${urlUsers}/removeAdmin`;

    const confirmResult = await Swal.fire({
      title:
        action === "make"
          ? "Make this user an admin?"
          : "Remove this user as admin?",
      icon: "question",
      showCancelButton: true,
      confirmButtonText: "Yes, do it",
      cancelButtonText: "Cancel",
    });

    if (!confirmResult.isConfirmed) return;

    try {
      setLoadingId(id);
      await axios.post(
        url,
        { userId: id }, // Wrap ID in object to match backend
        { headers: { "Content-Type": "application/json" } }
      );

      Swal.fire({
        title: "Success",
        text: `User has been ${
          action === "make" ? "promoted to admin" : "removed from admin"
        }.`,
        icon: "success",
      });
    } catch (error: any) {
      Swal.fire({
        title: "Error",
        text: error.response?.data || error.message,
        icon: "error",
      });
    } finally {
      setLoadingId(null);
    }
  };

  return (
    <Entity<UserDTO> title="Users" url={`${urlUsers}/listUsers`}>
      {(users) => (
        <>
          <thead>
            <tr>
              <th>Actions</th>
              <th>Email</th>
            </tr>
          </thead>
          <tbody>
            {users?.map((user) => (
              <tr key={user.id}>
                <td>
                  <Button
                    onClick={() => handleAdminAction(user.id, "make")}
                    disabled={loadingId === user.id}
                  >
                    Make Admin
                  </Button>
                  <Button
                    className="btn btn-danger ms-2"
                    onClick={() => handleAdminAction(user.id, "remove")}
                    disabled={loadingId === user.id}
                  >
                    Remove Admin
                  </Button>
                </td>
                <td>{user.email}</td>
              </tr>
            ))}
          </tbody>
        </>
      )}
    </Entity>
  );
};

export default Users;
