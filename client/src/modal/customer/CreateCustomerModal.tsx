import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { getCitiesByIdCountry } from "../../api";
import {
    TAuthContextProps,
    TCommonContextProps,
    useAuth,
    useCommon,
} from "../../contexts";
import {
    Button,
    GroupCombobox,
    GroupInput,
    GroupTextarea,
    Modal,
} from "../../components";
import { TCreateCustomerModalProps, TCreateCustomerRequest } from "../../types";
import { initCustomer } from "../../constants";

const CreateCustomerModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateCustomerModalProps) => {
    const [data, setData] = useState<TCreateCustomerRequest>(initCustomer);

    const { currentUser }: TAuthContextProps = useAuth();
    const { countries }: TCommonContextProps = useCommon();

    const { data: cities } = useQuery({
        queryKey: ["cities", data.idQuocGia],
        queryFn: () => getCitiesByIdCountry(data.idQuocGia),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idQuocGia !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        data.idUserCreate = currentUser?.id as number;
        const result = await onSubmit(data);

        if (result) setData(initCustomer);
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
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            required={true}
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    code: e.target.value,
                                }))
                            }
                        />
                        <GroupTextarea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameVI: e.target.value,
                                }))
                            }
                        />
                        <GroupTextarea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameEN: e.target.value,
                                }))
                            }
                        />
                        <GroupTextarea
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            required={true}
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressVI: e.target.value,
                                }))
                            }
                        />
                        <GroupTextarea
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressEN: e.target.value,
                                }))
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
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idQuocGia: e.id,
                                }))
                            }
                        />
                        <GroupCombobox
                            labelFor="idCity"
                            labelText="thành phố"
                            data={cities && cities.length > 0 ? cities : []}
                            optionText="Chọn thành phố"
                            updateValue={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    idCity: e.id,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="taxCode"
                            labelText="mã số thuế"
                            value={data.taxCode}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    taxCode: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="phone"
                            labelText="số điện thoại"
                            value={data.phone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    phone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="fax"
                            labelText="FAX"
                            value={data.fax}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    fax: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="email"
                            labelText="Thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    email: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="website"
                            labelText="Trang web"
                            value={data.website}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    website: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="note"
                            labelText="Ghi chú"
                            value={data.note}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    note: e.target.value,
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

export default CreateCustomerModal;
