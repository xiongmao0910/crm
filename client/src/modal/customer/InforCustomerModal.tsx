import { Tab } from "@headlessui/react";

import { Modal } from "../../components";
import { MAXWIDTH_CLASS } from "../../constants";
import {
    CustomerContactTable,
    CustomerClassifyTable,
    CustomerEvaluateTable,
    CustomerImExTable,
    CustomerMajorTable,
    CustomerOperationalTable,
    CustomerRouteTable,
} from "../../layout";
import { TInforCustomerModalProps } from "../../types";

const InforCustomerModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    idCustomer,
}: TInforCustomerModalProps) => {
    return (
        <>
            <Modal
                isOpen={isOpen}
                onClose={onClose}
                width={width}
                height={height}
                title={title}
                description={description}
            >
                <Tab.Group>
                    <div
                        className={`md:flex w-full text-start overflow-auto ${MAXWIDTH_CLASS[width]}`}
                    >
                        <Tab.List className="p-4 md:p-6 flex-shrink-0 flex-grow-0 relative md:w-64 max-h-[45rem] overflow-auto no-scrollbar border-t sm:border-t-0 sm:border-r border-gray-300 dark:border-gray-600 flex flex-col space-y-4 text-sm font-medium text-gray-500 dark:text-gray-400 md:me-4 mb-4 md:mb-0">
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách liên hệ
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách tác nghiệp
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách nghiệp vụ
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách phân loại
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách đánh giá
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách tuyến hàng
                            </Tab>
                            <Tab
                                className={({ selected }) => {
                                    return selected
                                        ? "inline-block p-4 first-letter:uppercase text-blue-600 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500 outline-none font-semibold"
                                        : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold";
                                }}
                            >
                                danh sách xuất nhập khẩu
                            </Tab>
                        </Tab.List>
                        <Tab.Panels className="p-4 md:p-6 flex-1 overflow-auto">
                            <Tab.Panel>
                                <CustomerContactTable idCustomer={idCustomer} />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerOperationalTable
                                    idCustomer={idCustomer}
                                />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerMajorTable idCustomer={idCustomer} />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerClassifyTable
                                    idCustomer={idCustomer}
                                />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerEvaluateTable
                                    idCustomer={idCustomer}
                                />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerRouteTable idCustomer={idCustomer} />
                            </Tab.Panel>
                            <Tab.Panel>
                                <CustomerImExTable idCustomer={idCustomer} />
                            </Tab.Panel>
                        </Tab.Panels>
                    </div>
                </Tab.Group>
            </Modal>
        </>
    );
};

export default InforCustomerModal;
