import React, { useContext } from "react";

import {
    TCountryDto,
    TDepartmentDto,
    TOfficeDto,
    TPositionDto,
} from "../types";

export type TCommonContextProps = {
    position: TPositionDto[] | undefined | null;
    departments: TDepartmentDto[] | undefined | null;
    offices: TOfficeDto[] | undefined | null;
    countries: TCountryDto[] | undefined | null;
};

export const CommonContext = React.createContext<TCommonContextProps>(
    {} as TCommonContextProps
);

export const useCommon = () => {
    return useContext(CommonContext);
};
