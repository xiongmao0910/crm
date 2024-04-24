import { useMemo } from "react";

import { CommonContext, TCommonContextProps } from "./common.context";
import { useQuery } from "react-query";
import {
    getAllCountries,
    getAllDepartments,
    getAllOffices,
    getAllPositions,
} from "../api";

type TCommonProviderProps = {
    children: React.ReactNode;
};

const CommonProvider = ({ children }: TCommonProviderProps) => {
    const { data: position, isLoading: isLoading1 } = useQuery({
        queryKey: "position",
        queryFn: getAllPositions,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: departments, isLoading: isLoading2 } = useQuery({
        queryKey: "departments",
        queryFn: getAllDepartments,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: offices, isLoading: isLoading3 } = useQuery({
        queryKey: "offices",
        queryFn: getAllOffices,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: countries, isLoading: isLoading4 } = useQuery({
        queryKey: "countries",
        queryFn: getAllCountries,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const value: TCommonContextProps = useMemo(() => {
        return {
            position,
            departments,
            offices,
            countries,
        };
    }, [position, departments, offices, countries]);

    return (
        <CommonContext.Provider value={value}>
            {!isLoading1 &&
                !isLoading2 &&
                !isLoading3 &&
                !isLoading4 &&
                children}
        </CommonContext.Provider>
    );
};

export default CommonProvider;
