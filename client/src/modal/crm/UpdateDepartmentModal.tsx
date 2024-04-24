import { FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import { TCommonContextProps, useCommon } from "../../contexts";
import { Button, GroupSelect, GroupTextarea, Modal } from "../../components";
import {
    TUpdateCategoryModalProps,
    TUpdateDepartmentRequest,
} from "../../types";
import { notification } from "../../utilities";

const UpdateDepartmentModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCategoryModalProps<TUpdateDepartmentRequest>) => {
    const [data, setData] = useState<TUpdateDepartmentRequest | null>(null);

    const { offices }: TCommonContextProps = useCommon();

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
                    <form className="grid gap-2" onSubmit={handleSubmit}>
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
                        <GroupSelect
                            labelFor="idVanPhong"
                            labelText="văn phòng"
                            data={offices && offices.length > 0 ? offices : []}
                            value={data.idVanPhong}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              idVanPhong: parseInt(
                                                  e.target.value
                                              ),
                                          }
                                        : null
                                )
                            }
                            optionText="chọn văn phòng"
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

export default UpdateDepartmentModal;
