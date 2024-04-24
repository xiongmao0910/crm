import request from "axios";

import { privateInstance } from "../configs";
import {
    TApiGetWithPageProps,
    TBusinessDto,
    TCityDto,
    TCountryDto,
    TCreateBusinessRequest,
    TCreateCityRequest,
    TCreateCountryRequest,
    TCreateCustomerTypeRequest,
    TCreateDepartmentRequest,
    TCreateMajorRequest,
    TCreateOfficeRequest,
    TCreateOperationalRequest,
    TCreatePortRequest,
    TCreatePositionRequest,
    TCreateTransportationRequest,
    TCreateTypeOfCustomerRequest,
    TCustomerTypeDto,
    TDepartmentDto,
    TMajorDto,
    TOfficeDto,
    TOperationalDto,
    TPortDto,
    TPositionDto,
    TResponseDto,
    TResponsePageDto,
    TTransportationDto,
    TTypeOfCustomerDto,
    TUpdateBusinessRequest,
    TUpdateCityRequest,
    TUpdateCountryRequest,
    TUpdateCustomerTypeRequest,
    TUpdateDepartmentRequest,
    TUpdateMajorRequest,
    TUpdateOfficeRequest,
    TUpdateOperationalRequest,
    TUpdatePortRequest,
    TUpdatePositionRequest,
    TUpdateTransportationRequest,
    TUpdateTypeOfCustomerRequest,
} from "../types";
import { notification } from "../utilities";

// City
export const getCities = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/city", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCityDto[]> =
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

export const getCitiesByIdCountry = async (id: number) => {
    try {
        const response = await privateInstance.get(`/city/${id}/country`);

        const { data }: TResponseDto<TCityDto[]> = response.data;

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

export const createCity = async (payload: TCreateCityRequest) => {
    try {
        const response = await privateInstance.post("/city", payload);
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

export const updateCity = async (payload: TUpdateCityRequest) => {
    try {
        const response = await privateInstance.put(
            `/city/${payload.id}`,
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

export const deleteCity = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/city/${id}`);
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

// Country
export const getCountries = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/country", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCountryDto[]> =
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

export const createCountry = async (payload: TCreateCountryRequest) => {
    try {
        const response = await privateInstance.post("/country", payload);
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

export const updateCountry = async (payload: TUpdateCountryRequest) => {
    try {
        const response = await privateInstance.put(
            `/country/${payload.id}`,
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

export const deleteCountry = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/country/${id}`);
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

// Port
export const getPorts = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/port", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TPortDto[]> =
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

export const getPortsByIdCountry = async (idCountry: number) => {
    try {
        const response = await privateInstance.get(`port/${idCountry}/all`);
        const { data }: TResponseDto<TPortDto[]> = response.data;

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

export const createPort = async (payload: TCreatePortRequest) => {
    try {
        const response = await privateInstance.post("/port", payload);
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

export const updatePort = async (payload: TUpdatePortRequest) => {
    try {
        const response = await privateInstance.put(
            `/port/${payload.id}`,
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

export const deletePort = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/port/${id}`);
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

// Position
export const getPositions = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/position", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TPositionDto[]> =
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

export const createPosition = async (payload: TCreatePositionRequest) => {
    try {
        const response = await privateInstance.post("/position", payload);
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

export const updatePosition = async (payload: TUpdatePositionRequest) => {
    try {
        const response = await privateInstance.put(
            `/position/${payload.id}`,
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

export const deletePosition = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/position/${id}`);
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

// Department
export const getDepartments = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/department", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TDepartmentDto[]> =
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

export const createDepartment = async (payload: TCreateDepartmentRequest) => {
    try {
        const response = await privateInstance.post("/department", payload);
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

export const updateDepartment = async (payload: TUpdateDepartmentRequest) => {
    try {
        const response = await privateInstance.put(
            `/department/${payload.id}`,
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

export const deleteDepartment = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/department/${id}`);
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

// Office
export const getOffices = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/office", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TOfficeDto[]> =
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

export const createOffice = async (payload: TCreateOfficeRequest) => {
    try {
        const response = await privateInstance.post("/office", payload);
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

export const updateOffice = async (payload: TUpdateOfficeRequest) => {
    try {
        const response = await privateInstance.put(
            `/office/${payload.id}`,
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

export const deleteOffice = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/office/${id}`);
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

// Business
export const getBusinesses = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/business", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TBusinessDto[]> =
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

export const createBusiness = async (payload: TCreateBusinessRequest) => {
    try {
        const response = await privateInstance.post("/business", payload);
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

export const updateBusiness = async (payload: TUpdateBusinessRequest) => {
    try {
        const response = await privateInstance.put(
            `/business/${payload.id}`,
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

export const deleteBusiness = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/business/${id}`);
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

// Transportation
export const getTransportations = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/transportation", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TTransportationDto[]> =
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

export const createTransportation = async (
    payload: TCreateTransportationRequest
) => {
    try {
        const response = await privateInstance.post("/transportation", payload);
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

export const updateTransportation = async (
    payload: TUpdateTransportationRequest
) => {
    try {
        const response = await privateInstance.put(
            `/transportation/${payload.id}`,
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

export const deleteTransportation = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/transportation/${id}`);
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

// Operational
export const getOperationals = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/operational", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TOperationalDto[]> =
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

export const createOperational = async (payload: TCreateOperationalRequest) => {
    try {
        const response = await privateInstance.post("/operational", payload);
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

export const updateOperational = async (payload: TUpdateOperationalRequest) => {
    try {
        const response = await privateInstance.put(
            `/operational/${payload.id}`,
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

export const deleteOperational = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/operational/${id}`);
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

// Major
export const getMajors = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/major", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TMajorDto[]> =
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

export const createMajor = async (payload: TCreateMajorRequest) => {
    try {
        const response = await privateInstance.post("/major", payload);
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

export const updateMajor = async (payload: TUpdateMajorRequest) => {
    try {
        const response = await privateInstance.put(
            `/major/${payload.id}`,
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

export const deleteMajor = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/major/${id}`);
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

// TypeOfCustomer
export const getTypeOfCustomers = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/typeofcustomer", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TTypeOfCustomerDto[]> =
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

export const createTypeOfCustomer = async (
    payload: TCreateTypeOfCustomerRequest
) => {
    try {
        const response = await privateInstance.post("/typeofcustomer", payload);
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

export const updateTypeOfCustomer = async (
    payload: TUpdateTypeOfCustomerRequest
) => {
    try {
        const response = await privateInstance.put(
            `/typeofcustomer/${payload.id}`,
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

export const deleteTypeOfCustomer = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/typeofcustomer/${id}`);
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

// CustomerTypes
export const getCustomerTypes = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customertypes", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerTypeDto[]> =
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

export const createCustomerType = async (
    payload: TCreateCustomerTypeRequest
) => {
    try {
        const response = await privateInstance.post("/customertypes", payload);
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

export const updateCustomerType = async (
    payload: TUpdateCustomerTypeRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customertypes/${payload.id}`,
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

export const deleteCustomerType = async (id: number) => {
    try {
        const response = await privateInstance.delete(`/customertypes/${id}`);
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
