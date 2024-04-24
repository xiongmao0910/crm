import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { getPortsByIdCountry } from "../../../api";
import { Button, GroupCombobox, GroupInput, Modal } from "../../../components";
import { initCreateCustomerImEx } from "../../../constants";
import {
    TAuthContextProps,
    TCommonContextProps,
    useAuth,
    useCommon,
} from "../../../contexts";
import {
    TCreateCustomerImExRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";

const CreateCustomerImExModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerImExRequest>) => {
    const { currentUser }: TAuthContextProps = useAuth();

    const [data, setData] = useState<TCreateCustomerImExRequest>({
        ...initCreateCustomerImEx,
        idCustomer: idCustomer as number,
        idUserCreate: currentUser?.id as number,
    });

    const { countries }: TCommonContextProps = useCommon();

    const { data: portsFrom } = useQuery({
        queryKey: ["portsFrom", data.idFromCountry],
        queryFn: () => getPortsByIdCountry(data.idFromCountry),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idFromCountry !== -1,
    });

    const { data: portsTo } = useQuery({
        queryKey: ["portsTo", data.idToCountry],
        queryFn: () => getPortsByIdCountry(data.idToCountry),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idToCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCreateCustomerImEx,
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
                        <GroupCombobox
                            labelFor="idFromCountry"
                            labelText="quốc gia đi"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            optionText="Chọn quốc gia đi"
                            value={countries?.find(
                                (c) => c.id === data.idFromCountry
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idFromCountry: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idToCountry"
                            labelText="quốc gia đến"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            optionText="Chọn quốc gia đi"
                            value={countries?.find(
                                (c) => c.id === data.idToCountry
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idToCountry: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idFromPort"
                            labelText="cảng đi"
                            data={
                                portsFrom && portsFrom.length > 0
                                    ? portsFrom
                                    : []
                            }
                            optionText="Chọn cảng đi"
                            value={portsFrom?.find(
                                (c) => c.id === data.idFromPort
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idFromPort: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idToPort"
                            labelText="cảng đi"
                            data={portsTo && portsTo.length > 0 ? portsTo : []}
                            optionText="Chọn cảng đi"
                            value={portsTo?.find((c) => c.id === data.idToPort)}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idToPort: e.id,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    code: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            type="date"
                            labelFor="date"
                            labelText="ngày"
                            value={data.date}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    date: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="type"
                            labelText="loại"
                            value={data.type}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    type: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="vessel"
                            labelText="tàu"
                            value={data.vessel}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    vessel: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="term"
                            labelText="term"
                            value={data.term}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    term: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="commd"
                            labelText="commd"
                            value={data.commd}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    commd: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="vol"
                            labelText="vol"
                            value={data.vol}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    vol: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="unt"
                            labelText="unt"
                            value={data.unt}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    unt: e.target.value,
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

export default CreateCustomerImExModal;
