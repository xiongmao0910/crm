import { BUTTON_COLOR_CLASS, BUTTON_OUTLINE_COLOR_CLASS } from "../constants";
import { TRoundedProps, TSizeProps, TVariantButtonProps } from "../types";

export const getButtonColor = (color: string, variant: TVariantButtonProps) => {
    let className = "";

    if (variant === "contained") {
        className = BUTTON_COLOR_CLASS[color];
    }

    if (variant === "outlined") {
        className = BUTTON_OUTLINE_COLOR_CLASS[color];
    }

    return className;
};

export const getFontSize = (size: TSizeProps) => {
    let className = "";

    switch (size) {
        case "xs":
            className = "text-xs";
            break;
        case "sm":
            className = "text-sm";
            break;
        case "base":
            className = "text-base";
            break;
        case "lg":
            className = "text-lg";
            break;
        case "xl":
            className = "text-xl";
            break;
        default:
            className = "text-base";
            break;
    }

    return className;
};

export const getRounded = (rounded: TRoundedProps): string => {
    let className: string = "";

    switch (rounded) {
        case "none":
            className = "rounded-none";
            break;
        case "sm":
            className = "rounded-sm";
            break;
        case "rounded":
            className = "rounded";
            break;
        case "md":
            className = "rounded-md";
            break;
        case "lg":
            className = "rounded-lg";
            break;
        case "xl":
            className = "rounded-xl";
            break;
        case "2xl":
            className = "rounded-2xl";
            break;
        case "3xl":
            className = "rounded-3xl";
            break;
        case "full":
            className = "rounded-full";
            break;
        default:
            className = "rounded-none";
            break;
    }

    return className;
};
