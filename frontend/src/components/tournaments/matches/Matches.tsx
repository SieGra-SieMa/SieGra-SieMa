import { useState } from "react";
import { Group } from "../../../_lib/_types/tournament";
import Button, { ButtonStyle } from "../../form/Button";
import Modal from "../../modal/Modal";
import Match from "./Match";
import styles from "./Matches.module.css";

type MatchesProps = {
	groups: Group[];
};

export default function Matches({ groups }: MatchesProps) {
	const [currentGroup, setCurrentGroup] = useState<Group>(groups[0]);
	const [isSelectGroup, setIsSelectGroup] = useState(false);

	return (
		<>
			<div className={styles.root}>
				<div className={styles.container}>
					<h4 className="underline" style={{ width: "fit-content" }}>
						Mecze
					</h4>
					<Button
						id={styles.selectGroupButton}
						value={"Grupa " + currentGroup.name}
						onClick={() => setIsSelectGroup(true)}
						style={ButtonStyle.TransparentBorder}
					/>
					{groups && isSelectGroup && (
						<Modal
							title="Wybierz grupę"
							isClose
							close={() => setIsSelectGroup(false)}
						>
							<div className={styles.selectGroup}>
								{groups
									.filter((group) => !group.ladder)
									.map((group) => (
										<Button
											value={"Grupa " + group.name}
											className={
												group === currentGroup
													? styles.active
													: ""
											}
											onClick={() => {
												setCurrentGroup(group);
												setIsSelectGroup(false);
											}}
											key={group.id}
											style={ButtonStyle.TransparentBorder}
										/>
									))}
							</div>
						</Modal>
					)}
					<div className={styles.group}>
						<div className={styles.matches}>
							{currentGroup.matches &&
								currentGroup.matches.map((match) => (
									<Match key={match.matchId} match={match} />
								))}
						</div>
					</div>
				</div>
			</div>
		</>
	);
}
