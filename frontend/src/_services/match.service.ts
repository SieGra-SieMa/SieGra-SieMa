import Config from '../config.json';
import { Ladder, MatchResult } from '../_lib/_types/tournament';
import Service from './service';

export default class MatchService extends Service {

    insertResults(result: MatchResult): Promise<Ladder> {
        return super.patch(`${Config.HOST}/api/match/insertMatchResults`, result);
    };
}
