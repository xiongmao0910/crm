import { FormEvent, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllMajors } from "../../../api";
import { Button, GroupSelect, Modal } from "../../../components";
import {
    TUpdateCustomerMajorRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";

const UpdateCustomerMajorModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerMajorRequest>) => {
    const [data, setData] = useState<TUpdateCustomerMajorRequest | null>(null);

    const { data: majors } = useQuery({
        queryKey: "majors",
        queryFn: getAllMajors,
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
            notification(false, "Thông tin nghiệp vụ không thay đổi");
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
                            labelFor="idNghiepVu"
                            labelText="nghiệp vụ"
                            data={majors && majors.length > 0 ? majors : []}
                            value={data.idNghiepVu}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              idNghiepVu: parseInt(
                                                  e.target.value
                                              ),
                                          }
                                        : null
                                )
                            }
                            optionText="chọn nghiệp vụ"
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

export default UpdateCustomerMajorModal;
