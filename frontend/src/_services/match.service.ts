import Config from '../config.json';
import { Match, MatchResult } from '../_lib/types';
import Service from './service';

export default class MatchService extends Service {

    insertResults(result: MatchResult): Promise<Match> {
        return super.patch(`${Config.HOST}/api/match/insertMatchResults`, result);
    };
}
