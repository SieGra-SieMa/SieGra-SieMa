import { BehaviorSubject } from 'rxjs';
import Config from '../config.json';
import { Account, Tokens } from '../_lib/types';
import { post } from '../_lib/_utils/utils';

const currentUserSubject = new BehaviorSubject<Account | null>(
    localStorage.getItem('currentUser') && JSON.parse(localStorage.getItem('currentUser') ?? 'null')
);

export const authenticationService = {
    register,
    authenticate,
    logout,
    refresh,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value },
    setCurrentUser,
};

function register(name: string, surname: string, email: string, password: string): Promise<{}> {
    return post(`${Config.HOST}/api/accounts/create`, { name, surname, email, password });
};

function authenticate(email: string, password: string): Promise<Account>  {
    return post<Account>(`${Config.HOST}/api/accounts/authenticate`, { email, password, name: "1", surname: "1" })
        .then(user => {
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            return user;
        })
};

function refresh(refreshToken: string): Promise<Tokens>  {
    return post<Tokens>(`${Config.HOST}/api/accounts/refresh-token`, { refreshToken })
        .then(tokens => {
            localStorage.setItem('currentUser', JSON.stringify({...currentUserSubject.value, ...tokens}));
            return tokens;
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
