import { useState, useMemo, useCallback, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

import { AuthContext, TAuthContextProps } from "./auth.context";
import { getProfileById } from "../api";
import { isExpiredToken, notification } from "../utilities";
import { TProfileDto } from "../types";

type TAuthProviderProps = {
    children: React.ReactNode;
};

const AuthProvider = ({ children }: TAuthProviderProps) => {
    const [currentUser, setCurrentUser] = useState<TProfileDto | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const getUser = useCallback(async () => {
        const token = localStorage.getItem("token");

        if (token) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const decoded: any = jwtDecode(token);

            if (isExpiredToken(decoded.Exp)) {
                // If token is expired, remove token.
                localStorage.removeItem("token");
            } else {
                // If token is not expired, get profile information
                const response = await getProfileById(decoded.Id);

                if (response) setCurrentUser(response);
            }
        }

        // Set loading state
        setLoading(false);
    }, []);

    const logOut = useCallback(() => {
        // Remove token in localstorage and current user to null
        localStorage.removeItem("token");
        setCurrentUser(null);

        // Notification
        notification(true, "Bạn đã đăng xuất thành công");

        return true;
    }, []);

    const updateInfoUser = useCallback((payload: TProfileDto | null) => {
        setCurrentUser(payload);
    }, []);

    const value: TAuthContextProps = useMemo(() => {
        return {
            currentUser,
            updateInfoUser,
            logOut,
        };
    }, [currentUser, updateInfoUser, logOut]);

    useEffect(() => {
        getUser();
    }, [getUser]);

    return (
        <AuthContext.Provider value={value}>
            {!loading && children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;
