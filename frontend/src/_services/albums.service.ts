import Config from '../config.json';
import { Media } from '../_lib/_types/response';
import { Album } from '../_lib/_types/tournament';
import Service from './service';

export default class AlbumsService extends Service {

    getAlbumWithMedia(id: string): Promise<Album> {
        return super.get(`${Config.HOST}/api/albums/${id}`);
    };

    deleteAlbum(id: string): Promise<{}> {
        return super.del(`${Config.HOST}/api/albums/${id}`);
    }

    addMediaToAlbum(id: number, data: FormData): Promise<Media> {
        const headers = new Headers();
        return super.post(`${Config.HOST}/api/albums/${id}/media`, data, headers, false);
    }

    editAlbum(id: number, album: Album): Promise<Album> {
        return super.patch(`${Config.HOST}/api/albums/${id}`, album);
    }
}
