import React, { useContext, useEffect, useState } from "react";

import AuthenticationContext from "../auth/AuthenticationContext";
import { RatingsProps } from "./rating";
import Swal from "sweetalert2";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import './Rating.css'

const Ratings = (props: RatingsProps) => {
  const [maximumValueArr, setMaximumValueArr] = useState<number[]>([]);
  const [selectedValue, setSelectedValue] = useState(props.selectedValue);
  const { claims } = useContext(AuthenticationContext);

  useEffect(() => {
    setMaximumValueArr(Array(props.maximumValue).fill(0));
  }, [props.maximumValue]);

const  handleMouseOver= (rate: number) =>{
    setSelectedValue(rate);
  }

const  handleClick= (rate: number)=> {
    const userIsLoggedIn = claims.length > 0;
    if (!userIsLoggedIn) {
      Swal.fire({
        title: "Error",
        text: "You need to register or login",
        icon: "error",
      });
      return;
    }

    setSelectedValue(rate);
    props.onChange(rate);
  }
    return (
      <>
        {maximumValueArr.map((_, index) => (
          <FontAwesomeIcon
            onMouseOver={() => handleMouseOver(index + 1)}
            onClick={() => handleClick(index + 1)}
            icon="star"
            key={index}
            className={`fa-lg pointer ${
              selectedValue >= index + 1 ? "checked" : null
            }`}
          />
        ))}
      </>
    );
};

export default Ratings;
