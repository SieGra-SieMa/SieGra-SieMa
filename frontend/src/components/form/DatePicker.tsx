import styles from './Form.module.css';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

type InputProp = {
    id: string;
    label: string;
    date: Date | null;
    onChange: (date: Date | null) => void;
    filterDate: (date: Date) => boolean;
    filterTime: (date: Date) => boolean;
}

export default function Input({
    id,
    label,
    date,
    onChange,
    filterDate,
    filterTime,
}: InputProp) {
    return (
        <div>
            {label && (<label htmlFor={id} className={styles.datepickerLabel}>{label}</label>)}
            <DatePicker
                id={id}
                className={[styles.input, styles.datepicker].join(' ')}
                required
                isClearable
                selected={date}
                showTimeSelect
                onChange={onChange}
                filterDate={filterDate}
                filterTime={filterTime}
                dateFormat='MMMM d, yyyy h:mm aa'
            />
        </div>
    );
}