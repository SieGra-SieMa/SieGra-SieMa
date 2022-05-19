import { useContext, createContext } from 'react';
import ApiClient from '../../_services';

export const ApiContext = createContext(new ApiClient());

export function useApi() {
    return useContext(ApiContext);
}
