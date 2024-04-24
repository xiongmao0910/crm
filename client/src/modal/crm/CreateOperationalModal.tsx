import { ChangeEvent, FormEvent, useState } from "react";

import { Button, GroupInput, GroupTextarea, Modal } from "../../components";
import {
    TCreateCategoryModalProps,
    TCreateOperationalRequest,
} from "../../types";
import { initOperational } from "../../constants";
import { hexToRgb, rgbToHex } from "../../utilities";

const CreateOperationalModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
}: TCreateCategoryModalProps<TCreateOperationalRequest>) => {
    const [data, setData] =
        useState<TCreateOperationalRequest>(initOperational);

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const result = await onSubmit(data);

        if (result) setData(initOperational);
    };

    const handleChangeColor = (e: ChangeEvent<HTMLInputElement>) => {
        const hexColor = e.target.value;

        const rgbColor = hexToRgb(hexColor);

        if (rgbColor) {
            setData((prev) => ({
                ...prev,
                r: rgbColor.r,
                g: rgbColor.g,
                b: rgbColor.b,
            }));
        }
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
                        type="color"
                        labelFor="color"
                        labelText="màu hiển thị"
                        value={rgbToHex(data.r, data.g, data.b)}
                        onChange={handleChangeColor}
                    />
                    <GroupTextarea
                        labelFor="name"
                        labelText="tên"
                        required={true}
                        value={data.name}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                name: e.target.value,
                            }))
                        }
                    />
                    <GroupInput
                        type="number"
                        labelFor="ngayTuTraKhach"
                        labelText="ngày tự trả khách"
                        value={data.ngayTuTraKhach}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                ngayTuTraKhach: parseInt(e.target.value),
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

export default CreateOperationalModal;
