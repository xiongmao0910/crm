import request from "axios";

import { privateInstance } from "../configs";
import {
    TApiGetWithPageProps,
    TCreateEmployeeRequest,
    TDeleteEmployeeRequest,
    TEmployeeDto,
    TProfileDto,
    TResponseDto,
    TResponsePageDto,
    TUpdateEmployeeRequest,
} from "../types";
import { notification } from "../utilities";

export const getEmployees = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/employee", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TProfileDto[]> =
            response.data;

        return { data, totalRowCount };
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponsePageDto<null> =
                err.response.data;
            console.log(status, message);
        }

        return null;
    }
};

export const getEmployeesDelete = async ({
    start,
    size = 100,
    search = "",
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/employee/delete", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TProfileDto[]> =
            response.data;

        return { data, totalRowCount };
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponsePageDto<null> =
                err.response.data;
            console.log(status, message);
        }

        return null;
    }
};

export const createEmployee = async (payload: TCreateEmployeeRequest) => {
    try {
        const response = await privateInstance.post("/employee", payload);
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

export const updateEmployee = async (payload: TUpdateEmployeeRequest) => {
    try {
        const response = await privateInstance.put(
            `/employee/${payload.idNhanVien}`,
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

export const deleteEmployee = async (payload: TDeleteEmployeeRequest) => {
    try {
        const response = await privateInstance.put(
            `/employee/${payload.idNhanVien}/delete`,
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

export const getEmployeesGroup = async () => {
    try {
        const response = await privateInstance.get("/employee/group");
        const { data }: TResponseDto<TEmployeeDto[]> = response.data;

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
