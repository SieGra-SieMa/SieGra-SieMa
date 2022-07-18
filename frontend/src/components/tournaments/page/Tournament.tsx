import Config from "../../../config.json";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ROLES } from "../../../_lib/roles";
import { Tournament as TournamentType } from "../../../_lib/_types/tournament";
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
import AddIcon from '@mui/icons-material/Add';
import { useAlert } from "../../alert/AlertContext";
import { useUser } from "../../user/UserContext";
import { Team } from "../../../_lib/types";
import Loader from "../../loader/Loader";
import { useAuth } from "../../auth/AuthContext";

export default function Tournament() {

	const navigate = useNavigate();

	const { id } = useParams<{ id: string }>();

	const alert = useAlert();
	const { teamsService, tournamentsService } = useApi();
	const { session } = useAuth();
	const { user } = useUser();
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
			teams: [],
			ladder: [],
		};
	});
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
	const [teams, setTeams] = useState<Team[] | null>(null);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	useEffect(() => {
		if (!session) return;
		teamsService.getTeamsIAmCaptain()
			.then((data) => {
				setTeams(data);
			});
	}, [session, teamsService]);

	useEffect(() => {
		if (!id || isNaN(parseInt(id))) return;
		tournamentsService.getTournamentById(id)
			.then((data) => {
				setTournament(data);
			});
	}, [id, tournamentsService]);

	if (!id || isNaN(parseInt(id))) {
		navigate('..');
		return null;
	};

	if (!tournament) {
		return (<>
			<div className={styles.top}>
				<ArrowBackIosNewIcon
					className="interactiveIcon"
					onClick={() => navigate('..')}
					fontSize="large"
				/>
			</div>
			<Loader size={20} margin={40} />
		</>);
	}

	const editTournament = (updatedTournament: TournamentType) => {
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
	};

	const deleteTournament = () => {
		return tournamentsService.deleteTournament(id)
			.then((data) => {
				setIsDelete(false);
				navigate('..');
				if (tournaments) {
					const updatedTournaments = tournaments.filter(
						(tournament) => tournament.id !== parseInt(id!)
					);
					setTournaments(updatedTournaments);
				}
				alert.success(data.message);
			});
	};

	const removeTeam = (teamId: number) => () => {
		return tournamentsService.removeTeam(id, teamId)
			.then(() => {
				setIsTeamRemove(false);
				if (!tournament) return;
				const updatedTournament = {
					...tournament,
					team: null,
					teams: tournament.teams.filter(
						(team) => team.teamId !== teamId
					),
				};
				setTournament(updatedTournament);
				if (!tournaments) return;
				const index = tournaments!.findIndex(
					(e) => e.id === updatedTournament.id
				);
				const data = [...tournaments];
				data[index] = updatedTournament;
				setTournaments(data);
			});
	};

	const composeLadder = () => {
		return tournamentsService.composeLadder(id!).then((data) => {
			setTournament(data);
			setIsLadderCompose(false);
		});
	};

	const resetLadder = () => {
		return tournamentsService.resetLadder(id).then((data) => {
			setTournament(data);
			setIsLadderReset(false);
		});
	};

	const prepareTournament = () => {
		return tournamentsService.prepareTournamnet(id)
			.then((data) => {
				setTournament(data);
				setIsPrepare(false);
			});
	};

	const resetTournament = () => {
		return tournamentsService.resetTournament(id)
			.then((data) => {
				setTournament(data);
				setIsReset(false);
			});
	};

	const prepareComponent = isOpen ? (<>
		<PlayArrowIcon
			className="interactiveIcon"
			onClick={() => setIsPrepare(true)}
			fontSize="medium"
		/>
		{isPrepare && (
			<Modal
				isClose
				close={() => setIsPrepare(false)}
				title="Czy na pewno chcesz przygotuj turniej?"
			>
				<Confirm
					cancel={() => setIsPrepare(false)}
					confirm={prepareTournament}
					label="Potwierdź"
					style={ButtonStyle.Yellow}
				/>
			</Modal>
		)}
	</>) : (<>
		<RestartAltIcon
			className="interactiveIcon"
			onClick={() => setIsReset(true)}
			fontSize="medium"
		/>
		{isReset && (
			<Modal
				isClose
				close={() => setIsReset(false)}
				title="Czy na pewno chcesz zresetować turniej?"
			>
				<Confirm
					cancel={() => setIsReset(false)}
					confirm={resetTournament}
					label="Potwierdź"
					style={ButtonStyle.Red}
				/>
			</Modal>
		)}
	</>);

	const pictureComponent = (<>
		<InsertPhotoIcon
			className="interactiveIcon"
			onClick={() => setIsPicture(true)}
			fontSize="medium"
		/>
		{isPicture && (
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
	</>);

	const editComponent = (<>
		<EditIcon
			className="interactiveIcon"
			onClick={() => setIsEdit(true)}
			fontSize="medium"
		/>
		{isEdit && (
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
	</>);

	const deleteComponent = (<>
		<DeleteIcon
			className="interactiveIcon"
			onClick={() => setIsDelete(true)}
			fontSize="medium"
		/>
		{isDelete && (
			<Modal
				close={() => setIsDelete(false)}
				title="Czy na pewno chcesz usunąć turniej?"
			>
				<Confirm
					cancel={() => setIsDelete(false)}
					confirm={deleteTournament}
					label="Usuń"
					style={ButtonStyle.Red}
				/>
			</Modal>
		)}
	</>);

	return (
		<TournamentContext.Provider value={{ tournament, setTournament }}>
			<div className={styles.top}>
				<ArrowBackIosNewIcon
					className="interactiveIcon"
					onClick={() => navigate('..')}
					fontSize="large"
				/>
				<GuardComponent roles={[ROLES.Admin]}>
					<div className={styles.adminControls}>
						{prepareComponent}
						{pictureComponent}
						{editComponent}
						{deleteComponent}
					</div>
				</GuardComponent>
			</div>
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
							style={{ color: 'var(--accent-color)' }}
						/>
						<p>{tournament.address}</p>
					</div>
					<div className={styles.tournamentInfo}>
						<div className={styles.description}>
							<h4 className="underline">Opis</h4>
							<div dangerouslySetInnerHTML={{
								__html: `${tournament.description}`,
							}}
							></div>
						</div>
						<div>
							{isOpen ? (
								<TeamsList teams={tournament.teams} />
							) : (
								tournament.groups.length > 0 && (
									<Groups tournament={tournament} />
								)
							)}
						</div>
					</div>
					{user && tournament.isOpen && tournament.team && tournament.team.captainId === user.id && (<>
						<div className={styles.team}>
							<h6>{tournament.team.name}</h6>
							<Button
								value="Usuń zespół"
								onClick={() => setIsTeamRemove(true)}
								style={ButtonStyle.Red}
							/>
						</div>
						{isTeamRemove && (
							<Modal
								isClose
								title={`Czy na pewno chcesz usunąć zespół - "${tournament.team.name}"?`}
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
					</>)}
					{user && tournament.isOpen && (!tournament.team) && teams && teams.length > 0 && (<>
						<Button
							value="Zapisz zespół"
							onClick={() => setIsTeamAssign(true)}
						/>
						{isTeamAssign && (
							<Modal
								title="Zapisz zespół"
								isClose
								close={() => setIsTeamAssign(false)}
							>
								<TeamAssign
									id={tournament.id}
									teams={teams}
									confirm={(team) => {
										setIsTeamAssign(false);
										const updatedTournament = {
											...tournament,
											team,
											teams: tournament.teams.concat({
												teamId: team.id,
												tournamentId: tournament.id,
												teamName: team.name,
												teamProfileUrl: team.profilePicture,
												paid: false,
											}),
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
									}}
								/>
							</Modal>
						)}
					</>)}
				</div>
			</div>

			<div className={styles.matchesAndContests}>
				<div className={styles.matchesContainer}>
					{!isOpen && tournament.groups.length > 1 && (
						<Matches groups={tournament.groups} />
					)}
				</div>
				<div className={styles.contestsContainer}>
					<Contests
						contests={tournament.contests}
						tournamentId={id}
					/>
				</div>
			</div>

			{!isOpen && (<>
				<div>
					{tournament.ladder[0]?.matches[0].teamHomeId && (
						<Ladder ladder={tournament.ladder} />
					)}
				</div>
				<GuardComponent roles={[ROLES.Admin]}>
					{tournament.groups
						.map((group) =>
							group.matches?.map(
								(e) => e.teamAwayScore !== null && e.teamHomeScore !== null
							).every((e) => e) ?? true
						).every((e) => e) && (
							<div className={styles.ladderControls}>
								{(tournament.ladder[0]?.matches[0].teamHomeId ? (<>
									<Button
										value="Zresetuj drabinke"
										onClick={() => setIsLadderReset(true)}
										style={ButtonStyle.Red}
									/>
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
								</>
								) : (<>
									<Button
										value="Zbuduj drabinke"
										onClick={() => setIsLadderCompose(true)}
									/>
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
								</>
								))}
							</div>
						)}
				</GuardComponent>
			</>)}

			<div>
				<div className={styles.header}>
					<h4 className="underline">Albumy</h4>
					<GuardComponent roles={[ROLES.Admin]}>
						<AddIcon
							className="interactiveIcon"
							onClick={() => setIsAddAlbum(true)}
							fontSize="large"
						/>
						{isAddAlbum && (
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
					</GuardComponent>
				</div>
				<ul className={styles.albums}>
					{tournament.albums.map((album, index) => (
						<li
							key={index}
							className={styles.item}
							style={album.profilePicture ? {
								backgroundImage: `url(${Config.HOST}${album.profilePicture})`,
							} : undefined}
							onClick={() => navigate(`/gallery/${id!}/albums/${album.id}`)}
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
		</TournamentContext.Provider>
	);
}
