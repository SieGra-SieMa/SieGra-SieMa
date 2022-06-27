import { useEffect } from "react";
import ApiClient from "../../_services";
import { useAlert } from "../alert/AlertContext";
import { ApiContext } from "./ApiContext";


type ApiProviderProps = {
    children: React.ReactNode;
    value: ApiClient;
}

export default function ApiProvider({ children, value }: ApiProviderProps) {

    const alert = useAlert();

    useEffect(() => {
        value.setAlert(alert.error);
    }, [value, alert.error]);

    return (
        <ApiContext.Provider value={value}>
            {children}
        </ApiContext.Provider>
    );
}
