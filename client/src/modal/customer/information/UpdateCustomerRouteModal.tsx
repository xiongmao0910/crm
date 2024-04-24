import { FormEvent, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllTransportations, getPortsByIdCountry } from "../../../api";
import { Button, GroupCombobox, Modal } from "../../../components";
import { TCommonContextProps, useCommon } from "../../../contexts";
import {
    TUpdateCustomerRouteRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";

const UpdateCustomerRouteModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerRouteRequest>) => {
    const [data, setData] = useState<TUpdateCustomerRouteRequest | null>(null);

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
        queryKey: ["portsFrom", data?.idQuocGiaDi],
        queryFn: () => getPortsByIdCountry(data?.idQuocGiaDi as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data !== null && data.idQuocGiaDi !== -1,
    });

    const { data: portsTo } = useQuery({
        queryKey: ["portsTo", data?.idQuocGiaDen],
        queryFn: () => getPortsByIdCountry(data?.idQuocGiaDen as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data !== null && data.idQuocGiaDen !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (isEqual(data, prevData)) {
            notification(false, "Thông tin tuyến hàng không thay đổi");
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
                            <GroupCombobox
                                labelFor="idLoaiHinhVanChuyen"
                                labelText="loại hình vận chuyển"
                                data={
                                    transportations &&
                                    transportations.length > 0
                                        ? transportations
                                        : []
                                }
                                optionText="Chọn loại hình vận chuyển"
                                value={transportations?.find(
                                    (c) => c.id === data.idLoaiHinhVanChuyen
                                )}
                                updateValue={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idLoaiHinhVanChuyen: e.id,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupCombobox
                                labelFor="idLoaiHinhVanChuyen"
                                labelText="loại hình vận chuyển"
                                data={
                                    transportations &&
                                    transportations.length > 0
                                        ? transportations
                                        : []
                                }
                                optionText="Chọn loại hình vận chuyển"
                                value={transportations?.find(
                                    (c) => c.id === data.idLoaiHinhVanChuyen
                                )}
                                updateValue={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idLoaiHinhVanChuyen: e.id,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idQuocGiaDi: e.id,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idQuocGiaDen: e.id,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idCangDi: e.id,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupCombobox
                                labelFor="idCangDen"
                                labelText="cảng đi"
                                data={
                                    portsTo && portsTo.length > 0 ? portsTo : []
                                }
                                optionText="Chọn cảng đi"
                                value={portsTo?.find(
                                    (c) => c.id === data.idCangDen
                                )}
                                updateValue={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idCangDen: e.id,
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

export default UpdateCustomerRouteModal;
