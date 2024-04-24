import { Fragment, useCallback, useState } from "react";
import { Link } from "react-router-dom";
import { Menu, Transition } from "@headlessui/react";
import { BsFillMoonFill, BsSunFill } from "react-icons/bs";
import { FiChevronDown } from "react-icons/fi";
import { BiLogOut } from "react-icons/bi";
import { AiOutlineUser } from "react-icons/ai";

import {
    TAuthContextProps,
    TThemeContextProps,
    useAuth,
    useTheme,
} from "../../../contexts";
import Modal from "../modal/Modal";
import Button from "../Button";

const Header = () => {
    const { currentUser, logOut }: TAuthContextProps = useAuth();
    const { theme, updateTheme }: TThemeContextProps = useTheme();

    const [isOpenLogOutModal, setIsOpenLogOutModal] = useState<boolean>(false);

    /**
     * * Handle events
     */
    const handleChangeTheme = () => {
        if (theme === "dark") {
            updateTheme("light");
        }

        if (theme === "light") {
            updateTheme("dark");
        }
    };

    const openLogOutModal = useCallback(() => {
        setIsOpenLogOutModal(true);
    }, []);

    const closeLogOutModal = useCallback(() => {
        setIsOpenLogOutModal(false);
    }, []);

    const handleLogOut = useCallback(() => {
        const result = logOut();

        return result;
    }, [logOut]);

    return (
        <>
            <header className="header">
                <div className="header-container">
                    <div className="header-action">
                        {/* Theme toggle */}
                        <div
                            className="header-theme"
                            onClick={handleChangeTheme}
                        >
                            <span className="text-base">
                                {theme === "light" ? (
                                    <BsFillMoonFill />
                                ) : (
                                    <BsSunFill />
                                )}
                            </span>
                        </div>
                        {/* Notification toggle */}
                        {/* <div className="header-notification">
                            <span className="text-base">
                                <BsBellFill />
                            </span>
                        </div> */}
                        {/* Profile toggle */}
                        <div className="header-profile">
                            <Menu as="div" className="relative inline-block">
                                <Menu.Button className="flex items-center gap-1 sm:gap-3">
                                    <span className="hidden text-right sm:block">
                                        <span className="block text-sm font-medium text-[#1c2434] dark:text-white">
                                            {currentUser?.hoten != ""
                                                ? currentUser?.hoten
                                                : currentUser?.username}
                                        </span>
                                        <span className="block text-xs text-[#1c2434] dark:text-white">
                                            {currentUser?.chucvu}
                                        </span>
                                    </span>
                                    <span className="w-8 h-8 md:h-10 md:w-10 rounded-full overflow-hidden">
                                        <img
                                            className="w-full h-full"
                                            src={
                                                currentUser?.photoURL ||
                                                "https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                                            }
                                            alt={currentUser?.username}
                                        />
                                    </span>
                                    <FiChevronDown className="text-xl text-[#1c2434] dark:text-white" />
                                </Menu.Button>
                                <Transition
                                    as={Fragment}
                                    enter="transition ease-out duration-100"
                                    enterFrom="transform opacity-0 scale-95"
                                    enterTo="transform opacity-100 scale-100"
                                    leave="transition ease-in duration-75"
                                    leaveFrom="transform opacity-100 scale-100"
                                    leaveTo="transform opacity-0 scale-95"
                                >
                                    <Menu.Items className="absolute top-full right-0 mt-6 w-56 origin-top-right divide-y rounded-md bg-white shadow-lg focus:outline-none">
                                        {!currentUser?.permission.includes(
                                            "1048576"
                                        ) && (
                                            <>
                                                <Menu.Item>
                                                    <Link
                                                        className="flex items-center w-full gap-2 p-4 text-sm lg:text-base font-medium duration-300 ease-in-out text-gray-500 hover:text-black cursor-pointer"
                                                        to={`/${currentUser?.username}`}
                                                    >
                                                        <AiOutlineUser className="text-xl" />
                                                        <span className="capitalize">
                                                            hồ sơ
                                                        </span>
                                                    </Link>
                                                </Menu.Item>
                                            </>
                                        )}
                                        <Menu.Item>
                                            <button
                                                className="flex items-center w-full gap-2 p-4 text-sm lg:text-base font-medium duration-300 ease-in-out text-gray-500 hover:text-black cursor-pointer"
                                                onClick={openLogOutModal}
                                            >
                                                <BiLogOut className="text-lg" />
                                                <span className="capitalize">
                                                    đăng xuất
                                                </span>
                                            </button>
                                        </Menu.Item>
                                    </Menu.Items>
                                </Transition>
                            </Menu>
                        </div>
                    </div>
                </div>
            </header>
            <Modal
                title="Đăng xuất"
                description="bạn có muốn đăng xuất"
                isOpen={isOpenLogOutModal}
                onClose={closeLogOutModal}
            >
                <div className="flex items-center gap-4">
                    <Button
                        buttonLabel="đăng xuất"
                        buttonVariant="contained"
                        buttonColor="red"
                        buttonRounded="xl"
                        onClick={handleLogOut}
                    />
                    <Button
                        buttonLabel="huỷ"
                        buttonVariant="contained"
                        buttonColor="blue"
                        buttonRounded="xl"
                        onClick={closeLogOutModal}
                    />
                </div>
            </Modal>
        </>
    );
};

export default Header;
