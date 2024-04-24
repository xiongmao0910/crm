import { FormEvent, useRef, useState } from "react";
import { useMutation } from "react-query";
import { IoEyeSharp } from "react-icons/io5";
import { FaEyeSlash } from "react-icons/fa";

import LogoPreview from "../../assets/images/logo-preview.png";
import { logInWithUsernameAndPassword } from "../../api";
import { Button, FloatingFilledInput } from "../../components";
import { TLogInRequest } from "../../types";
import { notification } from "../../utilities";
import { TAuthContextProps, useAuth } from "../../contexts";

const LogIn = () => {
    const userNameRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);

    const { updateInfoUser }: TAuthContextProps = useAuth();

    const [isShowPasswordInput, setIsShowPasswordInput] =
        useState<boolean>(false);

    const logInMutation = useMutation({
        mutationFn: logInWithUsernameAndPassword,
    });

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const payload: TLogInRequest = {
            username: userNameRef.current?.value ?? "",
            password: passwordRef.current?.value ?? "",
        };

        if (payload.username === "" || payload.password === "") {
            notification(false, "bạn cần nhập đủ thông tin đăng nhập");
            return;
        }

        const data = await logInMutation.mutateAsync(payload);

        if (data) {
            localStorage.setItem("token", data.token);
            updateInfoUser(data.profile);
        }
    };

    return (
        <>
            <section className="w-full min-h-screen flex justify-center items-center bg-white dark:bg-gray-900 text-gray-900 dark:text-white">
                <div className="grid gap-8 p-4 md:p-6 w-[15rem] sm:w-[24rem] md:max-w-[36rem]">
                    <header className="grid gap-4">
                        <div className="w-32 mx-auto aspect-square border border-gray-900 dark:border-white rounded-full bg-white">
                            <img
                                className="img-fluid"
                                src={LogoPreview}
                                alt="Logo preview"
                            />
                        </div>
                        <div className="first-letter:uppercase text-lg text-center font-semibold">
                            đăng nhập vào tài khoản của bạn
                        </div>
                    </header>
                    <form className="grid gap-4" onSubmit={handleSubmit}>
                        <FloatingFilledInput
                            labelFor="username"
                            labelText="username"
                            ref={userNameRef}
                            autoComplete="off"
                        />
                        <FloatingFilledInput
                            type={isShowPasswordInput ? "type" : "password"}
                            labelFor="password"
                            labelText="mật khẩu"
                            ref={passwordRef}
                            autoComplete="off"
                            children={
                                <span
                                    className="absolute top-5 right-2.5 cursor-pointer"
                                    onClick={() =>
                                        setIsShowPasswordInput((prev) => !prev)
                                    }
                                >
                                    {isShowPasswordInput ? (
                                        <FaEyeSlash />
                                    ) : (
                                        <IoEyeSharp />
                                    )}
                                </span>
                            }
                        />
                        <Button
                            buttonLabel="đăng nhập"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            isLoading={logInMutation.isLoading}
                            textIsLoading="đang đăng nhập"
                        />
                    </form>
                </div>
            </section>
        </>
    );
};

export default LogIn;
