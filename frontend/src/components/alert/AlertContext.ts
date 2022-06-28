import { useContext, createContext } from 'react';

interface AlertContextType {
    success: (message: string) => void;
    error: (message: string) => void;
}

export const AlertContext = createContext<AlertContextType>(null!);

export function useAlert() {
    return useContext(AlertContext);
}
