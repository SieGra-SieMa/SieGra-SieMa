import styles from './Form.module.css';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

type InputProp = {
    id: string;
    label: string;
    date: Date | null;
    onChange: (date: Date | null) => void;
    minDate?: Date;
    maxDate?: Date;
    filterTime: (date: Date) => boolean;
}

export default function Input({
    id,
    label,
    date,
    onChange,
    minDate,
    maxDate,
    filterTime,
}: InputProp) {
    return (
        <div>
            {label && (<label htmlFor={id} className={styles.datepickerLabel}>{label}</label>)}
            <div>
                <DatePicker
                    id={id}
                    className={[styles.input, styles.datepicker].join(' ')}
                    required
                    isClearable
                    popperPlacement='top'
                    selected={date}
                    showTimeSelect
                    onChange={onChange}
                    minDate={minDate}
                    maxDate={maxDate}
                    filterTime={filterTime}
                    dateFormat='MMMM d, yyyy h:mm aa'
                />
            </div>
        </div>
    );
}