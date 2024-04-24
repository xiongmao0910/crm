import { FormEvent, useRef, useState } from "react";
import { useQuery } from "react-query";

import { getEmployeesGroup } from "../../api";
import { TAuthContextProps, useAuth } from "../../contexts";
import { Button, GroupCombobox, GroupInput, Modal } from "../../components";
import {
    TDeliveryCustomerModalProps,
    TDeliveryCustomerRequest,
} from "../../types";
import { notification } from "../../utilities";

const DeliveryCustomerModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomers,
}: TDeliveryCustomerModalProps) => {
    const inputRef = useRef<HTMLInputElement | null>(null);

    const [idEmployee, setIdEmployee] = useState<number>(-1);

    const { currentUser }: TAuthContextProps = useAuth();

    const { data: employees } = useQuery({
        queryKey: ["employees", currentUser],
        queryFn: getEmployeesGroup,
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            currentUser !== null,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (idEmployee === -1) {
            notification(false, "Vui lòng chọn nhân viên");
            return;
        }

        const payload: TDeliveryCustomerRequest = {
            idNhanVienSale: idEmployee,
            idUserGiaoViec: currentUser?.id as number,
            idCustomers,
            thongTinGiaoViec: inputRef.current?.value ?? "",
        };

        const result = await onSubmit(payload);

        if (result) setIdEmployee(-1);
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
                <form className="grid gap-4" onSubmit={handleSubmit}>
                    <GroupCombobox
                        labelFor="idNhanVienSale"
                        labelText="nhân viên"
                        data={
                            employees && employees.length > 0 ? employees : []
                        }
                        optionText="Chọn nhân viên"
                        value={
                            idEmployee === -1
                                ? { id: idEmployee, nameVI: "chọn nhân viên" }
                                : employees?.find((e) => e.id === idEmployee)
                        }
                        updateValue={(e) => setIdEmployee(e.id)}
                    />
                    <GroupInput
                        ref={inputRef}
                        labelFor="thongTinGiaoViec"
                        labelText="Thông tin giao việc"
                    />
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="giao khách"
                            buttonVariant="contained"
                            buttonColor="cyan"
                            buttonRounded="xl"
                            isLoading={isLoading}
                            textIsLoading="đang giao khách"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            onClick={onClose}
                        />
                    </div>
                </form>
            </Modal>
        </>
    );
};

export default DeliveryCustomerModal;
