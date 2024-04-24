import { forwardRef, useMemo } from "react";

import { TButtonProps } from "../../types";
import { getButtonColor, getFontSize, getRounded } from "../../utilities";
import Loading from "./Loading";

const Button = forwardRef<HTMLButtonElement | null, TButtonProps>(
    (
        {
            buttonLabel = "button",
            buttonVariant = "contained",
            buttonColor = "neutral",
            buttonSize = "base",
            buttonRounded = "none",
            isLoading = false,
            textIsLoading = "loading...",
            ...props
        },
        ref
    ) => {
        const buttonClass = useMemo(() => {
            const baseClass =
                "inline-block font-medium py-2 px-4 first-letter:uppercase select-none border";
            const addClass = props.className ? props.className : "";
            const colorClass = getButtonColor(buttonColor, buttonVariant);
            const fontClass = getFontSize(buttonSize);
            const roundedClass = getRounded(buttonRounded);
            const classLoading = isLoading
                ? "disabled:opacity-75 cursor-not-allowed"
                : "";

            const classes = [
                baseClass,
                addClass,
                colorClass,
                fontClass,
                roundedClass,
                classLoading,
            ];
            const className = classes.join(" ");

            return className;
        }, [
            buttonVariant,
            buttonColor,
            buttonSize,
            buttonRounded,
            props.className,
            isLoading,
        ]);

        return (
            <>
                <button
                    {...props}
                    className={buttonClass}
                    disabled={isLoading}
                    ref={ref}
                >
                    {!isLoading && buttonLabel}
                    {isLoading && (
                        <span className="inline-flex items-center gap-2">
                            <span>
                                {<Loading onlyIcon={true} color="#ffffff" />}
                            </span>
                            <span className="first-letter:uppercase">
                                {textIsLoading}
                            </span>
                        </span>
                    )}
                </button>
            </>
        );
    }
);

export default Button;
