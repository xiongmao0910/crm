import { Fragment } from "react";
import { Dialog, Transition } from "@headlessui/react";
import { FaTimes } from "react-icons/fa";

import { TModalProps } from "../../../types";
import { MAXWIDTH_CLASS } from "../../../constants";

const Modal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    children,
}: TModalProps) => {
    return (
        <>
            <Transition appear show={isOpen} as={Fragment}>
                <Dialog as="div" className="relative z-[100]" onClose={onClose}>
                    {/* Backdrop */}
                    <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0"
                        enterTo="opacity-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100"
                        leaveTo="opacity-0"
                    >
                        <div className="fixed inset-0 bg-black/25 dark:bg-white/25" />
                    </Transition.Child>
                    {/* Modal content */}
                    <div className="fixed inset-0 overflow-y-auto">
                        <div className="flex min-h-full items-center justify-center p-4">
                            <Transition.Child
                                as={Fragment}
                                enter="ease-out duration-300"
                                enterFrom="opacity-0 scale-95"
                                enterTo="opacity-100 scale-100"
                                leave="ease-in duration-200"
                                leaveFrom="opacity-100 scale-100"
                                leaveTo="opacity-0 scale-95"
                            >
                                <Dialog.Panel
                                    className={`w-full max-h-[90vh] overflow-y-auto transform overflow-hidden rounded-2xl bg-white text-neutral-900 px-6 text-left align-middle shadow-xl transition-all
                                        ${MAXWIDTH_CLASS[width]} ${
                                        height ? "h-[" + height + "]" : ""
                                    }`}
                                >
                                    <div className="flex flex-col gap-6 relative">
                                        <div className="flex justify-between items-center sticky top-0 left-0 w-full py-6 bg-white z-10">
                                            <Dialog.Title
                                                as="h3"
                                                className="text-2xl font-medium leading-6 text-gray-900"
                                            >
                                                {title}
                                            </Dialog.Title>
                                            <button
                                                className="text-sm text-gray-400 hover:text-gray-900 transition-all"
                                                onClick={onClose}
                                            >
                                                <FaTimes />
                                            </button>
                                        </div>
                                        <div className="pb-6">
                                            {description && (
                                                <div className="py-2 mb-8">
                                                    <p className="text-gray-900 first-letter:uppercase">
                                                        {description}
                                                    </p>
                                                </div>
                                            )}
                                            {children}
                                        </div>
                                    </div>
                                </Dialog.Panel>
                            </Transition.Child>
                        </div>
                    </div>
                </Dialog>
            </Transition>
        </>
    );
};

export default Modal;
