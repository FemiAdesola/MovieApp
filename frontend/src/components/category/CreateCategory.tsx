import React from 'react';

import Button from '../features/Button';
import { useNavigate } from 'react-router-dom';

const CreateCategory = () => {
    const navigate = useNavigate();
    return (
        <div className='container'>
            <h3>Create</h3>
            <Button onClick={() => {
                navigate('/categories')
            }}>Save changes</Button>
        </div>
    );
};

export default CreateCategory;