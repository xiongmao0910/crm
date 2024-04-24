import { Tab } from "@headlessui/react";

import {
    CustomerDeleteTable,
    CustomerAssignedTable,
    CustomerTable,
    CustomerUndeliveredTable,
    CustomerReceivedTable,
    CustomerDeliveredTable,
} from "../../layout";
import { TAuthContextProps, useAuth } from "../../contexts";

const Customer = () => {
    const { currentUser }: TAuthContextProps = useAuth();

    return (
        <>
            <section className="section">
                <div className="section-container">
                    <h2 className="section-title">quản lý khách hàng</h2>
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
                                    khách hàng
                                </Tab>
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7020"
                                    )) && (
                                    <Tab
                                        className={({ selected }) => {
                                            return selected
                                                ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                                : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                        }}
                                    >
                                        khách hàng đã xoá
                                    </Tab>
                                )}
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7080"
                                    )) && (
                                    <Tab
                                        className={({ selected }) => {
                                            return selected
                                                ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                                : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                        }}
                                    >
                                        danh sách chưa giao
                                    </Tab>
                                )}
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7080"
                                    )) && (
                                    <Tab
                                        className={({ selected }) => {
                                            return selected
                                                ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                                : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                        }}
                                    >
                                        danh sách đã giao
                                    </Tab>
                                )}
                                {currentUser?.username !== "admin" && (
                                    <Tab
                                        className={({ selected }) => {
                                            return selected
                                                ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                                : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                        }}
                                    >
                                        danh sách được giao
                                    </Tab>
                                )}
                                <Tab
                                    className={({ selected }) => {
                                        return selected
                                            ? "inline-block p-4 first-letter:uppercase rounded-t-lg text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                            : "inline-block p-4 first-letter:uppercase rounded-t-lg hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                    }}
                                >
                                    danh sách đã nhận
                                </Tab>
                            </Tab.List>
                            <Tab.Panels className="mt-4">
                                <Tab.Panel>
                                    <div className="section-table">
                                        <CustomerTable />
                                    </div>
                                </Tab.Panel>
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7020"
                                    )) && (
                                    <Tab.Panel>
                                        <div className="section-table">
                                            <CustomerDeleteTable />
                                        </div>
                                    </Tab.Panel>
                                )}
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7080"
                                    )) && (
                                    <Tab.Panel>
                                        <div className="section-table">
                                            <CustomerUndeliveredTable />
                                        </div>
                                    </Tab.Panel>
                                )}
                                {(currentUser?.permission.includes("1048576") ||
                                    currentUser?.permission.includes("7000") ||
                                    currentUser?.permission.includes(
                                        "7080"
                                    )) && (
                                    <Tab.Panel>
                                        <div className="section-table">
                                            <CustomerAssignedTable />
                                        </div>
                                    </Tab.Panel>
                                )}
                                {currentUser?.username !== "admin" && (
                                    <Tab.Panel>
                                        <div className="section-table">
                                            <CustomerDeliveredTable />
                                        </div>
                                    </Tab.Panel>
                                )}
                                <Tab.Panel>
                                    <div className="section-table">
                                        <CustomerReceivedTable />
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

export default Customer;
