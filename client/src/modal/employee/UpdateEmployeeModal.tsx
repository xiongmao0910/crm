import { FormEvent, useEffect, useRef, useState } from "react";
import { isEqual } from "lodash";

import {
    Button,
    GroupInput,
    GroupSelect,
    Modal,
    SwitchToggle,
} from "../../components";
import { TCommonContextProps, useCommon } from "../../contexts";
import { TUpdateEmployeeModalProps, TUpdateEmployeeRequest } from "../../types";
import { permissionAccount } from "../../constants";
import { notification } from "../../utilities";

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

const UpdateEmployeeModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateEmployeeModalProps) => {
    const [data, setData] = useState<TUpdateEmployeeRequest | null>(null);

    const passwordRef = useRef<HTMLInputElement | null>(null);

    const { position, departments, offices }: TCommonContextProps = useCommon();

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (isEqual(data, prevData) && passwordRef.current?.value === "") {
            notification(false, "Thông tin nhân viên không thay đổi");
            return;
        }

        if (data) {
            const payload: TUpdateEmployeeRequest = {
                id: data.id,
                username: data.username,
                password: passwordRef.current?.value ?? "",
                active: true,
                permission: data.permission,
                idNhanVien: data.idNhanVien,
                idChucVu: data.idChucVu,
                idPhongBan: data.idPhongBan,
                idVanPhong: data.idVanPhong,
                manhanvien: data.manhanvien,
                hoten: data.hoten,
                namsinh: data.namsinh,
                gioitinh: data.gioitinh,
                quequan: data.quequan,
                diachi: data.diachi,
                soCMT: data.soCMT,
                noiCapCMT: data.noiCapCMT,
                ngayCapCMT: data.ngayCapCMT,
                photoURL: data.photoURL,
                ghichu: data.ghichu,
                soLuongKH: data.soLuongKH,
            };

            await onSubmit(payload);
        }
    };

    const handlePermissionChange = (checked: boolean, value: string) => {
        let permissionsArr = data?.permission?.split(";") || [];
        permissionsArr?.shift();

        if (checked) {
            permissionsArr.push(value);
        }

        if (!checked) {
            permissionsArr = permissionsArr.filter((e) => e !== value);
        }

        const permissionStr =
            permissionsArr.length > 0 ? `;${permissionsArr.join(";")}` : "";

        setData((prev) => {
            if (prev) return { ...prev, permission: permissionStr };
            return null;
        });
    };

    useEffect(() => {
        setData(prevData);
    }, [prevData]);

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
                {data !== null && (
                    <form className="grid gap-4" onSubmit={handleSubmit}>
                        <div className="grid gap-4 sm:gap-8 sm:grid-cols-2">
                            <div className="flex flex-col gap-2">
                                <h3 className="first-letter:uppercase text-lg">
                                    thông tin cơ bản
                                </h3>
                                <GroupInput
                                    labelFor="manhanvien"
                                    labelText="mã nhân viên"
                                    required={true}
                                    value={data.manhanvien}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    manhanvien: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="hoten"
                                    labelText="họ và tên"
                                    required={true}
                                    value={data.hoten}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    hoten: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    type="date"
                                    labelFor="namsinh"
                                    labelText="năm sinh"
                                    required={true}
                                    value={data.namsinh}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    namsinh: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupSelect
                                    labelFor="gioitinh"
                                    labelText="giới tính"
                                    data={gender}
                                    value={
                                        gender.find(
                                            (i) => i.id === data.gioitinh
                                        )?.id
                                    }
                                    optionText="chọn giới tính"
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    gioitinh: parseInt(
                                                        e.target.value
                                                    ),
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="quequan"
                                    labelText="quê quán"
                                    value={data.quequan}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    quequan: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="diachi"
                                    labelText="địa chỉ"
                                    value={data.diachi}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    diachi: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="soCMT"
                                    labelText="số chứng minh thư"
                                    value={data.soCMT}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    soCMT: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="noiCapCMT"
                                    labelText="nơi cấp chứng minh thư"
                                    value={data.noiCapCMT}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    noiCapCMT: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    type="date"
                                    labelFor="ngayCapCMT"
                                    labelText="ngày cấp chứng minh thư"
                                    value={data.ngayCapCMT}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    ngayCapCMT: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="ghichu"
                                    labelText="ghi chú"
                                    value={data.ghichu}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    ghichu: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                            </div>
                            <div className="flex flex-col gap-2">
                                <h3 className="first-letter:uppercase text-lg">
                                    thông tin làm việc
                                </h3>
                                <GroupSelect
                                    labelFor="idChucVu"
                                    labelText="chức vụ"
                                    data={
                                        position && position.length > 0
                                            ? position
                                            : []
                                    }
                                    optionText="chọn chức vụ"
                                    value={data.idChucVu ? data.idChucVu : -1}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    idChucVu: parseInt(
                                                        e.target.value
                                                    ),
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupSelect
                                    labelFor="idPhongBan"
                                    labelText="phòng ban"
                                    data={
                                        departments && departments.length > 0
                                            ? departments
                                            : []
                                    }
                                    optionText="chọn phòng ban"
                                    value={
                                        data.idPhongBan ? data.idPhongBan : -1
                                    }
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    idPhongBan: parseInt(
                                                        e.target.value
                                                    ),
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupSelect
                                    labelFor="idVanPhong"
                                    labelText="chức vụ"
                                    data={
                                        offices && offices.length > 0
                                            ? offices
                                            : []
                                    }
                                    optionText="chọn văn phòng"
                                    value={
                                        data.idVanPhong ? data.idVanPhong : -1
                                    }
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    idVanPhong: parseInt(
                                                        e.target.value
                                                    ),
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    labelFor="soLuongKH"
                                    labelText="số lượng khách"
                                    value={data.soLuongKH}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    soLuongKH: parseInt(
                                                        e.target.value
                                                    ),
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <h3 className="first-letter:uppercase text-lg">
                                    thông tin tài khoản
                                </h3>
                                <GroupInput
                                    labelFor="username"
                                    labelText="tên đăng nhập"
                                    required={true}
                                    value={data.username}
                                    onChange={(e) =>
                                        setData((prev) => {
                                            if (prev)
                                                return {
                                                    ...prev,
                                                    username: e.target.value,
                                                };
                                            return null;
                                        })
                                    }
                                />
                                <GroupInput
                                    type="password"
                                    labelFor="password"
                                    labelText="mật khẩu"
                                    ref={passwordRef}
                                />
                                <h3 className="first-letter:uppercase text-lg">
                                    phân quyền tài khoản
                                </h3>
                                <div className="grid grid-cols-2 gap-2">
                                    {permissionAccount.map((item) => (
                                        <div
                                            key={item.label}
                                            className="flex items-center gap-2 col-span-2 md:col-span-1 leading-none cursor-pointer"
                                            onClick={() =>
                                                handlePermissionChange(
                                                    !data.permission.includes(
                                                        item.value
                                                    ),
                                                    item.value
                                                )
                                            }
                                        >
                                            <SwitchToggle
                                                checked={data.permission.includes(
                                                    item.value
                                                )}
                                                isSetEnabled={false}
                                                size="sm"
                                            />
                                            <p className="text-sm">
                                                {item.label}
                                            </p>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </div>
                        <div className="flex items-center gap-4">
                            <Button
                                buttonLabel="cập nhật"
                                buttonVariant="contained"
                                buttonColor="blue"
                                buttonRounded="xl"
                                isLoading={isLoading}
                                textIsLoading="đang cập nhật"
                            />
                            <Button
                                type="button"
                                buttonLabel="huỷ"
                                buttonVariant="contained"
                                buttonColor="red"
                                buttonRounded="xl"
                                onClick={onClose}
                            />
                        </div>
                    </form>
                )}
            </Modal>
        </>
    );
};

export default UpdateEmployeeModal;
