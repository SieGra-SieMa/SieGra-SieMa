import Config from "../../../config.json";
import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ROLES } from "../../../_lib/roles";
import {
	TeamInTournament,
	Tournament as TournamentType,
} from "../../../_lib/_types/tournament";
import { useApi } from "../../api/ApiContext";
import Button, { ButtonStyle } from "../../form/Button";
import GuardComponent from "../../guard-components/GuardComponent";
import Ladder from "../ladder/Ladder";
import Confirm from "../../modal/Confirm";
import Modal from "../../modal/Modal";
import EditTournament from "./EditTournament";
import { useTournaments } from "../TournamentsContext";
import styles from "./Tournament.module.css";
import { TournamentContext } from "../TournamentContext";
import EditTournamentPicture from "./EditTournamentPicture";
import Groups from "../groups/Groups";
import TeamAssign from "../list/TeamAssign";
import { useAuth } from "../../auth/AuthContext";
import { SyncLoader } from "react-spinners";
import TeamsList from "../teams/TeamsList";
import Matches from "../matches/Matches";
import ImageIcon from "@mui/icons-material/Image";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import InsertPhotoIcon from "@mui/icons-material/InsertPhoto";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import PlayArrowIcon from "@mui/icons-material/PlayArrow";
import LocationOnOutlinedIcon from "@mui/icons-material/LocationOnOutlined";
import Contests from "../contests/Contests";
import CreateAlbum from "../../gallery/CreateAlbum";

