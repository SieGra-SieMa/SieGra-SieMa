import { BehaviorSubject } from 'rxjs';
import Config from '../config.json';
import { Account } from '../_lib/types';
import { handleResponse } from '../_lib/_utils/handle-response';

const currentUserSubject = new BehaviorSubject<Account | null>(
    localStorage.getItem('currentUser') && JSON.parse(localStorage.getItem('currentUser')!)
);

export const authenticationService = {
    register,
    authenticate,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value },
    setCurrentUser,
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

function authenticate(email: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password, name: "1", surname: "1" })
    }

    return fetch(`${Config.HOST}/api/accounts/authenticate`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('currentUser', JSON.stringify(user))
            currentUserSubject.next(user)

            return user
        })
};

function logout() {
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
};

function setCurrentUser(user: Account) {
    localStorage.setItem('currentUser', JSON.stringify(user));
    currentUserSubject.next(user);
}
