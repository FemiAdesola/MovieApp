import React, { useEffect, useState } from "react";
import { LinkModel, PaginationProps } from "../types/pagination";

const Pagination = (props: PaginationProps) => {
  const [linkModels, setLinkModels] = useState<LinkModel[]>([]);

  const selectPage =(link: LinkModel) =>{
    if (link.page === props.currentPage) {
      return;
    }
    if (!link.enabled) {
      return;
    }
    props.onChange(link.page);
  }

 const getClass=(link: LinkModel) =>{
    if (link.active) {
      return "active pointer";
    }
    if (!link.enabled) {
      return "disabled";
    }
    return "pointer";
  }

  useEffect(() => {
    const prevPageEnabled = props.currentPage !== 1;
    const prevPage = props.currentPage - 1;
    const links: LinkModel[] = [];

    links.push({
      text: "Previous",
      enabled: prevPageEnabled,
      page: prevPage,
      active: false,
    });

    for (let i = 1; i <= props.totalAmountOfPages; i++) {
      if (
        i >= props.currentPage - props.radio &&
        i <= props.currentPage + props.radio
      ) {
        links.push({
          text: `${i}`,
          active: props.currentPage === i,
          enabled: true,
          page: i,
        });
      }
    }

    const nextPageEnabled =
      props.currentPage !== props.totalAmountOfPages &&
      props.totalAmountOfPages > 0;
    const nextPage = props.currentPage + 1;

    links.push({
      text: "Next",
      page: nextPage,
      enabled: nextPageEnabled,
      active: false,
    });

    setLinkModels(links);
  }, [props.currentPage, props.totalAmountOfPages, props.radio]);
  return (
    <nav>
      <ul className="pagination justify-content-center">
        {linkModels.map((link) => (
          <li
            key={link.text}
            onClick={() => selectPage(link)}
            className={`page-item cursor ${getClass(link)}`}
          >
            <span className="page-link">{link.text}</span>
          </li>
          
        ))}
        
      </ul>
    </nav>
  );
};

export default Pagination;

// Pagination.defaultProps = {
//     radio: 4
// }
