import React from 'react';
import { urlUsers } from '../common/endpoint';
import axios from 'axios';
import Swal from 'sweetalert2';
import Entity from '../utils/Entity';
import Button from '../features/Button';
import { UserDTO } from './usersType';
import ConfirmMessage from '../utils/ConfirmMessage';



const Users = () => {
    const makeAdmin = async (id: string) =>{
      await doAdmin(`${urlUsers}/makeAdmin`, id);
    }

   const removeAdmin = async (id: string) => {
     await doAdmin(`${urlUsers}/removeAdmin`, id);
   };

const doAdmin = async (url: string, id: string) => {
  await axios.post(url, JSON.stringify(id), {
    headers: { "Content-Type": "application/json" },
  });

  Swal.fire({
    title: "Success",
    text: "Operation done correctly",
    icon: "success",
  });
};

    return (
        <Entity<UserDTO>
            title="Users"
            url={`${urlUsers}/listUsers`}>
        {(users) => (
          <>
            <thead>
              <tr>
                <th></th>
                <th>Email</th>
              </tr>
            </thead>
            <tbody>
              {users?.map((user) => (
                <tr key={user.id}>
                  <td>
                    <Button
                      onClick={() =>
                        ConfirmMessage(
                          () => makeAdmin(user.id),
                          `Do you wish to make ${user.email} an admin?`,
                          "Yes, do it"
                        )
                      }
                    >
                      Make Admin
                    </Button>

                    <Button
                      className="btn btn-danger ms-2"
                      onClick={() =>
                        ConfirmMessage(
                          () => removeAdmin(user.id),
                          `Do you wish to remove ${user.email} as an admin?`,
                          "Yes, do it"
                        )
                      }
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