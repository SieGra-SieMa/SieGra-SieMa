import Config from '../config.json';
import { handleResponse } from '../_lib/_utils/handle-response';
import { authHeader } from '../_lib/_utils/auth-header';

export const teamsService = {
    join,
    create,
    get
};

function join(code: string): Promise<Response> {
    const requestOptions = {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json', 
            ...authHeader() 
        },
        body: JSON.stringify({ 
            code
        })
    }

    return fetch(`${Config.HOST}/api/teams/join`, requestOptions)
        .then(handleResponse)
};

function create(name: string): Promise<Response> {
    const requestOptions = {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json', 
            ...authHeader() 
        },
        body: JSON.stringify({ 
            name
        })
    }

    return fetch(`${Config.HOST}/api/teams/`, requestOptions)
        .then(handleResponse)
};

function get() {
    const requestOptions = {
        headers: { 
            ...authHeader() 
        }
    }

    return fetch(`${Config.HOST}/api/teams/`, requestOptions)
        .then(handleResponse)
};
