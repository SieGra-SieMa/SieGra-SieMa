import { useMemo } from 'react';
import { Phase as PhaseType } from '../../../_lib/_types/tournament';
import Phase from './Phase';
import { Swiper, SwiperSlide } from 'swiper/react';
import styles from './Ladder.module.css';
import 'swiper/swiper-bundle.min.css';
import 'swiper/swiper.min.css';


type LadderProps = {
    ladder: PhaseType[];
};

export default function Ladder({ ladder }: LadderProps) {

    const phases = useMemo(() => {
        const phases = [...ladder];
        const last = phases.pop();
        if (last) {
            const final = phases.pop();
            phases.push(last);
            if (final) {
                phases.push(final);
            }
        }
        return phases;
    }, [ladder]);

    return (
        <Swiper
            slidesPerView={'auto'}
            className={styles.ladder}
        >
            {phases.map((phase, index) => (
                <SwiperSlide style={{ height: 'auto', display: 'flex', flexShrink: '1' }} key={index}>
                    <Phase phase={phase} />
                </SwiperSlide>
            ))}
        </Swiper>
    );
};