import request from "axios";

import { privateInstance } from "../configs";
import {
    TBusinessDto,
    TCountryDto,
    TDepartmentDto,
    TMajorDto,
    TOfficeDto,
    TOperationalDto,
    TPositionDto,
    TResponseDto,
    TTransportationDto,
    TTypeOfCustomerDto,
} from "../types";

export const getAllPositions = async () => {
    try {
        const response = await privateInstance.get("/position/all");
        const { data }: TResponseDto<TPositionDto[]> = response.data;

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

export const getAllBusinesses = async () => {
    try {
        const response = await privateInstance.get("/business/all");
        const { data }: TResponseDto<TBusinessDto[]> = response.data;

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

export const getAllDepartments = async () => {
    try {
        const response = await privateInstance.get("/department/all");
        const { data }: TResponseDto<TDepartmentDto[]> = response.data;

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

export const getAllOffices = async () => {
    try {
        const response = await privateInstance.get("/office/all");
        const { data }: TResponseDto<TOfficeDto[]> = response.data;

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

export const getAllCountries = async () => {
    try {
        const response = await privateInstance.get("/country/all");
        const { data }: TResponseDto<TCountryDto[]> = response.data;

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

export const getAllMajors = async () => {
    try {
        const response = await privateInstance.get("/major/all");
        const { data }: TResponseDto<TMajorDto[]> = response.data;

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

export const getAllTypeOfCustomers = async () => {
    try {
        const response = await privateInstance.get("/typeofcustomer/all");
        const { data }: TResponseDto<TTypeOfCustomerDto[]> = response.data;

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

export const getAllTransportations = async () => {
    try {
        const response = await privateInstance.get("/transportation/all");
        const { data }: TResponseDto<TTransportationDto[]> = response.data;

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

export const getAllOperationals = async () => {
    try {
        const response = await privateInstance.get("/operational/all");
        const { data }: TResponseDto<TOperationalDto[]> = response.data;

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
