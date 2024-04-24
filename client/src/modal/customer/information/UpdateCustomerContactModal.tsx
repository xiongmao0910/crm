import { FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import {
    Button,
    GroupInput,
    GroupSelect,
    Modal,
    SwitchToggle,
} from "../../../components";
import {
    TUpdateCustomerContactRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";

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

const UpdateCustomerContactModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerContactRequest>) => {
    const [data, setData] = useState<TUpdateCustomerContactRequest | null>(
        null
    );

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (isEqual(data, prevData)) {
            notification(false, "Thông tin người liên hệ không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
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
                    <form className="grid gap-2" onSubmit={handleSubmit}>
                        <div className="grid gap-4 sm:gap-8 sm:grid-cols-2">
                            <GroupInput
                                labelFor="nameVI"
                                labelText="họ và tên (VI)"
                                required={true}
                                value={data.nameVI}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  nameVI: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="nameEN"
                                labelText="họ và tên (EN)"
                                value={data.nameEN}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  nameEN: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="addressVI"
                                labelText="địa chỉ (VI)"
                                required={true}
                                value={data.addressVI}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  addressVI: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="addressEN"
                                labelText="địa chỉ (EN)"
                                value={data.addressEN}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  addressEN: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupSelect
                                labelFor="enumGioiTinh"
                                labelText="giới tính"
                                data={gender}
                                value={data.enumGioiTinh}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  enumGioiTinh: parseInt(
                                                      e.target.value
                                                  ),
                                              }
                                            : null
                                    )
                                }
                                optionText="chọn giới tính"
                            />
                            <GroupInput
                                labelFor="handPhone"
                                labelText="số điện thoại"
                                value={data.handPhone}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  handPhone: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="homePhone"
                                labelText="số điện thoại bàn"
                                value={data.homePhone}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  homePhone: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="email"
                                labelText="thư điện tử"
                                value={data.email}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  email: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="note"
                                labelText="ghi chú"
                                value={data.note}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  note: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="bankAccountNumber"
                                labelText="số tài khoản ngân hàng"
                                value={data.bankAccountNumber}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  bankAccountNumber:
                                                      e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="bankBranchName"
                                labelText="tên ngân hàng"
                                value={data.bankBranchName}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  bankBranchName:
                                                      e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="bankAddress"
                                labelText="địa chỉ ngân hàng"
                                value={data.bankAddress}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  bankAddress: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="chat"
                                labelText="chat"
                                value={data.chat}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  chat: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="chucVu"
                                labelText="chức vụ"
                                value={data.chucVu}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  chucVu: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <div
                                className="flex items-center gap-2 leading-none cursor-pointer"
                                onClick={() =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  flagDaiDien:
                                                      !prev?.flagDaiDien,
                                              }
                                            : null
                                    )
                                }
                            >
                                <SwitchToggle
                                    checked={data.flagDaiDien}
                                    isSetEnabled={false}
                                    size="sm"
                                />
                                <p className="text-sm first-letter:uppercase">
                                    người đại diện
                                </p>
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

export default UpdateCustomerContactModal;
