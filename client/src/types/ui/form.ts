import {
    TColorProps,
    TRoundedProps,
    TSizeProps,
    TVariantButtonProps,
} from "./common";

export type TOptionProps = {
    [key: string]: unknown;
    id: number;
    nameVI: string;
};

/**
 * * Group input types
 */
export type TGroupInputProps = {
    labelFor: string;
    labelText: string;
    children?: React.ReactNode;
} & React.ComponentProps<"input">;

export type TGroupTextareaProps = {
    labelFor: string;
    labelText: string;
    children?: React.ReactNode;
} & React.ComponentProps<"textarea">;

export type TGroupSelectProps = {
    labelFor: string;
    labelText: string;
    data: TOptionProps[];
    optionText: string;
    children?: React.ReactNode;
} & React.ComponentProps<"select">;

export type TGroupCombobox = {
    labelFor: string;
    labelText: string;
    data: TOptionProps[];
    optionText: string;
    value?: TOptionProps;
    updateValue: (data: TOptionProps) => void;
    required?: boolean;
    children?: React.ReactNode;
};

/**
 * * Button types
 */
export type TButtonProps = {
    buttonLabel?: string;
    buttonVariant?: TVariantButtonProps;
    buttonColor?: TColorProps;
    buttonSize?: TSizeProps;
    buttonRounded?: TRoundedProps;

    isLoading?: boolean;
    textIsLoading?: string;
} & React.ComponentProps<"button">;

/**
 * * Switch Toggle types
 */
export type TSwitchToggleProps = {
    checked: boolean;
    isSetEnabled?: boolean;
    size?: "sm" | "base" | "lg";
};
