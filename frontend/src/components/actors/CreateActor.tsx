import React from 'react';
import ActorForm from './ActorForm';

const CreateActor = () => {
    return (
        <div className='container'>
            <h3>Create actors</h3>
            <ActorForm
                model={{ name: '', dateOfBirth: undefined }}
                onSubmit={values => console.log(values)}
            />
        </div>
    );
};

export default CreateActor;