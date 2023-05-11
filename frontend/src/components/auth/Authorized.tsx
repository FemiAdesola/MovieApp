import React, { useContext, useEffect, useState } from 'react';
import { AuthorizedProps } from './auth';
import AuthenticationContext from './AuthenticationContext';

const Authorized = (props: AuthorizedProps) => {
    const [isAuthorized, setIsAuthorized] = useState(true);
    const { claims } = useContext(AuthenticationContext);

     useEffect(() => {
        if (props.role){
            const index = claims.findIndex(claim => 
                claim.name === 'role' && claim.value === props.role)
            setIsAuthorized(index > -1);
        } else {
            setIsAuthorized(claims.length > 0);
        }
     }, [claims, props.role]);
    
    return (
        <div>
            {isAuthorized ? props.authorized : props.notAuthorized}
        </div>
    );
};

export default Authorized;