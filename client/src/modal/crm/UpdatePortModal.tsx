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
import { TUpdateCategoryModalProps, TUpdatePortRequest } from "../../types";
import { notification } from "../../utilities";

const UpdatePortModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCategoryModalProps<TUpdatePortRequest>) => {
    const [data, setData] = useState<TUpdatePortRequest | null>(null);

    const { countries }: TCommonContextProps = useCommon();

    const { data: cities } = useQuery({
        queryKey: ["cities", data?.idQuocGia],
        queryFn: () => getCitiesByIdCountry(data?.idQuocGia as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data?.idQuocGia != null && data?.idQuocGia !== -1,
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
                    <form className="grid gap-4" onSubmit={handleSubmit}>
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            required={true}
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              code: e.target.value,
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
                                    prev
                                        ? {
                                              ...prev,
                                              taxCode: e.target.value,
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
                                    prev
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
                                    prev
                                        ? {
                                              ...prev,
                                              nameEN: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextarea
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            required={true}
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              addressVI: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextarea
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
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
                            value={countries?.find(
                                (c) => c.id === data.idQuocGia
                            )}
                            optionText="Chọn quốc gia"
                            updateValue={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              idQuocGia: e.id,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupCombobox
                            labelFor="idCity"
                            labelText="thành phố"
                            data={cities && cities.length > 0 ? cities : []}
                            value={cities?.find((c) => c.id === data.idCity)}
                            optionText="Chọn thành phố"
                            updateValue={(e) =>
                                setData((prev) =>
                                    prev
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
                                    prev
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
                            labelText="FAX"
                            value={data.fax}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
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
                                    prev
                                        ? {
                                              ...prev,
                                              email: e.target.value,
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
                                    prev
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
                                    prev
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

export default UpdatePortModal;
