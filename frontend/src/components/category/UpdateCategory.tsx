import React from 'react';
import { useParams } from 'react-router-dom';

const UpdateCategory = () => {
    const { id }: any = useParams();
    return (
        <div className='container'>
            <h3>Update</h3>
            The id is {id}
        </div>
    );
};

export default UpdateCategory;