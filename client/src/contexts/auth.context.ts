import React, { useContext } from "react";
import { TProfileDto } from "../types";

export type TAuthContextProps = {
    currentUser: TProfileDto | null;
    updateInfoUser: (payload: TProfileDto | null) => void;
    logOut: () => boolean;
};

export const AuthContext = React.createContext<TAuthContextProps>(
    {} as TAuthContextProps
);

export const useAuth = () => {
    return useContext(AuthContext);
};
