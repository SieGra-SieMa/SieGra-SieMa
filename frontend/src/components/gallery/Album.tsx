import Config from "../../config.json";
import { useEffect, useMemo, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ROLES } from "../../_lib/roles";
import { useApi } from "../api/ApiContext";
import styles from "./AlbumsList.module.css";
import Button, { ButtonStyle } from "../form/Button";
import Modal from "../modal/Modal";
import CreateMedia from "./CreateMedia";
import GuardComponent from "../guard-components/GuardComponent";
import { Album as AlbumType } from "../../_lib/_types/tournament";
import Confirm from "../modal/Confirm";
import { Media } from "../../_lib/_types/response";
import EditAlbum from "./EditAlbum";
import VerticalSpacing from "../spacing/VerticalSpacing";
import { useTournaments } from "../tournaments/TournamentsContext";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useAlert } from "../alert/AlertContext";

export default function Album() {
	const navigate = useNavigate();

	const { albumId } = useParams<{ albumId: string }>();

	const alert = useAlert();
	const { albumsService, mediaService } = useApi();
	const { tournaments } = useTournaments();

	const [album, setAlbum] = useState<AlbumType | null>(null);

	const [isAddPhoto, setIsAddPhoto] = useState(false);
	const [isEdit, setIsEdit] = useState(false);
	const [isRemove, setIsRemove] = useState(false);

	const [selectedImage, setSelectedImage] = useState<Media | null>(null);

	const tournament = useMemo(() => {
		if (!tournaments || !album) return null;
		return tournaments.find(
			(tournament) => tournament.id === parseInt(album.tournamentId)
		);
	}, [album, tournaments]);

	useEffect(() => {
		window.scrollTo(0, 0);
	}, []);

	useEffect(() => {
		albumsService.getAlbumWithMedia(albumId!).then((data) => {
			setAlbum(data);
		});
	}, [albumId, albumsService]);

	const removeAlbum = () => {
		albumsService.deleteAlbum(albumId!).then(() => navigate(-1));
	};

	const deleteMedia = () => {
		if (!selectedImage || !album) return;
		mediaService.deleteMedia(selectedImage.id).then((data) => {
			setAlbum({
				...album,
				mediaList: album.mediaList.filter(
					(media) => media.id !== selectedImage.id
				),
			});
			setSelectedImage(null);
			alert.success(data.message);
		});
	};

	return (
		<>
			<div className={styles.top}>
				<ArrowBackIosNewIcon
					className="interactiveIcon"
					onClick={() => navigate(-1)}
					fontSize="large"
				/>
				<GuardComponent roles={[ROLES.Admin]}>
					<div className={styles.adminControls}>
						<AddIcon
							className="interactiveIcon"
							onClick={() => setIsAddPhoto(true)}
							fontSize="large"
						/>
						<EditIcon
							className="interactiveIcon"
							onClick={() => setIsEdit(true)}
							fontSize="large"
						/>
						<DeleteIcon
							className="interactiveIcon"
							onClick={() => setIsRemove(true)}
							fontSize="large"
						/>
					</div>
				</GuardComponent>
			</div>

			{album && tournament && (
				<>
					<h6 className={styles.breadcrumbs}>
						Galeria - {tournament.name}
					</h6>
					<h1 className={styles.title}>{album.name}</h1>
					<ul className={styles.images}>
						{album.mediaList.map((media, index) => (
							<li
								key={index}
								className={styles.item}
								style={{
									backgroundImage: `url(${Config.HOST}${media.url})`,
								}}
								onClick={() => setSelectedImage(media)}
							></li>
						))}
					</ul>
				</>
			)}

			{album && isAddPhoto && (
				<Modal
					close={() => setIsAddPhoto(false)}
					isClose
					title="Dodaj zdjęcie"
				>
					<CreateMedia
						albumId={album.id}
						confirm={(media) => {
							setAlbum({
								...album,
								mediaList: album.mediaList.concat(media),
							});
							setIsAddPhoto(false);
						}}
					/>
				</Modal>
			)}
			{album && isEdit && (
				<Modal
					close={() => setIsEdit(false)}
					isClose
					title="Edytuj album"
				>
					<EditAlbum
						album={album}
						confirm={(album) => {
							setAlbum(album);
							setIsEdit(false);
						}}
					/>
				</Modal>
			)}
			{album && isRemove && (
				<Modal
					close={() => setIsRemove(false)}
					isClose
					title="Usuń album"
				>
					<Confirm
						cancel={() => setIsRemove(false)}
						confirm={removeAlbum}
						label="Usuń"
						style={ButtonStyle.Red}
					/>
				</Modal>
			)}

			{album && selectedImage && (
				<Modal close={() => setSelectedImage(null)} isClose title="">
					<>
						<img
							alt=""
							className={styles.image}
							src={`${Config.HOST}${selectedImage.url}`}
						/>
						<GuardComponent roles={[ROLES.Admin]}>
							<VerticalSpacing size={30} />
							<Button
								value="Usuń"
								style={ButtonStyle.Red}
								onClick={deleteMedia}
							/>
						</GuardComponent>
					</>
				</Modal>
			)}
		</>
	);
}
