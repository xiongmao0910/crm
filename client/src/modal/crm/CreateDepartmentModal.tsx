import { FormEvent, useState } from "react";

import { TCommonContextProps, useCommon } from "../../contexts";
import { Button, GroupSelect, GroupTextarea, Modal } from "../../components";
import {
    TCreateCategoryModalProps,
    TCreateDepartmentRequest,
} from "../../types";
import { initDepartment } from "../../constants";

const CreateDepartmentModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateCategoryModalProps<TCreateDepartmentRequest>) => {
    const [data, setData] = useState<TCreateDepartmentRequest>(initDepartment);

    const { offices }: TCommonContextProps = useCommon();

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result) setData(initDepartment);
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
                        labelFor="idVanPhong"
                        labelText="văn phòng"
                        data={offices && offices.length > 0 ? offices : []}
                        value={data.idVanPhong}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                idVanPhong: parseInt(e.target.value),
                            }))
                        }
                        optionText="chọn văn phòng"
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

export default CreateDepartmentModal;
