import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import { Button, GroupInput, GroupTextarea, Modal } from "../../components";
import {
    TUpdateCategoryModalProps,
    TUpdateOperationalRequest,
} from "../../types";
import { notification } from "../../utilities";
import { hexToRgb, rgbToHex } from "../../utilities";

const UpdateOperationalModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCategoryModalProps<TUpdateOperationalRequest>) => {
    const [data, setData] = useState<TUpdateOperationalRequest | null>(null);

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

    const handleChangeColor = (e: ChangeEvent<HTMLInputElement>) => {
        const hexColor = e.target.value;

        const rgbColor = hexToRgb(hexColor);

        if (rgbColor) {
            setData((prev) =>
                prev
                    ? {
                          ...prev,
                          r: rgbColor.r,
                          g: rgbColor.g,
                          b: rgbColor.b,
                      }
                    : null
            );
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
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            name: e.target.value,
                                        };
                                    }

                                    return null;
                                })
                            }
                        />
                        <GroupInput
                            type="number"
                            labelFor="ngayTuTraKhach"
                            labelText="ngày tự trả khách"
                            value={data.ngayTuTraKhach}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            ngayTuTraKhach: parseInt(
                                                e.target.value
                                            ),
                                        };
                                    }

                                    return null;
                                })
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

export default UpdateOperationalModal;
