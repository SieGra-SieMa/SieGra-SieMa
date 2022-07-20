import { FormEvent, useEffect, useState } from 'react';
import Select from 'react-select'
import { User } from '../../../_lib/types';
import { Contest, Contestant } from '../../../_lib/_types/tournament';
import { useApi } from '../../api/ApiContext';
import Button from '../../form/Button';
import Form from '../../form/Form';
import Input from '../../form/Input';
import VerticalSpacing from '../../spacing/VerticalSpacing';
import styles from './Contests.module.css';

type Props = {
    contest: Contest;
    confirm: (data: Contestant) => void;
}

const customStyles = {
    control: (provided: any) => ({
        ...provided,
        padding: "5px 0"
    }),
    menuList: (provided: any) => ({
        ...provided,
        maxHeight: "175px",
    }),
}

export default function AddScoreContest({
    contest,
    confirm,
}: Props) {

    const { tournamentsService, usersService } = useApi();

    const [search, setSearch] = useState('');
    const [points, setPoints] = useState(0);

    const [options, setOptions] = useState<{ label: string, value: User }[]>();

    const [selectedOption, setSelectedOption] = useState<{ label: string, value: User } | null>();

    useEffect(() => {
        return usersService.getUsers(1, 10, search)
            .then((data) => setOptions(data.items.map((item) => ({
                label: item.email,
                value: item,
            }))))
            .abort;
    }, [search, usersService])

    const onSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!selectedOption) return;
        return tournamentsService.addContestScore(
            contest.tournamentId,
            contest.id,
            selectedOption.value.email,
            points
        )
            .then((data) => {
                confirm(data);
            });
    }

    return (
        <Form onSubmit={onSubmit} trigger={<>
            <VerticalSpacing size={15} />
            <Button value='Dodaj wynik' />
        </>}>
            <div>
                <label htmlFor="test">Użytkownik</label>
                <VerticalSpacing size={5} />
                <Select
                    styles={customStyles}
                    placeholder="Wyszukaj..."
                    options={options}
                    onInputChange={(e) => setSearch(e)}
                    onChange={(e) => setSelectedOption(e)}
                    value={selectedOption}
                    formatOptionLabel={({ value, label }) => (
                        <div className={styles.option}>
                            <div className={styles.optionTitle}>
                                {value.name} {value.surname}
                            </div>
                            <div>{label}</div>
                        </div>
                    )}
                />
            </div>
            <Input
                id='AddScoreContest-points'
                label='Punkty'
                value={points.toString()}
                required
                onChange={(e) => {
                    const result = parseInt(e.target.value);
                    if (isNaN(result)) {
                        setPoints(0);
                        return;
                    }
                    setPoints(Math.abs(result));
                }}
            />
        </Form>
    );
}