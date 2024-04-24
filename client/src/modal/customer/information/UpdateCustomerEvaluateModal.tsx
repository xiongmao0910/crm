import { FormEvent, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { Button, GroupInput, GroupSelect, Modal } from "../../../components";
import {
    TUpdateCustomerEvaluateRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";
import { getAllTypeOfCustomers } from "../../../api";

const UpdateCustomerEvaluateModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerEvaluateRequest>) => {
    const [data, setData] = useState<TUpdateCustomerEvaluateRequest | null>(
        null
    );

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

        if (isEqual(data, prevData)) {
            notification(false, "Thông tin đánh giá không thay đổi");
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
                                labelFor="idCustomerType"
                                labelText="loại đánh giá"
                                data={
                                    typeofcustomers &&
                                    typeofcustomers.length > 0
                                        ? typeofcustomers
                                        : []
                                }
                                value={data.idCustomerType}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idCustomerType: parseInt(
                                                      e.target.value
                                                  ),
                                              }
                                            : null
                                    )
                                }
                                optionText="chọn loại đánh giá"
                            />
                            <GroupInput
                                labelFor="ghiChu"
                                labelText="ghi chú"
                                value={data.ghiChu}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  ghiChu: e.target.value,
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

export default UpdateCustomerEvaluateModal;
