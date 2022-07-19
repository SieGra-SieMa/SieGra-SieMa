import { FormEvent, useState } from "react";
import { Tournament, TournamentRequest } from "../../../_lib/_types/tournament";
import { useApi } from "../../api/ApiContext";
import Button from "../../form/Button";
import DatePicker from "../../form/DatePicker";
import Input from "../../form/Input";
import VerticalSpacing from "../../spacing/VerticalSpacing";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import Form from "../../form/Form";


type Props = {
	tournament: Tournament;
	confirm: (tournament: Tournament) => void;
};

export default function EditTournament({
	tournament,
	confirm,
}: Props) {
	const { tournamentsService } = useApi();

	const [name, setName] = useState(tournament.name);
	const [address, setAddress] = useState(tournament.address);
	const [description, setDescription] = useState(tournament.description);

	const [startDate, setStartDate] = useState<Date | null>(
		new Date(tournament.startDate)
	);
	const [endDate, setEndDate] = useState<Date | null>(
		new Date(tournament.endDate)
	);

	const onSubmit = (e: FormEvent) => {
		e.preventDefault();
		const updatedTournament: TournamentRequest = {
			name,
			startDate: startDate!.toISOString(),
			endDate: endDate!.toISOString(),
			address,
			description,
		};

		return tournamentsService
			.updateTournament(tournament.id, updatedTournament)
			.then((data) => {
				confirm(data);
			});
	};

	return (
		<Form onSubmit={onSubmit} trigger={<>
			<VerticalSpacing size={15} />
			<Button value="Zatwierdź" />
		</>}>
			<Input
				id="TournamentAdd-name"
				label="Nazwa"
				value={name}
				required
				onChange={(e) => setName(e.target.value)}
			/>
			<Input
				id="TournamentAdd-address"
				label="Adres"
				value={address}
				required
				onChange={(e) => setAddress(e.target.value)}
			/>
			<p>Opis</p>
			<ReactQuill
				className="quill"
				theme="snow"
				value={description}
				onChange={setDescription}
				style={{ minHeight: "30px", backgroundColor: "white" }}
			/>
			<DatePicker
				id="DatePicker-startDate"
				label="Czas rozpoczęcia"
				date={startDate}
				onChange={(date) => setStartDate(date)}
				maxDate={endDate ?? undefined}
				filterTime={(date) =>
					endDate ? date.getTime() - endDate.getTime() <= 0 : true
				}
			/>
			<DatePicker
				id="DatePicker-endDate"
				label="Czas końca"
				date={endDate}
				onChange={(date) => setEndDate(date)}
				minDate={startDate ?? undefined}
				filterTime={(date) =>
					startDate ? date.getTime() - startDate.getTime() >= 0 : true
				}
			/>
		</Form>
	);
}
