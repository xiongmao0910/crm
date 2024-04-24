import { forwardRef } from "react";

import { TGroupTextareaProps } from "../../../types";

const GroupTextarea = forwardRef<
    HTMLTextAreaElement | null,
    TGroupTextareaProps
>(({ labelFor, labelText, children, ...props }, ref) => {
    return (
        <>
            <div className="relative flex flex-col gap-2">
                <label htmlFor={labelFor} className="first-letter:uppercase">
                    {props.required ? `${labelText} (*)` : labelText}
                </label>
                <textarea
                    id={labelFor}
                    className="bg-transparent px-3 py-3 text-base border rounded outline-none border-gray-300 dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600"
                    {...props}
                    ref={ref}
                />
                {children && children}
            </div>
        </>
    );
});

export default GroupTextarea;
