import Config from '../config.json';
import { Message } from '../_lib/_types/response';
import Service from './service';

export default class MediaService extends Service {

    deleteMedia(id: number): Promise<Message> {
        return super.del(`${Config.HOST}/api/media/${id}`);
    }
}
