import request from "axios";

import {
    TChangePasswordRequest,
    TLogInRequest,
    TProfileDto,
    TResponseDto,
} from "../types";
import { privateInstance } from "../configs";
import { notification } from "../utilities";

export const logInWithUsernameAndPassword = async (payload: TLogInRequest) => {
    try {
        const response = await privateInstance.post("/auth/login", payload);
        const {
            status,
            message,
            data,
        }: TResponseDto<{ profile: TProfileDto; token: string }> =
            response.data;

        notification(status, message);

        return data;
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponseDto<null> = err.response.data;
            notification(status, message);
        }

        return null;
    }
};

export const changePassword = async (payload: TChangePasswordRequest) => {
    try {
        const response = await privateInstance.put(
            `/profile/${payload.id}/change-password`,
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
