import request from "axios";

import { privateInstance } from "../configs";
import { TProfileDto, TResponseDto, TUpdateProfileRequest } from "../types";
import { notification } from "../utilities";

export const getProfileById = async (id: number) => {
    try {
        const response = await privateInstance.get(`/profile/${id}`);
        const { data }: TResponseDto<TProfileDto> = response.data;

        return data;
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponseDto<null> = err.response.data;
            console.log(status, message);
        }

        return null;
    }
};

export const updateProfile = async (payload: TUpdateProfileRequest) => {
    try {
        const response = await privateInstance.put(
            `/profile/${payload.id}`,
            payload
        );

        const { status, message }: TResponseDto<null> = response.data;

        notification(status, message);

        return status;
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponseDto<null> = err.response.data;
            notification(status, message);
        }

        return false;
    }
};
