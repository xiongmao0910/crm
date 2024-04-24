import { FormEvent, useEffect, useRef, useState } from "react";
import { useMutation } from "react-query";
import { useParams } from "react-router-dom";
import { isEqual } from "lodash";

import {
    TChangePasswordRequest,
    TProfileDto,
    TUpdateProfileRequest,
} from "../../types";
import { TAuthContextProps, useAuth } from "../../contexts";
import { NotFound } from "../../layout";
import { changePassword, updateProfile } from "../../api";
import { notification } from "../../utilities";
import { Button, GroupInput, GroupSelect } from "../../components";

const gender = [
    {
        id: 0,
        nameVI: "nam",
    },
    {
        id: 1,
        nameVI: "nữ",
    },
];

const EditProfile = () => {
    const { username } = useParams();

    const [profile, setProfile] = useState<TProfileDto | null>(null);

    const { currentUser, updateInfoUser }: TAuthContextProps = useAuth();

    const avatarRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);
    const newPasswordRef = useRef<HTMLInputElement | null>(null);
    const reNewPasswordRef = useRef<HTMLInputElement | null>(null);

    const updateProfileMutation = useMutation({
        mutationFn: updateProfile,
    });

    const changePasswordMutation = useMutation({
        mutationFn: changePassword,
        onSuccess: (data) => {
            if (
                data &&
                passwordRef.current &&
                newPasswordRef.current &&
                reNewPasswordRef.current
            ) {
                passwordRef.current.value = "";
                newPasswordRef.current.value = "";
                reNewPasswordRef.current.value = "";
            }
        },
    });

    /**
     * Handle events
     */
    const openAvatarFile = () => {
        avatarRef.current?.click();
    };

    const handleAvatarChange = () => {
        const file = avatarRef.current?.files
            ? avatarRef.current.files[0]
            : undefined;

        if (file) {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                console.log(reader.result);
                setProfile((prev) =>
                    prev != null
                        ? { ...prev, photoURL: reader.result as string }
                        : null
                );
            };
        }
    };

    const handleUpdateProfile = async (e: FormEvent) => {
        e.preventDefault();

        if (isEqual(currentUser, profile)) {
            notification(false, "Dữ liệu không thay đổi");
            return;
        }

        if (profile) {
            const payload: TUpdateProfileRequest = {
                id: profile.idNhanVien,
                hoten: profile.hoten,
                namsinh: profile.namsinh,
                gioitinh: profile.gioitinh,
                quequan: profile.quequan,
                diachi: profile.diachi,
                soCMT: profile.soCMT,
                noiCapCMT: profile.noiCapCMT,
                ngayCapCMT: profile.ngayCapCMT,
                didong: profile.didong,
                email: profile.email,
                photoURL: profile.photoURL,
            };

            const isSuccess = await updateProfileMutation.mutateAsync(payload);

            if (isSuccess && currentUser) {
                updateInfoUser({ ...currentUser, ...payload });
            }
        }
    };

    const handleChangePassword = async (e: FormEvent) => {
        e.preventDefault();

        if (
            !passwordRef.current?.value ||
            !newPasswordRef.current?.value ||
            !reNewPasswordRef.current?.value
        ) {
            notification(false, "Vui lòng nhập đầy đủ thông tin");
            return;
        }

        if (newPasswordRef.current?.value !== reNewPasswordRef.current?.value) {
            notification(false, "Thông tin mật khẩu mới không khớp!");
            return;
        }

        const payload: TChangePasswordRequest = {
            id: currentUser?.id as number,
            password: passwordRef.current?.value,
            newPassword: newPasswordRef.current?.value,
        };

        await changePasswordMutation.mutateAsync(payload);
    };

    useEffect(() => {
        setProfile(currentUser);
    }, [currentUser]);

    if (currentUser !== null && currentUser.username !== username) {
        return (
            <div className="h-screen">
                <NotFound />
            </div>
        );
    }

    return (
        <>
            <section className="section">
                <div className="section-container">
                    <h2 className="section-title">
                        Chỉnh sửa hồ sơ người dùng
                    </h2>
                    <div className="grid grid-cols-5 gap-8 mt-6">
                        <div className="col-span-5 xl:col-span-3">
                            <div className="rounded-sm border border-stroke shadow-lg">
                                <div className="border-b border-stroke py-4 px-7">
                                    <h3 className="first-letter:uppercase font-semibold">
                                        thông tin cá nhân
                                    </h3>
                                </div>
                                {profile && (
                                    <>
                                        <div className="p-7">
                                            <form
                                                className="flex flex-col gap-4"
                                                onSubmit={handleUpdateProfile}
                                            >
                                                <div className="flex items-center gap-4">
                                                    <img
                                                        className="w-24 h-24 rounded-full object-cover cursor-pointer hover:opacity-85"
                                                        src={
                                                            profile.photoURL
                                                                ? profile.photoURL
                                                                : "https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                                                        }
                                                        alt="profile"
                                                        onClick={openAvatarFile}
                                                    />
                                                    <Button
                                                        type="button"
                                                        onClick={openAvatarFile}
                                                        buttonVariant="contained"
                                                        buttonSize="sm"
                                                        buttonColor="blue"
                                                        buttonLabel="thay đổi ảnh"
                                                        buttonRounded="lg"
                                                    />
                                                    <input
                                                        type="file"
                                                        name="avatar"
                                                        id="avatar"
                                                        ref={avatarRef}
                                                        onChange={
                                                            handleAvatarChange
                                                        }
                                                        className="sr-only"
                                                    />
                                                </div>
                                                <GroupInput
                                                    labelFor="hoten"
                                                    labelText="họ tên"
                                                    required={true}
                                                    value={profile.hoten}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            hoten: e.target
                                                                .value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    labelFor="didong"
                                                    labelText="số điện thoại"
                                                    value={profile.didong}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            didong: e.target
                                                                .value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    labelFor="email"
                                                    labelText="địa chỉ email"
                                                    value={profile.email}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            email: e.target
                                                                .value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    labelFor="diachi"
                                                    labelText="địa chỉ"
                                                    value={profile.diachi}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            diachi: e.target
                                                                .value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    labelFor="quequan"
                                                    labelText="quê quán"
                                                    value={profile.quequan}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            quequan:
                                                                e.target.value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    type="date"
                                                    labelFor="namsinh"
                                                    labelText="ngày sinh"
                                                    required={true}
                                                    value={profile.namsinh}
                                                    onChange={(e) =>
                                                        setProfile({
                                                            ...profile,
                                                            namsinh:
                                                                e.target.value,
                                                        })
                                                    }
                                                />
                                                <GroupSelect
                                                    labelFor="gioitinh"
                                                    labelText="giới tính"
                                                    data={gender}
                                                    value={profile.gioitinh?.toString()}
                                                    onChange={(e) =>
                                                        setProfile({
                                                            ...profile,
                                                            gioitinh: parseInt(
                                                                e.target.value
                                                            ),
                                                        })
                                                    }
                                                    optionText="chọn giới tính"
                                                />
                                                <GroupInput
                                                    labelFor="soCMT"
                                                    labelText="số chứng minh thư"
                                                    value={profile.soCMT}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            soCMT: e.target
                                                                .value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    labelFor="noiCapCMT"
                                                    labelText="nơi cấp chứng minh thư"
                                                    value={profile.noiCapCMT}
                                                    onChange={(e) => {
                                                        setProfile({
                                                            ...profile,
                                                            noiCapCMT:
                                                                e.target.value,
                                                        });
                                                    }}
                                                />
                                                <GroupInput
                                                    type="date"
                                                    labelFor="ngayCapCMT"
                                                    labelText="ngày cấp chứng minh thư"
                                                    value={profile.ngayCapCMT}
                                                    onChange={(e) =>
                                                        setProfile({
                                                            ...profile,
                                                            ngayCapCMT:
                                                                e.target.value,
                                                        })
                                                    }
                                                />
                                                <Button
                                                    buttonVariant="contained"
                                                    buttonColor="blue"
                                                    buttonLabel="chỉnh sửa"
                                                    buttonRounded="lg"
                                                    isLoading={
                                                        updateProfileMutation.isLoading
                                                    }
                                                    textIsLoading="đang cập nhật..."
                                                />
                                            </form>
                                        </div>
                                    </>
                                )}
                            </div>
                        </div>
                        <div className="col-span-5 xl:col-span-2">
                            <div className="rounded-sm border border-stroke shadow-lg">
                                <div className="border-b border-stroke py-4 px-7">
                                    <h3 className="first-letter:uppercase font-semibold">
                                        mật khẩu
                                    </h3>
                                </div>
                                <div className="p-7">
                                    <form
                                        className="flex flex-col gap-4"
                                        onSubmit={handleChangePassword}
                                    >
                                        <GroupInput
                                            type="password"
                                            autoComplete="off"
                                            labelFor="passWord"
                                            labelText="Mật khẩu cũ"
                                            ref={passwordRef}
                                        />
                                        <GroupInput
                                            type="password"
                                            labelFor="newPassWord"
                                            labelText="Mật khẩu mới"
                                            ref={newPasswordRef}
                                        />
                                        <GroupInput
                                            type="password"
                                            labelFor="reNewPassWord"
                                            labelText="Nhập lại mật khẩu mới"
                                            ref={reNewPasswordRef}
                                        />
                                        <Button
                                            buttonVariant="contained"
                                            buttonColor="blue"
                                            buttonLabel="đổi mật khẩu"
                                            buttonRounded="lg"
                                            isLoading={
                                                changePasswordMutation.isLoading
                                            }
                                            textIsLoading="đang đổi mật khẩu..."
                                        />
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
};

export default EditProfile;
