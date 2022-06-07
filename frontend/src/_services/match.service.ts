import Config from '../config.json';
import { MatchResult, Phase } from '../_lib/_types/tournament';
import Service from './service';

export default class MatchService extends Service {

    insertResults(result: MatchResult): Promise<Phase[]> {
        return super.patch(`${Config.HOST}/api/match/insertMatchResults`, result);
    };
}
