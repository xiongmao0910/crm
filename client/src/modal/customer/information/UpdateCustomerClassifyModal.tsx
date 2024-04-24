import { FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import { Button, GroupSelect, Modal } from "../../../components";
import {
    TUpdateCustomerClassifyRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";
import { getAllTypeOfCustomers } from "../../../api";
import { useQuery } from "react-query";

const UpdateCustomerClassifyModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerClassifyRequest>) => {
    const [data, setData] = useState<TUpdateCustomerClassifyRequest | null>(
        null
    );

    const { data: types } = useQuery({
        queryKey: "types",
        queryFn: getAllTypeOfCustomers,
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
            notification(false, "Thông tin loại khách hàng không thay đổi");
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
                        <GroupSelect
                            labelFor="idPhanLoaiKhachHang"
                            labelText="phân loại khách hàng"
                            data={types && types.length > 0 ? types : []}
                            value={data.idPhanLoaiKhachHang}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              idPhanLoaiKhachHang: parseInt(
                                                  e.target.value
                                              ),
                                          }
                                        : null
                                )
                            }
                            optionText="chọn phân loại khách hàng"
                        />
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

export default UpdateCustomerClassifyModal;
