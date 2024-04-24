import { Tab } from "@headlessui/react";

import { EmployeeDeleteTable, EmployeeTable } from "../../layout";

const Employee = () => {
    return (
        <>
            <section className="section">
                <div className="section-container">
                    <h2 className="section-title">quản lý nhân viên</h2>
                    <div>
                        <Tab.Group>
                            <Tab.List className="flex flex-wrap text-sm font-medium text-center text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400">
                                <Tab
                                    className={({ selected }) => {
                                        return selected
                                            ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                            : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                    }}
                                >
                                    nhân viên
                                </Tab>
                                <Tab
                                    className={({ selected }) => {
                                        return selected
                                            ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                            : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                    }}
                                >
                                    nhân viên đã xoá
                                </Tab>
                            </Tab.List>
                            <Tab.Panels className="mt-4">
                                <Tab.Panel>
                                    <div className="section-table">
                                        <EmployeeTable />
                                    </div>
                                </Tab.Panel>
                                <Tab.Panel>
                                    <div className="section-table">
                                        <EmployeeDeleteTable />
                                    </div>
                                </Tab.Panel>
                            </Tab.Panels>
                        </Tab.Group>
                    </div>
                </div>
            </section>
        </>
    );
};

export default Employee;
