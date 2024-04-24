import { FormEvent, useState } from "react";
import { useQuery } from "react-query";

import { getCitiesByIdCountry } from "../../api";
import {
    Button,
    GroupCombobox,
    GroupInput,
    GroupTextarea,
    Modal,
} from "../../components";
import { TCreateCategoryModalProps, TCreateOfficeRequest } from "../../types";
import { initOffice } from "../../constants";
import { TCommonContextProps, useCommon } from "../../contexts";

const CreateOfficeModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateCategoryModalProps<TCreateOfficeRequest>) => {
    const [data, setData] = useState<TCreateOfficeRequest>(initOffice);

    const { countries }: TCommonContextProps = useCommon();

    const { data: cities } = useQuery({
        queryKey: ["cities", data.idCountry],
        queryFn: () => getCitiesByIdCountry(data.idCountry),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled: data.idCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result) setData(initOffice);
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
                    <GroupInput
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
                    <GroupInput
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
                            countries && countries.length > 0 ? countries : []
                        }
                        optionText="Chọn quốc gia"
                        updateValue={(e) =>
                            setData((prev) => ({
                                ...prev,
                                idCountry: e.id,
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
                        labelText="fax"
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
                        labelText="thư điện tử"
                        value={data.email}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                email: e.target.value,
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
                        labelFor="website"
                        labelText="trang web"
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
                        labelText="ghi chú"
                        value={data.note}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                note: e.target.value,
                            }))
                        }
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

export default CreateOfficeModal;
