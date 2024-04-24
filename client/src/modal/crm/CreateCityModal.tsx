import { FormEvent, useState } from "react";

import {
    Button,
    GroupInput,
    GroupSelect,
    GroupTextarea,
    Modal,
} from "../../components";
import { TCreateCategoryModalProps, TCreateCityRequest } from "../../types";
import { initCity } from "../../constants";
import { TCommonContextProps, useCommon } from "../../contexts";

const CreateCityModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateCategoryModalProps<TCreateCityRequest>) => {
    const [data, setData] = useState<TCreateCityRequest>(initCity);

    const { countries }: TCommonContextProps = useCommon();

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result) setData(initCity);
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
                    <GroupSelect
                        labelFor="idQuocGia"
                        labelText="quốc gia"
                        data={
                            countries && countries.length > 0 ? countries : []
                        }
                        value={data.idQuocGia}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                idQuocGia: parseInt(e.target.value),
                            }))
                        }
                        optionText="chọn quốc gia"
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

export default CreateCityModal;
