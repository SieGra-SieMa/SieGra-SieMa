import Config from '../config.json';
import { UserDetailsRequest } from '../_lib/types';
import { patch } from '.';

const usersService = {
    update,
};

export default usersService;

function update(user: UserDetailsRequest): Promise<{}> {
    return patch(`${Config.HOST}/api/users/change-details`, user);
};
