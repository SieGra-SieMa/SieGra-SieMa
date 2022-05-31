import { Ladder as LadderType } from '../../_lib/types';
import Phase from './Phase';
import styles from './Ladder.module.css';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/swiper-bundle.min.css';
import 'swiper/swiper.min.css';


type LadderProps = {
    ladder: LadderType;
}

export default function Ladder({ ladder }: LadderProps) {
    return (
        <Swiper
            slidesPerView={'auto'}
            className={styles.ladder}
        >
            {ladder.phases.map((phase, index) => (
                <SwiperSlide style={{ height: 'auto', display: 'flex', flexShrink: '1' }} key={index}>
                    <Phase phase={phase} />
                </SwiperSlide>
            ))}
        </Swiper>
    );
}