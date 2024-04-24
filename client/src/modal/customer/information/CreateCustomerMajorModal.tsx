import { FormEvent, useState } from "react";

import { Button, GroupSelect, Modal } from "../../../components";
import {
    TCreateCustomerMajorRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";
import { initCreateCustomerMajor } from "../../../constants";
import { getAllMajors } from "../../../api";
import { useQuery } from "react-query";

const CreateCustomerMajorModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerMajorRequest>) => {
    const [data, setData] = useState<TCreateCustomerMajorRequest>({
        ...initCreateCustomerMajor,
        idCustomer: idCustomer as number,
    });

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

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCreateCustomerMajor,
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
                        labelFor="idNghiepVu"
                        labelText="nghiệp vụ"
                        data={majors && majors.length > 0 ? majors : []}
                        value={data.idNghiepVu}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                idNghiepVu: parseInt(e.target.value),
                            }))
                        }
                        optionText="chọn nghiệp vụ"
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

export default CreateCustomerMajorModal;
