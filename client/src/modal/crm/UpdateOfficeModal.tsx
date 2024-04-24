import { FormEvent, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getCitiesByIdCountry } from "../../api";
import { TCommonContextProps, useCommon } from "../../contexts";
import {
    Button,
    GroupCombobox,
    GroupInput,
    GroupTextarea,
    Modal,
} from "../../components";
import { TUpdateCategoryModalProps, TUpdateOfficeRequest } from "../../types";
import { notification } from "../../utilities";

const UpdateOfficeModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCategoryModalProps<TUpdateOfficeRequest>) => {
    const [data, setData] = useState<TUpdateOfficeRequest | null>(null);

    const { countries }: TCommonContextProps = useCommon();

    const { data: cities, isLoading: isFetching } = useQuery({
        queryKey: ["cities", data?.idCountry],
        queryFn: () => getCitiesByIdCountry(data?.idCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data?.idCountry != null && data?.idCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (isEqual(data, prevData)) {
            notification(false, "Thông tin không thay đổi");
            return;
        }

        if (data) {
            const payload: TUpdateOfficeRequest = {
                id: data.id,
                code: data.code,
                idCountry: data.idCountry,
                idCity: data.idCity,
                nameVI: data.nameVI,
                nameEN: data.nameEN,
                addressVI: data.addressVI,
                addressEN: data.addressEN,
                phone: data.phone,
                fax: data.fax,
                email: data.email,
                website: data.website,
                taxCode: data.taxCode,
                note: data.note,
            };

            await onSubmit(payload);
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
                {data !== null && !isFetching && (
                    <form className="grid gap-2" onSubmit={handleSubmit}>
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            required={true}
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              code: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextarea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              nameVI: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextarea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              nameEN: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            required={true}
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              addressVI: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              addressEN: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupCombobox
                            labelFor="idCountry"
                            labelText="quốc gia"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            optionText="Chọn quốc gia"
                            value={countries?.find(
                                (c) => c.id === data.idCountry
                            )}
                            updateValue={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              idCountry: e.id,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupCombobox
                            labelFor="idCity"
                            labelText="thành phố"
                            data={cities && cities.length > 0 ? cities : []}
                            optionText="Chọn thành phố"
                            value={cities?.find((c) => c.id === data.idCity)}
                            updateValue={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              idCity: e.id,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="phone"
                            labelText="số điện thoại"
                            value={data.phone}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              phone: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="fax"
                            labelText="fax"
                            value={data.fax}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              fax: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="email"
                            labelText="thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              email: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="taxCode"
                            labelText="mã số thuế"
                            value={data.taxCode}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              taxCode: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="website"
                            labelText="trang web"
                            value={data.website}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              website: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="note"
                            labelText="ghi chú"
                            value={data.note}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              note: e.target.value,
                                          }
                                        : null
                                )
                            }
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

export default UpdateOfficeModal;
