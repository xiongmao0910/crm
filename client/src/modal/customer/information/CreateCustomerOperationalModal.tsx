import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { Button, GroupInput, GroupSelect, Modal } from "../../../components";
import {
    TCreateCustomerOperationalRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";
import { initCustomerOperational } from "../../../constants";
import { getAllCustomerContacts, getAllOperationals } from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";

const CreateCustomerOperationalModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerOperationalRequest>) => {
    const { currentUser }: TAuthContextProps = useAuth();

    const [data, setData] = useState<TCreateCustomerOperationalRequest>({
        ...initCustomerOperational,
        idCustomer: idCustomer as number,
        idUserCreate: currentUser?.id as number,
    });

    const { data: contacts } = useQuery({
        queryKey: ["contacts", idCustomer],
        queryFn: () => getAllCustomerContacts(idCustomer as number),
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            idCustomer !== null,
    });

    const { data: operationals } = useQuery({
        queryKey: "operationals",
        queryFn: getAllOperationals,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerOperational,
                idCustomer: idCustomer as number,
                idUserCreate: currentUser?.id as number,
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
                        <GroupSelect
                            labelFor="idLoaiTacNghiep"
                            labelText="loại tác nghiệp"
                            data={
                                operationals && operationals.length > 0
                                    ? operationals.map((i) => ({
                                          id: i.id,
                                          nameVI: i.name,
                                      }))
                                    : []
                            }
                            value={data.idLoaiTacNghiep}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idLoaiTacNghiep: parseInt(e.target.value),
                                }))
                            }
                            optionText="chọn loại tác nghiệp"
                        />
                        <GroupInput
                            labelFor="noiDung"
                            labelText="nội dung"
                            value={data.noiDung}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    noiDung: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            type="date"
                            labelFor="thoiGianThucHien"
                            labelText="thời gian thực hiện"
                            value={data.thoiGianThucHien}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    thoiGianThucHien: e.target.value,
                                }))
                            }
                        />
                        <GroupSelect
                            labelFor="idNguoiLienHe"
                            labelText="người liên hệ"
                            data={
                                contacts && contacts.length > 0 ? contacts : []
                            }
                            value={data.idNguoiLienHe}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idNguoiLienHe: parseInt(e.target.value),
                                }))
                            }
                            optionText="chọn người liên hệ"
                        />
                        <GroupInput
                            labelFor="khachHangPhanHoi"
                            labelText="khách hàng phản hồi"
                            value={data.khachHangPhanHoi}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    khachHangPhanHoi: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            type="date"
                            labelFor="ngayPhanHoi"
                            labelText="ngày phản hồi"
                            value={data.ngayPhanHoi}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    ngayPhanHoi: e.target.value,
                                }))
                            }
                        />
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

export default CreateCustomerOperationalModal;
