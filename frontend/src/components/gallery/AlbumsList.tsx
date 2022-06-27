import Config from "../../config.json";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ROLES } from "../../_lib/roles";
import { useApi } from "../api/ApiContext";
import styles from "./AlbumsList.module.css";
import Modal from "../modal/Modal";
import CreateAlbum from "./CreateAlbum";
import GuardComponent from "../guard-components/GuardComponent";
import { TournamentWithAlbums } from "../../_lib/_types/tournament";
import ImageIcon from "@mui/icons-material/Image";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import AddIcon from "@mui/icons-material/Add";

export default function AlbumsList() {
	const navigate = useNavigate();

	const { tournamentsService } = useApi();

	const { id } = useParams<{ id: string }>();

	const [tournamentWithAlbums, setTournamentWithAlbums] =
		useState<TournamentWithAlbums | null>(null);
	const [isAddAlbum, setIsAddAlbum] = useState(false);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	useEffect(() => {
		tournamentsService.getTournamentWithAlbums(id!).then((data) => {
			setTournamentWithAlbums(data);
		});
	}, [id, tournamentsService]);

	return (
		<>
			<div className={styles.top}>
				<ArrowBackIosNewIcon
					className="interactiveIcon"
					onClick={() => navigate("../..")}
					fontSize="large"
				/>
				<GuardComponent roles={[ROLES.Admin]}>
					<AddIcon
						className="interactiveIcon"
						onClick={() => setIsAddAlbum(true)}
						fontSize="large"
					/>
				</GuardComponent>
			</div>

			{tournamentWithAlbums && (
				<>
					<h1 className={styles.title}>
						{tournamentWithAlbums.name}
					</h1>
					<h4>Albumy</h4>
					<ul className={styles.albums}>
						{tournamentWithAlbums.albums.map((album, index) => (
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
								onClick={() => navigate(`${album.id}`)}
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
				</>
			)}

			{tournamentWithAlbums && isAddAlbum && (
				<Modal
					title="Dodaj album"
					close={() => setIsAddAlbum(false)}
					isClose
				>
					<CreateAlbum
						tournamentId={id!}
						confirm={(album) => {
							setTournamentWithAlbums({
								...tournamentWithAlbums,
								albums: tournamentWithAlbums.albums.concat(
									album
								),
							});
							setIsAddAlbum(false);
						}}
					/>
				</Modal>
			)}
		</>
	);
}
