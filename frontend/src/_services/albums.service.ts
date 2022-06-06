import Config from '../config.json';
import { Album } from '../_lib/types';
import Service from './service';

export default class AlbumsService extends Service {

    getAlbumWithMedia(id: string): Promise<Album> {
        return super.get(`${Config.HOST}/api/albums/${id}`);
    };

}
