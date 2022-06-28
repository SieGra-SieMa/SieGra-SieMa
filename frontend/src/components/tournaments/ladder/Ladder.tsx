import { useMemo } from "react";
import { Phase as PhaseType } from "../../../_lib/_types/tournament";
import Phase from "./Phase";
import { Swiper, SwiperSlide } from "swiper/react";
import styles from "./Ladder.module.css";
import "swiper/swiper-bundle.min.css";
import "swiper/swiper.min.css";

type LadderProps = {
	ladder: PhaseType[];
};

export default function Ladder({ ladder }: LadderProps) {
	const phases = useMemo(() => {
		const phases = [...ladder];
		const final = phases.pop();
		if (final) {
			const thirdPlace = phases.pop();
			const semiFinal = phases.pop();
			const quarterFinal = phases.pop();
			const firstPhase = phases.pop();
			if (firstPhase) {
				phases.push({
					...firstPhase,
					name: '1/8 finału'
				});
			}
			if (quarterFinal) {
				phases.push({
					...quarterFinal,
					name: 'Ćwierć finał'
				});
			}
			if (semiFinal) {
				phases.push({
					...semiFinal,
					name: 'Pół finał'
				});
			}
			phases.push({
				...final,
				name: 'Finał'
			});
			if (thirdPlace) {
				phases.push({
					...thirdPlace,
					name: 'Mecz o 3 miejsce'
				});
			}
		}
		return phases;
	}, [ladder]);

	return (
		<>
			<h4 className="underline" style={{ width: "fit-content" }}>
				Drabinka
			</h4>
			<Swiper slidesPerView={"auto"} className={styles.ladder}>
				{phases.map((phase, index) => (
					<SwiperSlide
						style={{
							height: "auto",
							display: "flex",
							flexShrink: "1",
						}}
						key={index}
					>
						<Phase phase={phase} />
					</SwiperSlide>
				))}
			</Swiper>
		</>
	);
}
