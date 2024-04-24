import { FormEvent, useEffect, useState } from "react";

import {
    Button,
    GroupInput,
    GroupSelect,
    Modal,
    SwitchToggle,
} from "../../components";
import { TCommonContextProps, useCommon } from "../../contexts";
import { TCreateEmployeeModalProps, TCreateEmployeeRequest } from "../../types";
import { initEmployee, permissionAccount } from "../../constants";

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

const CreateEmployeeModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateEmployeeModalProps) => {
    const [data, setData] = useState<TCreateEmployeeRequest>(initEmployee);
    const [permission, setPermission] = useState<string[]>([]);

    const { position, departments, offices }: TCommonContextProps = useCommon();

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result) setData(initEmployee);
    };

    const handlePermissionChange = (checked: boolean, value: string) => {
        if (checked) {
            setPermission((prev) => [...prev, value]);
        }
        if (!checked) {
            const permissionUpdate = permission.filter((e) => e !== value);
            setPermission(permissionUpdate);
        }
    };

    useEffect(() => {
        const permissionStr =
            permission.length > 0 ? `;${permission.join(";")}` : "";
        setData((prev) => ({ ...prev, permission: permissionStr }));
    }, [permission]);

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
                                    setData((prev) => ({
                                        ...prev,
                                        manhanvien: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="hoten"
                                labelText="họ và tên"
                                required={true}
                                value={data.hoten}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        hoten: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="namsinh"
                                labelText="năm sinh"
                                required={true}
                                value={data.namsinh}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        namsinh: e.target.value,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelFor="gioitinh"
                                labelText="giới tính"
                                data={gender}
                                value={data.gioitinh}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        gioitinh: parseInt(e.target.value),
                                    }))
                                }
                                optionText="chọn giới tính"
                            />
                            <GroupInput
                                labelFor="quequan"
                                labelText="quê quán"
                                value={data.quequan}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        quequan: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="diachi"
                                labelText="địa chỉ"
                                value={data.diachi}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        diachi: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="soCMT"
                                labelText="số chứng minh thư"
                                value={data.soCMT}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        soCMT: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="noiCapCMT"
                                labelText="nơi cấp chứng minh thư"
                                value={data.noiCapCMT}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        noiCapCMT: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="ngayCapCMT"
                                labelText="ngày cấp chứng minh thư"
                                value={data.ngayCapCMT}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        ngayCapCMT: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="ghichu"
                                labelText="ghi chú"
                                value={data.ghichu}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        ghichu: e.target.value,
                                    }))
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
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        idChucVu: parseInt(e.target.value),
                                    }))
                                }
                                optionText="chọn chức vụ"
                            />
                            <GroupSelect
                                labelFor="idPhongBan"
                                labelText="phòng ban"
                                data={
                                    departments && departments.length > 0
                                        ? departments
                                        : []
                                }
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        idPhongBan: parseInt(e.target.value),
                                    }))
                                }
                                optionText="chọn phòng ban"
                            />
                            <GroupSelect
                                labelFor="idVanPhong"
                                labelText="văn phòng"
                                data={
                                    offices && offices.length > 0 ? offices : []
                                }
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        idVanPhong: parseInt(e.target.value),
                                    }))
                                }
                                optionText="chọn văn phòng"
                            />
                            <GroupInput
                                labelFor="soLuongKH"
                                labelText="số lượng khách"
                                value={data.soLuongKH}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        soLuongKH: parseInt(e.target.value),
                                    }))
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
                                    setData((prev) => ({
                                        ...prev,
                                        username: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="password"
                                labelText="mật khẩu"
                                required={true}
                                value={data.password}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        password: e.target.value,
                                    }))
                                }
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
                                                !permission.includes(
                                                    item.value
                                                ),
                                                item.value
                                            )
                                        }
                                    >
                                        <SwitchToggle
                                            checked={permission.includes(
                                                item.value
                                            )}
                                            isSetEnabled={false}
                                            size="sm"
                                        />
                                        <p className="text-sm first-letter:uppercase">
                                            {item.label}
                                        </p>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="tạo"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            isLoading={isLoading}
                            textIsLoading="đang tạo"
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
            </Modal>
        </>
    );
};

export default CreateEmployeeModal;
