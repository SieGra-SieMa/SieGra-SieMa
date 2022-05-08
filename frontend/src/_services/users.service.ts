import Config from '../config.json';
import { User, UserDetailsRequest } from '../_lib/types';
import { get, patch } from '.';

const usersService = {
    updateUser,
    currentUser,
};

export default usersService;

function updateUser(user: UserDetailsRequest): Promise<{}> {
    return patch(`${Config.HOST}/api/users/change-details`, user);
};

function currentUser(): Promise<User> {
    return get(`${Config.HOST}/api/users/current`);
};
