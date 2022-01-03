import { useEffect } from 'react';
import AccountData from '../account-data/AccountData';
import TeamsList from '../teams-list/TeamsList';
import CreateTeam from '../team-options/CreateTeam';
import JoinTeam from '../team-options/JoinTeam';
import styles from './AccountPage.module.css';
import { useLocation, useNavigate } from 'react-router-dom';

const pages = [
    {
        value: '/account',
        title: 'Create / Join a team',
        component: (_: any) => (
            <div style={{
                display: 'flex', 
                justifyContent: 'center', 
                flexWrap: 'wrap',
            }}>
                <CreateTeam />
                <JoinTeam />
            </div>
        ),
    },
    {
        value: '/account/myteams',
        title: 'My teams',
        component: (props: any) => <TeamsList {...props}/>,
    },
];

export default function AccountPage() {

    const navigate = useNavigate();

    const location = useLocation();

    const offset = pages.findIndex(e => e.value === location.pathname);

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <div className={styles.root}>
            <AccountData />
            <div className={styles.container}>
                <div className={styles.navigation}>
                    <ul>
                        {Object.values(pages).map((page) => (
                            <li
                                key={page.value}
                                style={page.value === location.pathname ? {
                                    backgroundColor: '#ffbca1'
                                } : undefined}
                                onClick={() => navigate(page.value)}
                            >
                                {page.title}
                            </li>
                        ))}
                        
                    </ul>
                </div>
                <div className={styles.carouselContainer}>
                    <ul style={{
                        transform: `translateX(${-85 * (offset < 0 ? 0 : offset)}%)`,
                    }}>
                        {pages.map((page) => 
                            <li
                                key={page.value}
                                onClick={() => navigate(page.value)}
                            >
                                {page.component({ reload: page.value === location.pathname })}
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </div>
    );
}
