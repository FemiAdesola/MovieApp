import React from 'react';

import css from '../movies/Movies.module.css'

const Loading = () => {
    return (
      <div className={css.loading}>
        <img
          src="https://i.pinimg.com/originals/b4/d8/f7/b4d8f79fc22824362a704f11096d8658.gif"
          alt="loading"
        />
      </div>
    );
};

export default Loading;