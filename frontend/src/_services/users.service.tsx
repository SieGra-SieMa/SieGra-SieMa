import Config from '../config.json';
import { handleResponse } from '../_lib/_utils/handle-response';
import { authHeader } from '../_lib/_utils/auth-header';
import { UserDetailsRequest } from '../_lib/types';

export const usersService = {
    update,
};

function update(user: UserDetailsRequest): Promise<Response> {
    const requestOptions = {
        method: 'PATCH',
        headers: { 
            'Content-Type': 'application/json', 
            ...authHeader(),
        },
        body: JSON.stringify(user)
    }

    return fetch(`${Config.HOST}/api/users/change-details`, requestOptions)
        .then(handleResponse)
};
