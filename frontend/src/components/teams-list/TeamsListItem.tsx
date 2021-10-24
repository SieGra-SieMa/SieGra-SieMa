import React from 'react';
import './TeamsListItem.css';
import { Team } from '../../_lib/types';

type TeamsListItemProp = {
    team: Team
}

export default function TeamsListItem({ team }: TeamsListItemProp) {
    return (
        <div className="teams-list-item-block">
            <div className="teams-list-item-header">
                <h3>{ team.name }</h3>
                <div className="code-block">
                    <span>Code: </span>
                    <h3>{ team.code }</h3>
                </div>
            </div>
            <div className="teams-list-item-content">
                <p>Participants</p>
                <ul className="participants-list">
                    {
                        team.players.map((player, index) => (
                            <li className="participants-list-item" key={index}>
                                <img src="/placeholder.png" alt="" />
                                <p>{`${player.user.name} ${player.user.surname}`}</p>
                            </li>
                        ))
                    }
                </ul>
            </div>
            <div className="teams-list-item-footer">
                <div className="exit-button">
                    EXIT
                </div>
            </div>
        </div>
    );
}
