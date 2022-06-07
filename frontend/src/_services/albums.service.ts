import Config from '../config.json';
import { Album, Media } from '../_lib/types';
import Service from './service';

export default class AlbumsService extends Service {

    getAlbumWithMedia(id: string): Promise<Album> {
        return super.get(`${Config.HOST}/api/albums/${id}`);
    };

    deleteAlbum(id: string): Promise<{}> {
        return super.del(`${Config.HOST}/api/albums/${id}`);
    }

    addMediaToAlbum(id: string, files: FormData): Promise<Media>{
        return super.post(`${Config.HOST}/api/albums/${id}/media`, files);
    }

}
