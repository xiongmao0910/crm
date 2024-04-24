import { FormEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import {
    Button,
    GroupInput,
    GroupSelect,
    GroupTextarea,
    Modal,
} from "../../components";
import { TUpdateCategoryModalProps, TUpdateCityRequest } from "../../types";
import { notification } from "../../utilities";
import { TCommonContextProps, useCommon } from "../../contexts";

const UpdateCityModal = ({
    isOpen,
    onClose,
    width = "lg",
    height,
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
}: TUpdateCategoryModalProps<TUpdateCityRequest>) => {
    const [data, setData] = useState<TUpdateCityRequest | null>(null);

    const { countries }: TCommonContextProps = useCommon();

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
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            code: e.target.value,
                                        };
                                    }
                                    return null;
                                })
                            }
                        />
                        <GroupTextarea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            nameVI: e.target.value,
                                        };
                                    }

                                    return null;
                                })
                            }
                        />
                        <GroupTextarea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            nameEN: e.target.value,
                                        };
                                    }

                                    return null;
                                })
                            }
                        />
                        <GroupSelect
                            labelFor="idQuocGia"
                            labelText="quốc gia"
                            data={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            value={data.idQuocGia}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              idQuocGia: parseInt(
                                                  e.target.value
                                              ),
                                          }
                                        : null
                                )
                            }
                            optionText="chọn quốc gia"
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

export default UpdateCityModal;
