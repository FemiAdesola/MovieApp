import React, { ChangeEvent, useState } from 'react';

import { useFormikContext } from 'formik';
import { Base64 } from '../Base64';
import { ImageFieldProps } from '../../types/field';

const ImageField = (props: ImageFieldProps) => {
    const [imageBase64, setImageBase64] = useState("");
    const [actorImageURL, setActorImageURL] = useState(props.imageURL);
    const { values } = useFormikContext<any>();

    const handleOnChange = (eventsArgs: ChangeEvent<HTMLInputElement>) => {
      if (eventsArgs.currentTarget.files) {
        const file = eventsArgs.currentTarget.files[0];
        if (file) {
          Base64(file)
            .then((base64Representation: string) =>
              setImageBase64(base64Representation)
            )
            .catch((error) => console.error(error));
          values[props.field] = file;
          setActorImageURL("");
        } else {
          setImageBase64("");
        }
      }
    };

    return (
      <div className="mb-3">
        <label>{props.displayName}</label>
        <div>
          <input
            type="file"
            accept=".jpg,.jpeg,.png"
            onChange={handleOnChange}
          />
        </div>
        {imageBase64 ? (
          <div>
            <div className="image">
              <img src={imageBase64} alt="selected" />
            </div>
          </div>
        ) : null}
        {actorImageURL ? (
          <div>
            <div className="image">
              <img src={actorImageURL} alt="selected" />
            </div>
          </div>
        ) : null}
      </div>
    );
};

export default ImageField;

ImageField.defaultProps = {
  imageURL: "",
};