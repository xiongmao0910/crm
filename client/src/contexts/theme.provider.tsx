import { useState, useMemo, useCallback, useEffect } from "react";
import { TTheme } from "../types";
import { TThemeContextProps, ThemeContext } from "./theme.context";

type TThemeProviderProps = {
    children: React.ReactNode;
};

const ThemeProvider = ({ children }: TThemeProviderProps) => {
    const [theme, setTheme] = useState<TTheme>("light");
    const [loading, setLoading] = useState<boolean>(true);

    const getTheme = useCallback(() => {
        if (localStorage.getItem("theme")) {
            const themeStr = localStorage.getItem("theme") as TTheme;
            document.querySelector("body")?.classList.add(themeStr);
            setTheme(themeStr);
        } else {
            localStorage.setItem("theme", "light");
            document.querySelector("body")?.classList.add("light");
            setTheme("light");
        }
        setLoading(false);
    }, []);

    const updateTheme = useCallback((payload: TTheme) => {
        if (payload === "light")
            document.querySelector("body")?.classList.remove("dark");
        if (payload === "dark")
            document.querySelector("body")?.classList.remove("light");
        document.querySelector("body")?.classList.add(payload);
        localStorage.setItem("theme", payload);
        setTheme(payload);
    }, []);

    const value: TThemeContextProps = useMemo(() => {
        return {
            theme,
            updateTheme,
        };
    }, [theme, updateTheme]);

    useEffect(() => {
        getTheme();
    }, [getTheme]);

    return (
        <ThemeContext.Provider value={value}>
            {!loading && children}
        </ThemeContext.Provider>
    );
};

export default ThemeProvider;
