import React, { useContext } from "react";
import { TTheme } from "../types";

export type TThemeContextProps = {
    theme: TTheme;
    updateTheme: (payload: TTheme) => void;
};

export const ThemeContext = React.createContext<TThemeContextProps>(
    {} as TThemeContextProps
);

export const useTheme = () => {
    return useContext(ThemeContext);
};
