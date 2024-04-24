import { FormEvent, useState } from "react";

import { Button, GroupSelect, Modal } from "../../../components";
import {
    TCreateCustomerClassifyRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";
import { initCreateCustomerClassify } from "../../../constants";
import { getAllTypeOfCustomers } from "../../../api";
import { useQuery } from "react-query";

const CreateCustomerClassifyModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerClassifyRequest>) => {
    const [data, setData] = useState<TCreateCustomerClassifyRequest>({
        ...initCreateCustomerClassify,
        idCustomer: idCustomer as number,
    });

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

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCreateCustomerClassify,
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
                    <GroupSelect
                        labelFor="idPhanLoaiKhachHang"
                        labelText="phân loại khách hàng"
                        data={types && types.length > 0 ? types : []}
                        value={data.idPhanLoaiKhachHang}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                idPhanLoaiKhachHang: parseInt(e.target.value),
                            }))
                        }
                        optionText="chọn phân loại khách hàng"
                    />
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

export default CreateCustomerClassifyModal;
