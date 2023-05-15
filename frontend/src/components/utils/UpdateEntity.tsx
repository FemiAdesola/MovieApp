import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import axios, { AxiosResponse } from 'axios';

import DisplayError from './DisplayError';
import Loading from './Loading';
import { UpdateEntityProps } from "../types/entity";

const UpdateEntity = <TCreation, TRead>(
  props: UpdateEntityProps<TCreation, TRead>
) => {
  const { id }: any = useParams();
  const navigate = useNavigate();
  const [entity, setEntity] = useState<TCreation>();
  const [errors, setErrors] = useState();

  useEffect(() => {
    axios
      .get(`${props.url}/${id}`)
      .then((response: AxiosResponse<TRead>) => {
        setEntity(props.transform(response.data));
      });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const update = async (entityToUpdate: TCreation) => {
    try {
      if (props.transformFormData) {
        const formData = props.transformFormData(entityToUpdate);
        await axios({
          method: 'put',
          url: `${props.url}/${id}`,
          data: formData,
          headers: {'Content-Type':'multipart/form-data'}
        })
      } else {
        await axios.put(`${props.url}/${id}`, entityToUpdate);
      }
      navigate(props.indexURL);
      
    } catch (err) {
      if (err && err.response) {
        setErrors(err.response.data);
      }
    }
  };
  return (
    <>
      <h3>Update {props.entityName}</h3>
      <DisplayError errors={errors} />
      {entity ? props.children(entity, update) : <Loading />}
    </>
  );
};

export default UpdateEntity;

UpdateEntity.defaultProps = {
    transform: (entity: any) => entity
}