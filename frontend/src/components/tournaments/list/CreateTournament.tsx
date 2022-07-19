import { FormEvent, useState } from "react";
import ReactQuill from "react-quill";
import {
	TournamentListItem,
	TournamentRequest,
} from "../../../_lib/_types/tournament";
import { useApi } from "../../api/ApiContext";
import Button from "../../form/Button";
import DatePicker from "../../form/DatePicker";
import Form from "../../form/Form";
import Input from "../../form/Input";
import VerticalSpacing from "../../spacing/VerticalSpacing";

type CreateTournamentType = {
	confirm: (tournament: TournamentListItem) => void;
};

export default function CreateTournament({ confirm }: CreateTournamentType) {
	const { tournamentsService } = useApi();

	const [name, setName] = useState('');
	const [address, setAddress] = useState('');
	const [description, setDescription] = useState('');

	const [startDate, setStartDate] = useState<Date | null>(null);
	const [endDate, setEndDate] = useState<Date | null>(null);

	const onSubmit = (e: FormEvent) => {
		e.preventDefault();
		const tournament: TournamentRequest = {
			name,
			address,
			description,
			startDate: startDate!.toISOString(),
			endDate: endDate!.toISOString(),
		};
		return tournamentsService.createTournament(tournament)
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
