import Config from '../config.json';
import { UserDetailsRequest } from '../_lib/types';
import { patch } from '../_lib/utils';

export const usersService = {
    update,
};

function update(user: UserDetailsRequest): Promise<{}> {
    return patch(`${Config.HOST}/api/users/change-details`, user);
};
