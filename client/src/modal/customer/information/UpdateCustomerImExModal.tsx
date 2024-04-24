import { FormEvent, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getPortsByIdCountry } from "../../../api";
import { Button, GroupCombobox, GroupInput, Modal } from "../../../components";
import { TCommonContextProps, useCommon } from "../../../contexts";
import {
    TUpdateCustomerImExRequest,
    TUpdateCustomerInfoModalProps,
} from "../../../types";
import { notification } from "../../../utilities";

const UpdateCustomerImExModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCustomerInfoModalProps<TUpdateCustomerImExRequest>) => {
    const [data, setData] = useState<TUpdateCustomerImExRequest | null>(null);

    const { countries }: TCommonContextProps = useCommon();

    const { data: portsFrom } = useQuery({
        queryKey: ["portsFrom", data?.idFromCountry],
        queryFn: () => getPortsByIdCountry(data?.idFromCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data !== null && data?.idFromCountry !== -1,
    });

    const { data: portsTo } = useQuery({
        queryKey: ["portsTo", data?.idToCountry],
        queryFn: () => getPortsByIdCountry(data?.idToCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data !== null && data?.idToCountry !== -1,
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idFromCountry: e.id,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idToCountry: e.id,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idFromPort: e.id,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupCombobox
                                labelFor="idToPort"
                                labelText="cảng đi"
                                data={
                                    portsTo && portsTo.length > 0 ? portsTo : []
                                }
                                optionText="Chọn cảng đi"
                                value={portsTo?.find(
                                    (c) => c.id === data.idToPort
                                )}
                                updateValue={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idToPort: e.id,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="code"
                                labelText="mã"
                                value={data.code}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  code: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="date"
                                labelText="ngày"
                                value={data.date}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  date: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="type"
                                labelText="loại"
                                value={data.type}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  type: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="vessel"
                                labelText="tàu"
                                value={data.vessel}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  vessel: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="term"
                                labelText="term"
                                value={data.term}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  term: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="commd"
                                labelText="commd"
                                value={data.commd}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  commd: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="vol"
                                labelText="vol"
                                value={data.vol}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  vol: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="unt"
                                labelText="unt"
                                value={data.unt}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  unt: e.target.value,
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

export default UpdateCustomerImExModal;
