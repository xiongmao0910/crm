import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { Button, GroupInput, GroupSelect, Modal } from "../../../components";
import {
    TCreateCustomerEvaluateRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";
import { initCreateCustomerEvaluate } from "../../../constants";
import { TAuthContextProps, useAuth } from "../../../contexts";
import { getAllTypeOfCustomers } from "../../../api";

const CreateCustomerEvaluateModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerEvaluateRequest>) => {
    const { currentUser }: TAuthContextProps = useAuth();

    const [data, setData] = useState<TCreateCustomerEvaluateRequest>({
        ...initCreateCustomerEvaluate,
        idCustomer: idCustomer as number,
        idUserCreate: currentUser?.id as number,
    });

    const { data: typeofcustomers } = useQuery({
        queryKey: "typeofcustomers",
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
                ...initCreateCustomerEvaluate,
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
                            labelFor="idCustomerType"
                            labelText="loại đánh giá"
                            data={
                                typeofcustomers && typeofcustomers.length > 0
                                    ? typeofcustomers
                                    : []
                            }
                            value={data.idCustomerType}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idCustomerType: parseInt(e.target.value),
                                }))
                            }
                            optionText="chọn loại đánh giá"
                        />
                        <GroupInput
                            labelFor="ghiChu"
                            labelText="ghi chú"
                            value={data.ghiChu}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    ghiChu: e.target.value,
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

export default CreateCustomerEvaluateModal;
