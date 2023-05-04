import React from "react";

import '../../CSS/MultipleSelectorField.css'
import {
  MultipleSelectorFieldProps,
  MultipleSelectorModel,
} from "../../types/field";

const MuiltipleSelectorField = (props: MultipleSelectorFieldProps) => {
  const select = (item: MultipleSelectorModel) => {
    const selected = [...props.selected, item];
    const nonSelected = props.nonSelected.filter((value) => value !== item);
    props.onChange(selected, nonSelected);
  };

  const deselect = (item: MultipleSelectorModel) => {
    const nonSelected = [...props.nonSelected, item];
    const selected = props.selected.filter((value) => value !== item);
    props.onChange(selected, nonSelected);
  };

  const selectAll = () => {
    const selected = [...props.selected, ...props.nonSelected];
    const nonSelected: MultipleSelectorModel[] = [];
    props.onChange(selected, nonSelected);
  };

  const deselectAll = () => {
    const nonSelected = [...props.nonSelected, ...props.selected];
    const selected: MultipleSelectorModel[] = [];
    props.onChange(selected, nonSelected);
  };
  return (
    <div className="mb-3">
      <label>{props.displayName}</label>
      <div className="multiple-selector">
        <ul>
          {props.nonSelected.map((item) => (
            <li key={item.key} onClick={() => select(item)}>
              {item.value}
            </li>
          ))}
        </ul>
        <div className="multiple-selector-buttons">
          <button type="button" onClick={selectAll}>
            {">>"}
          </button>
          <button type="button" onClick={deselectAll}>
            {"<<"}
          </button>
        </div>
        <ul>
          {props.selected.map((item) => (
            <li key={item.key} onClick={() => deselect(item)}>
              {item.value}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default MuiltipleSelectorField;
