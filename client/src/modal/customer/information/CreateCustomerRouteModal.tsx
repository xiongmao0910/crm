import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { getAllTransportations, getPortsByIdCountry } from "../../../api";
import { Button, GroupCombobox, Modal } from "../../../components";
import { initCreateCustomerRoute } from "../../../constants";
import { TCommonContextProps, useCommon } from "../../../contexts";
import {
    TCreateCustomerRouteRequest,
    TCreateCustomerInfoModalProps,
} from "../../../types";

const CreateCustomerRouteModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerRouteRequest>) => {
    const [data, setData] = useState<TCreateCustomerRouteRequest>({
        ...initCreateCustomerRoute,
        idCustomer: idCustomer as number,
    });

    const { countries }: TCommonContextProps = useCommon();

    const { data: transportations } = useQuery({
        queryKey: "transportations",
        queryFn: getAllTransportations,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: portsFrom } = useQuery({
        queryKey: ["portsFrom", data.idQuocGiaDi],
        queryFn: () => getPortsByIdCountry(data.idQuocGiaDi),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idQuocGiaDi !== -1,
    });

    const { data: portsTo } = useQuery({
        queryKey: ["portsTo", data.idQuocGiaDen],
        queryFn: () => getPortsByIdCountry(data.idQuocGiaDen),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idQuocGiaDen !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCreateCustomerRoute,
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
                            labelFor="idLoaiHinhVanChuyen"
                            labelText="loại hình vận chuyển"
                            data={
                                transportations && transportations.length > 0
                                    ? transportations
                                    : []
                            }
                            optionText="Chọn loại hình vận chuyển"
                            value={transportations?.find(
                                (c) => c.id === data.idLoaiHinhVanChuyen
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idLoaiHinhVanChuyen: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idQuocGiaDi"
                            labelText="quốc gia đi"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            optionText="Chọn quốc gia đi"
                            value={countries?.find(
                                (c) => c.id === data.idQuocGiaDi
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idQuocGiaDi: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idQuocGiaDen"
                            labelText="quốc gia đến"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            optionText="Chọn quốc gia đi"
                            value={countries?.find(
                                (c) => c.id === data.idQuocGiaDen
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idQuocGiaDen: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idCangDi"
                            labelText="cảng đi"
                            data={
                                portsFrom && portsFrom.length > 0
                                    ? portsFrom
                                    : []
                            }
                            optionText="Chọn cảng đi"
                            value={portsFrom?.find(
                                (c) => c.id === data.idCangDi
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idCangDi: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idCangDen"
                            labelText="cảng đi"
                            data={portsTo && portsTo.length > 0 ? portsTo : []}
                            optionText="Chọn cảng đi"
                            value={portsTo?.find(
                                (c) => c.id === data.idCangDen
                            )}
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idCangDen: e.id,
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

export default CreateCustomerRouteModal;
