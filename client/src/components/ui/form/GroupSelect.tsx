import { forwardRef } from "react";

import { TGroupSelectProps } from "../../../types";

const GroupSelect = forwardRef<HTMLSelectElement | null, TGroupSelectProps>(
    ({ labelFor, labelText, children, data, optionText, ...props }, ref) => {
        return (
            <>
                <div className="relative flex flex-col gap-2">
                    <label
                        htmlFor={labelFor}
                        className="first-letter:uppercase"
                    >
                        {props.required ? `${labelText} (*)` : labelText}
                    </label>
                    <select
                        id={labelFor}
                        className="px-3 py-3 text-base border rounded outline-none border-gray-300 dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600"
                        {...props}
                        ref={ref}
                    >
                        <option className="first-letter:uppercase" value="-1">
                            {optionText}
                        </option>
                        {data.length > 0 &&
                            data.map((item) => (
                                <option
                                    key={item.id}
                                    className="first-letter:uppercase"
                                    value={item.id}
                                >
                                    {item.nameVI}
                                </option>
                            ))}
                    </select>
                    {children && children}
                </div>
            </>
        );
    }
);
export default GroupSelect;
