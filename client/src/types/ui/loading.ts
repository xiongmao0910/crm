export type TLoadingProps = {
    onlyIcon?: boolean;
    message?: string;
    color?: string;
    size?: "xs" | "sm" | "base" | "lg" | "xl" | "2xl" | "3xl" | "4xl" | "5xl";
    type?:
        | "balls"
        | "bars"
        | "bubbles"
        | "cubes"
        | "cylon"
        | "spin"
        | "spinningBubbles"
        | "spokes";
};
