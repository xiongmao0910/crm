import { FormEvent, useState } from "react";

import {
    Button,
    GroupInput,
    GroupSelect,
    Modal,
    SwitchToggle,
} from "../../../components";
import {
    TCreateCustomerContactRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";
import { initCustomerContact } from "../../../constants";

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

const CreateCustomerContactModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerContactRequest>) => {
    const [data, setData] = useState<TCreateCustomerContactRequest>({
        ...initCustomerContact,
        idCustomer: idCustomer as number,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerContact,
                idCustomer: idCustomer as number,
            });
    };

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
                <form className="grid gap-2" onSubmit={handleSubmit}>
                    <div className="grid gap-4 sm:gap-8 sm:grid-cols-2">
                        <GroupInput
                            labelFor="nameVI"
                            labelText="họ và tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameVI: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="nameEN"
                            labelText="họ và tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameEN: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            required={true}
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressVI: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressEN: e.target.value,
                                }))
                            }
                        />
                        <GroupSelect
                            labelFor="enumGioiTinh"
                            labelText="giới tính"
                            data={gender}
                            value={data.enumGioiTinh}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    enumGioiTinh: parseInt(e.target.value),
                                }))
                            }
                            optionText="chọn giới tính"
                        />
                        <GroupInput
                            labelFor="handPhone"
                            labelText="số điện thoại"
                            value={data.handPhone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    handPhone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="homePhone"
                            labelText="số điện thoại bàn"
                            value={data.homePhone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    homePhone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="email"
                            labelText="thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    email: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="note"
                            labelText="ghi chú"
                            value={data.note}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    note: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="bankAccountNumber"
                            labelText="số tài khoản ngân hàng"
                            value={data.bankAccountNumber}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankAccountNumber: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="bankBranchName"
                            labelText="tên ngân hàng"
                            value={data.bankBranchName}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankBranchName: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="bankAddress"
                            labelText="địa chỉ ngân hàng"
                            value={data.bankAddress}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankAddress: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="chat"
                            labelText="chat"
                            value={data.chat}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    chat: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="chucVu"
                            labelText="chức vụ"
                            value={data.chucVu}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    chucVu: e.target.value,
                                }))
                            }
                        />
                        <div
                            className="flex items-center gap-2 leading-none cursor-pointer"
                            onClick={() =>
                                setData((prev) => ({
                                    ...prev,
                                    flagDaiDien: !prev?.flagDaiDien,
                                }))
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

export default CreateCustomerContactModal;
