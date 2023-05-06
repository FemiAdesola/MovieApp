import React from "react";
import { RecordsPerPageSelectionProps } from "../types/pagination";

const RecordsPerPageSelection = (props: RecordsPerPageSelectionProps) => {
  return (
    <div className="mb-3" style={{ width: "150px" }}>
      <label>Records per page</label>
      <select
        className="form-select"
        defaultValue={5}
        onChange={(e) => {
          props.onChange(parseInt(e.currentTarget.value, 10));
        }}
      >
        <option value={5}>5</option>
        <option value={10}>10</option>
        <option value={20}>20</option>
        <option value={50}>50</option>
      </select>
    </div>
  );
};

export default RecordsPerPageSelection;
