import { useContext, createContext } from 'react';
import services from '../../_services';

export const ApiContext = createContext(services);

export function useApi() {
    return useContext(ApiContext);
}
