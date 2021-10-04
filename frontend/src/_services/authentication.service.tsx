import { BehaviorSubject } from 'rxjs';
import Config from '../config.json';
import { handleResponse } from '../_lib/_utils/handle-response';

const currentUserSubject = new BehaviorSubject(
    localStorage.getItem('currentUser') && JSON.parse(localStorage.getItem('currentUser')!)
);

export const authenticationService = {
    register,
    authorize,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function register(name: string, surname: string, email: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, surname, email, password })
    }

    return fetch(`${Config.HOST}/api/accounts/create`, requestOptions)
        .then(handleResponse)
};

function authorize(email: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
    }

    return fetch(`${Config.HOST}/api/accounts/authorize`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('currentUser', JSON.stringify(user))
            currentUserSubject.next(user)

            return user
        })
};

function logout() {
    localStorage.removeItem('currentUser')
    currentUserSubject.next(null)
};
