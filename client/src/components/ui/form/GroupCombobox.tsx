import { useEffect, useMemo, useState } from "react";
import { FaChevronDown } from "react-icons/fa";

import { TGroupCombobox, TOptionProps } from "../../../types";

const GroupCombobox = ({
    labelFor,
    labelText,
    children,
    data,
    value,
    updateValue,
    required = false,
    optionText,
}: TGroupCombobox) => {
    const [selected, setSelected] = useState<TOptionProps | null>(null);
    const [isOpenListSelect, setIsOpenListSelect] = useState<boolean>(false);
    const [query, setQuery] = useState<string>("");

    const filterData = useMemo(() => {
        if (query === "") return data;

        const filterRs = data.filter((i) =>
            i.nameVI
                .toLowerCase()
                .replace(/\s+/g, "")
                .includes(query.toLowerCase().replace(/\s+/g, ""))
        );

        return filterRs;
    }, [query, data]);

    /**
     * * Handle events
     */
    const handleValueChange = (selectData: TOptionProps) => {
        setSelected(selectData);
        setIsOpenListSelect(false);
        updateValue(selectData);
        setQuery("");
    };

    useEffect(() => {
        if (value) setSelected(value);
        else setSelected(null);
    }, [value]);

    return (
        <>
            <div className="relative flex flex-col gap-2">
                <label htmlFor={labelFor} className="first-letter:uppercase">
                    {required ? `${labelText} (*)` : labelText}
                </label>
                <div className="relative" id={labelFor}>
                    <div
                        className="bg-transparent border border-gray-300 dark:border-gray-600 px-3 py-3 flex justify-between items-center rounded cursor-pointer"
                        onClick={() => setIsOpenListSelect((prev) => !prev)}
                    >
                        <p className="text-base first-letter:uppercase">
                            {selected ? selected.nameVI : optionText}
                        </p>
                        <FaChevronDown />
                    </div>
                    {isOpenListSelect && (
                        <div className="absolute z-20 top-full left-0 right-0 w-full p-1 border border-gray-300 dark:border-gray-600 overflow-y-auto bg-white dark:bg-gray-800 text-gray-900 dark:text-white">
                            <div className="relative min-h-32 max-h-[30vh] overflow-auto">
                                <input
                                    type="text"
                                    className="w-full bg-white dark:bg-gray-900 border border-gray-300 dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 rounded p-2 sticky top-0 mb-2"
                                    placeholder="Nhập từ khoá tìm kiếm"
                                    value={query}
                                    onChange={(e) => setQuery(e.target.value)}
                                />
                                <ul className="flex flex-col gap-2" role="list">
                                    {filterData.length === 0 && (
                                        <p className="first-letter:uppercase px-2 py-6">
                                            không có kết quả nào được tìm thấy
                                        </p>
                                    )}
                                    {filterData.length > 0 && (
                                        <>
                                            {filterData.map((item) => (
                                                <li
                                                    key={item.id}
                                                    className="first-letter:uppercase cursor-pointer p-2 hover:bg-gray-100 dark:hover:bg-gray-600 rounded"
                                                    onClick={() =>
                                                        handleValueChange(item)
                                                    }
                                                >
                                                    {item.nameVI}
                                                </li>
                                            ))}
                                        </>
                                    )}
                                </ul>
                            </div>
                        </div>
                    )}
                </div>
                {children && children}
            </div>
        </>
    );
};
export default GroupCombobox;
