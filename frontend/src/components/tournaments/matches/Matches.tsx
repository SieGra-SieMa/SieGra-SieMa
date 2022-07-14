import { useState } from "react";
import { Group } from "../../../_lib/_types/tournament";
import Button, { ButtonStyle } from "../../form/Button";
import Modal from "../../modal/Modal";
import Match from "./Match";
import styles from "./Matches.module.css";


type Props = {
	groups: Group[];
};

export default function Matches({ groups }: Props) {

	const [currentGroupId, setCurrentGroupId] = useState<number | null>(null);
	const [isSelectGroup, setIsSelectGroup] = useState(false);

	const currentGroup = groups.find((g) => currentGroupId === null || g.id === currentGroupId);

	return (
		<>
			<div className={styles.root}>
				<div className={styles.container}>
					<h4
						className="underline"
						style={{
							width: "fit-content",
							padding: 0,
							marginTop: "25px",
						}}
					>
						Mecze
					</h4>
					{currentGroup && (
						<Button
							id={styles.selectGroupButton}
							value={"Grupa " + currentGroup.name}
							onClick={() => setIsSelectGroup(true)}
							style={ButtonStyle.TransparentBorder}
						/>
					)}
					{groups && isSelectGroup && (
						<Modal
							title="Wybierz grupę"
							isClose
							close={() => setIsSelectGroup(false)}
						>
							<div className={styles.selectGroup}>
								{groups
									.filter((group) => group.matches)
									.map((group) => (
										<Button
											value={"Grupa " + group.name}
											className={
												group === currentGroup
													? styles.active
													: ""
											}
											onClick={() => {
												setCurrentGroupId(group.id);
												setIsSelectGroup(false);
											}}
											key={group.id}
											style={
												ButtonStyle.TransparentBorder
											}
										/>
									))}
							</div>
						</Modal>
					)}
					<div className={styles.group}>
						<div className={styles.matches}>
							{currentGroup && currentGroup.matches &&
								currentGroup.matches.map((match) => (
									<Match key={`${match.groupId}-${match.matchId}`} match={match} />
								))}
						</div>
					</div>
				</div>
			</div>
		</>
	);
}