export default function Tournament() {
	const navigate = useNavigate();

	const { id } = useParams<{ id: string }>();

	const { tournamentsService } = useApi();
	const { session } = useAuth();
	const { tournaments, setTournaments } = useTournaments();

	const [tournament, setTournament] = useState<TournamentType | null>(() => {
		const tournament = tournaments?.find(
			(tournament) => tournament.id === parseInt(id!)
		);
		if (!tournament) return null;
		return {
			...tournament,
			contests: [],
			albums: [],
			groups: [],
			ladder: [],
		};
	});
	const [isLoading, setIsLoading] = useState(true);
	const [teams, setTeams] = useState<TeamInTournament[] | null>(null);
	const [isPrepare, setIsPrepare] = useState(false);
	const [isReset, setIsReset] = useState(false);
	const [isEdit, setIsEdit] = useState(false);
	const [isDelete, setIsDelete] = useState(false);
	const [isPicture, setIsPicture] = useState(false);
	const [isTeamAssign, setIsTeamAssign] = useState(false);
	const [isTeamRemove, setIsTeamRemove] = useState(false);
	const [isLadderCompose, setIsLadderCompose] = useState(false);
	const [isLadderReset, setIsLadderReset] = useState(false);
	const [isAddAlbum, setIsAddAlbum] = useState(false);
	const isOpen = tournament?.isOpen;

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	useEffect(() => {
		tournamentsService.getTournamentById(id!).then((data) => {
			setTournament(data);
			setIsLoading(false);
		});
	}, [id, tournamentsService]);

	useEffect(() => {
		if (!isOpen) return;

		tournamentsService.getTeamsInTournament(id!).then((data) => {
			setTeams(data);
		});
	}, [isOpen, id, tournamentsService]);

	const prepareTournament = () => {
		tournamentsService.prepareTournamnet(id!).then((data) => {
			setTournament(data);
			setIsPrepare(false);
		});
	};

	const resetTournament = () => {
		tournamentsService.resetTournament(id!).then((data) => {
			setTournament(data);
			setIsReset(false);
		});
	};

	const editTournament = useCallback(
		(updatedTournament: TournamentType) => {
			setTournament(updatedTournament);
			setIsEdit(false);
			if (tournaments) {
				const index = tournaments!.findIndex(
					(e) => e.id === updatedTournament.id
				);
				const data = [...tournaments];
				data[index] = updatedTournament;
				setTournaments(data);
			}
		},
		[tournaments, setTournaments]
	);

	const deleteTournament = useCallback(() => {
		tournamentsService.deleteTournament(id!).then((data) => {
			setIsDelete(false);
			navigate("..");
			if (tournaments) {
				const updatedTournaments = tournaments.filter(
					(tournament) => tournament.id !== parseInt(id!)
				);
				setTournaments(updatedTournaments);
			}
		});
	}, [id, tournaments, setTournaments, tournamentsService, navigate]);

	const removeTeam = (teamId: number) => () => {
		tournamentsService.removeTeam(id!, teamId).then(() => {
			setIsTeamRemove(false);
			const updatedTournament = {
				...tournament!,
				team: null,
			};
			setTournament(updatedTournament);
			if (tournaments) {
				const index = tournaments!.findIndex(
					(e) => e.id === updatedTournament.id
				);
				const data = [...tournaments];
				data[index] = updatedTournament;
				setTournaments(data);
			}
			setTeams(teams && teams.filter((team) => team.teamId !== teamId));
		});
	};

	const composeLadder = () => {
		tournamentsService.composeLadder(id!).then((data) => {
			setTournament(data);
			setIsLadderCompose(false);
		});
	};

	const resetLadder = () => {
		tournamentsService.resetLadder(id!).then((data) => {
			setTournament(data);
			setIsLadderReset(false);
		});
	};

	return (
		<TournamentContext.Provider
			value={{ tournament, setTournament, teams, setTeams }}
		>
			<div className={styles.top}>
				<ArrowBackIosNewIcon
					className="interactiveIcon"
					onClick={() => navigate("..")}
					fontSize="large"
				/>
				{tournament && (
					<GuardComponent roles={[ROLES.Admin]}>
						<div className={styles.adminControls}>
							{isOpen ? (
								<PlayArrowIcon
									className="interactiveIcon"
									onClick={() => setIsPrepare(true)}
									fontSize="medium"
								/>
							) : (
								<RestartAltIcon
									className="interactiveIcon"
									onClick={() => setIsReset(true)}
									fontSize="medium"
								/>
							)}
							<InsertPhotoIcon
								className="interactiveIcon"
								onClick={() => setIsPicture(true)}
								fontSize="medium"
							/>
							<EditIcon
								className="interactiveIcon"
								onClick={() => setIsEdit(true)}
								fontSize="medium"
							/>
							<DeleteIcon
								className="interactiveIcon"
								onClick={() => setIsDelete(true)}
								fontSize="medium"
							/>
						</div>
					</GuardComponent>
				)}
			</div>

			{tournament && (
				<div>
					<h1 className={styles.title}>{tournament.name}</h1>
					<h4 className={styles.dates}>
						{new Date(tournament.startDate).toLocaleString()}
						<span className={styles.line}> | </span>
						{new Date(tournament.endDate).toLocaleString()}
					</h4>
					<div className={styles.details}>
						<div className={styles.address}>
							<LocationOnOutlinedIcon
								fontSize="medium"
								style={{ color: "var(--accent-color)" }}
							/>
							<p>{tournament.address}</p>
						</div>
						<br />
						<div className={styles.tournamentInfo}>
							<div className={styles.description}>
								<h4 className="underline">Opis</h4>
								<div
									dangerouslySetInnerHTML={{
										__html: `${tournament.description}`,
									}}
								></div>
							</div>
							<div>
								{isOpen && <TeamsList teams={teams} />}
								{tournament &&
									!isOpen &&
									tournament.groups.length > 1 && (
										<Groups groups={tournament.groups} />
									)}
							</div>
						</div>

						{session &&
							tournament.isOpen &&
							(tournament.team ? (
								<div className={styles.team}>
									<h6>{tournament.team.name}</h6>
									<Button
										value="Usuń zespół"
										onClick={() => setIsTeamRemove(true)}
										style={ButtonStyle.Red}
									/>
								</div>
							) : (
								<Button
									value="Zapisz zespół"
									onClick={() => setIsTeamAssign(true)}
								/>
							))}
					</div>
				</div>
			)}

			{!isOpen &&
				(isLoading ? (
					<div className={styles.loader}>
						<SyncLoader
							loading={true}
							size={20}
							margin={20}
							color="#fff"
						/>
					</div>
				) : (
					<>
						{tournament && (
							<>
								<div className={styles.matchesAndContests}>
									<div className={styles.matchesContainer}>
										{!isOpen && (
											<>
												{tournament.groups.length >
													1 && (
													<>
														<Matches
															groups={
																tournament.groups
															}
														/>
													</>
												)}
											</>
										)}
									</div>
									<div className={styles.contestsContainer}>
										<Contests
											contests={tournament.contests}
											tournamentId={id!}
										/>
									</div>
								</div>
							</>
						)}
						{tournament && !isOpen && (
							<>
								{tournament.ladder[0]?.matches[0].teamHome && (
									<Ladder ladder={tournament.ladder} />
								)}
								<GuardComponent roles={[ROLES.Admin]}>
									{tournament &&
										tournament.groups
											.map(
												(group) =>
													group.matches
														?.map(
															(e) =>
																e.teamAwayScore !==
																	null &&
																e.teamHomeScore !==
																	null
														)
														.every((e) => e) ?? true
											)
											.every((e) => e) &&
										(tournament.ladder[0]?.matches[0]
											.teamHome ? (
											<div
												className={
													styles.ladderControls
												}
											>
												<Button
													value="Zresetuj drabinke"
													onClick={() =>
														setIsLadderReset(true)
													}
													style={ButtonStyle.Red}
												/>
											</div>
										) : (
											<div
												className={
													styles.ladderControls
												}
											>
												<Button
													value="Zbuduj drabinke"
													onClick={() =>
														setIsLadderCompose(true)
													}
												/>
											</div>
										))}
								</GuardComponent>
							</>
						)}
					</>
				))}

			{tournament && (
				<div>
					<div className={styles.header}>
						<h4 className="underline">Albumy</h4>
						<GuardComponent roles={[ROLES.Admin]}>
							<Button
								value="Dodaj album"
								onClick={() => setIsAddAlbum(true)}
							/>
						</GuardComponent>
					</div>
					<ul className={styles.albums}>
						{tournament.albums.map((album, index) => (
							<li
								key={index}
								className={styles.item}
								style={
									album.profilePicture
										? {
												backgroundImage: `url(${Config.HOST}${album.profilePicture})`,
										  }
										: undefined
								}
								onClick={() =>
									navigate(
										`/gallery/${id!}/albums/${album.id}`
									)
								}
							>
								<div className={styles.box}>
									{!album.profilePicture && (
										<ImageIcon className={styles.picture} />
									)}
									<h4>{album.name}</h4>
								</div>
							</li>
						))}
					</ul>
				</div>
			)}

			{tournament && isPrepare && (
				<Modal
					isClose
					close={() => setIsPrepare(false)}
					title={`Czy na pewno chcesz przygotuj turniej`}
				>
					<Confirm
						cancel={() => setIsPrepare(false)}
						confirm={prepareTournament}
						label="Potwierdź"
						style={ButtonStyle.Yellow}
					/>
				</Modal>
			)}
			{tournament && isReset && (
				<Modal
					isClose
					close={() => setIsReset(false)}
					title={`Czy na pewno chcesz zresetować turniej?"`}
				>
					<Confirm
						cancel={() => setIsReset(false)}
						confirm={resetTournament}
						label="Potwierdź"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{tournament && isEdit && (
				<Modal
					isClose
					close={() => setIsEdit(false)}
					title={`Edytuj turniej  - "${tournament.name}"`}
				>
					<EditTournament
						tournament={tournament}
						confirm={editTournament}
					/>
				</Modal>
			)}
			{tournament && isDelete && (
				<Modal
					close={() => setIsDelete(false)}
					title='Czy na pewno chcesz usunąć turniej"?'
				>
					<Confirm
						cancel={() => setIsDelete(false)}
						confirm={deleteTournament}
						label="Usuń"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{tournament && isPicture && (
				<Modal
					isClose
					close={() => setIsPicture(false)}
					title="Edytuj zdjęcie profilowe"
				>
					<EditTournamentPicture
						tournament={tournament}
						confirm={(url) => {
							setIsPicture(false);
							const updatedTournament = {
								...tournament,
								profilePicture: url,
							};
							setTournament(updatedTournament);
							setIsEdit(false);
							if (tournaments) {
								const index = tournaments!.findIndex(
									(e) => e.id === updatedTournament.id
								);
								const data = [...tournaments];
								data[index] = updatedTournament;
								setTournaments(data);
							}
						}}
					/>
				</Modal>
			)}
			{session &&
				tournament &&
				tournament.isOpen &&
				!tournament.team &&
				isTeamAssign && (
					<Modal
						title="Zapisz zespół"
						isClose
						close={() => setIsTeamAssign(false)}
					>
						<TeamAssign
							id={tournament.id}
							confirm={(team) => {
								setIsTeamAssign(false);
								const updatedTournament = {
									...tournament,
									team,
								};
								setTournament(updatedTournament);
								if (tournaments) {
									const index = tournaments!.findIndex(
										(e) => e.id === updatedTournament.id
									);
									const data = [...tournaments];
									data[index] = updatedTournament;
									setTournaments(data);
								}
								const newTeams: TeamInTournament[] = [];
								if (teams) {
									newTeams.push(...teams);
								}
								newTeams.push({
									teamId: team.id,
									tournamentId: tournament.id,
									teamName: team.name,
									teamProfileUrl: team.profilePicture,
									paid: false,
								});
								setTeams(newTeams);
							}}
						/>
					</Modal>
				)}
			{session &&
				tournament &&
				tournament.isOpen &&
				tournament.team &&
				isTeamRemove && (
					<Modal
						title={`Czy na pewno chcesz usunąć zespół - "${tournament.team.name}"?`}
						isClose
						close={() => setIsTeamRemove(false)}
					>
						<Confirm
							confirm={removeTeam(tournament.team.id)}
							cancel={() => setIsTeamRemove(false)}
							label="Usuń"
							style={ButtonStyle.Red}
						/>
					</Modal>
				)}
			{isLadderCompose && (
				<Modal
					title="Czy na pewno chcesz zbudować drabinke?"
					isClose
					close={() => setIsLadderCompose(false)}
				>
					<Confirm
						confirm={composeLadder}
						cancel={() => setIsLadderCompose(false)}
						label="Potwierdź"
						style={ButtonStyle.Yellow}
					/>
				</Modal>
			)}
			{isLadderReset && (
				<Modal
					title="Czy na pewno chcesz zresetować drabinke?"
					isClose
					close={() => setIsLadderReset(false)}
				>
					<Confirm
						confirm={resetLadder}
						cancel={() => setIsLadderReset(false)}
						label="Potwierdź"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}
			{tournament && isAddAlbum && (
				<Modal
					title="Dodaj album"
					close={() => setIsAddAlbum(false)}
					isClose
				>
					<CreateAlbum
						tournamentId={id!}
						confirm={(album) => {
							setTournament({
								...tournament,
								albums: tournament.albums.concat(album),
							});
							setIsAddAlbum(false);
						}}
					/>
				</Modal>
			)}
		</TournamentContext.Provider>
	);
}
