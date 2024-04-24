import { FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import { Button, GroupInput, GroupSelect, Modal } from "../../../components";
import {
    TUpdateCustomerOperationalRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";
import { getAllCustomerContacts, getAllOperationals } from "../../../api";
import { useQuery } from "react-query";

const UpdateCustomerOperationalModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerOperationalRequest>) => {
    const [data, setData] = useState<TUpdateCustomerOperationalRequest | null>(
        null
    );

    const { data: contacts } = useQuery({
        queryKey: "contacts",
        queryFn: () => getAllCustomerContacts(data?.idCustomer as number),
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data !== null &&
            data?.idCustomer !== null,
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

        if (isEqual(data, prevData)) {
            notification(false, "Thông tin loại tác nghiệp không thay đổi");
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idLoaiTacNghiep: parseInt(
                                                      e.target.value
                                                  ),
                                              }
                                            : null
                                    )
                                }
                                optionText="chọn loại tác nghiệp"
                            />
                            <GroupInput
                                labelFor="noiDung"
                                labelText="nội dung"
                                value={data.noiDung}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  noiDung: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="thoiGianThucHien"
                                labelText="thời gian thực hiện"
                                value={data.thoiGianThucHien}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  thoiGianThucHien:
                                                      e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupSelect
                                labelFor="idNguoiLienHe"
                                labelText="người liên hệ"
                                data={
                                    contacts && contacts.length > 0
                                        ? contacts
                                        : []
                                }
                                value={data.idNguoiLienHe}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idNguoiLienHe: parseInt(
                                                      e.target.value
                                                  ),
                                              }
                                            : null
                                    )
                                }
                                optionText="chọn người liên hệ"
                            />
                            <GroupInput
                                labelFor="khachHangPhanHoi"
                                labelText="khách hàng phản hồi"
                                value={data.khachHangPhanHoi}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  khachHangPhanHoi:
                                                      e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="ngayPhanHoi"
                                labelText="ngày phản hồi"
                                value={data.ngayPhanHoi}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  ngayPhanHoi: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
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

export default UpdateCustomerOperationalModal;
