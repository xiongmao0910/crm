import request from "axios";

import { privateInstance } from "../configs";
import {
    TAcceptCustomerRequest,
    TApiGetWithPageProps,
    TApiResponseExportProps,
    TChooseCustomerRequest,
    TCreateCustomerClassifyRequest,
    TCreateCustomerContactRequest,
    TCreateCustomerEvaluateRequest,
    TCreateCustomerImExRequest,
    TCreateCustomerMajorRequest,
    TCreateCustomerOperationalRequest,
    TCreateCustomerRequest,
    TCreateCustomerRouteRequest,
    TCustomerClassifyDto,
    TCustomerContactDto,
    TCustomerDto,
    TCustomerEvaluateDto,
    TCustomerImExDto,
    TCustomerMajorDto,
    TCustomerOperationalDto,
    TCustomerRouteDto,
    TDeleteCustomerRequest,
    TDeliveryCustomerRequest,
    TDenyCustomerRequest,
    TResponseDto,
    TResponsePageDto,
    TUndeliveryCustomerRequest,
    TUpdateCustomerClassifyRequest,
    TUpdateCustomerContactRequest,
    TUpdateCustomerEvaluateRequest,
    TUpdateCustomerImExRequest,
    TUpdateCustomerMajorRequest,
    TUpdateCustomerOperationalRequest,
    TUpdateCustomerRequest,
    TUpdateCustomerRouteRequest,
} from "../types";
import { notification } from "../utilities";

export const getCustomers = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const getCustomersDelete = async ({
    start,
    size = 100,
    search = "",
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer/delete", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const createCustomer = async (payload: TCreateCustomerRequest) => {
    try {
        const response = await privateInstance.post("/customer", payload);
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

export const updateCustomer = async (payload: TUpdateCustomerRequest) => {
    try {
        const response = await privateInstance.put(
            `/customer/${payload.id}`,
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

export const deleteCustomer = async (payload: TDeleteCustomerRequest) => {
    try {
        const response = await privateInstance.put(
            `/customer/${payload.id}/delete`,
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

export const getCustomersAssigned = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer/assign", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const getCustomersDelivered = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer/delivered", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const getCustomersUndelivered = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer/undelivered", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const getCustomersReceived = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    try {
        const response = await privateInstance.get("/customer/received", {
            params: {
                start,
                size,
                search,
            },
        });

        const { data, totalRowCount }: TResponsePageDto<TCustomerDto[]> =
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

export const chooseCustomer = async (payload: TChooseCustomerRequest) => {
    try {
        const response = await privateInstance.put("/customer/choose", payload);
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

export const deliveryCustomer = async (payload: TDeliveryCustomerRequest) => {
    try {
        const response = await privateInstance.put(
            "/customer/delivery",
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

export const undeliveryCustomer = async (
    payload: TUndeliveryCustomerRequest
) => {
    try {
        const response = await privateInstance.put(
            "/customer/undelivery",
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

export const acceptCustomer = async (payload: TAcceptCustomerRequest) => {
    try {
        const response = await privateInstance.put("/customer/accept", payload);
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

export const denyCustomer = async (payload: TDenyCustomerRequest) => {
    try {
        const response = await privateInstance.put("/customer/deny", payload);
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

// Information
// List ImEx of customer id
export const getCustomerImExs = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/imex/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const { data, totalRowCount }: TResponsePageDto<TCustomerImExDto[]> =
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

export const createCustomerImEx = async (
    payload: TCreateCustomerImExRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/imex",
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

export const updateCustomerImEx = async (
    payload: TUpdateCustomerImExRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/imex/${payload.id}`,
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

export const deleteCustomerImEx = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/imex/${id}`
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

// List Operational of customer id
export const getCustomerOperationals = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/operational/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const {
            data,
            totalRowCount,
        }: TResponsePageDto<TCustomerOperationalDto[]> = response.data;

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

export const createCustomerOperational = async (
    payload: TCreateCustomerOperationalRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/operational",
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

export const updateCustomerOperational = async (
    payload: TUpdateCustomerOperationalRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/operational/${payload.id}`,
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

export const deleteCustomerOperational = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/operational/${id}`
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

// List Contact of customer id
export const getAllCustomerContacts = async (idCustomer: number) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/contact/${idCustomer}/all`
        );
        const { data }: TResponseDto<TCustomerContactDto[]> = response.data;

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

export const getCustomerContacts = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/contact/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const { data, totalRowCount }: TResponsePageDto<TCustomerContactDto[]> =
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

export const createCustomerContact = async (
    payload: TCreateCustomerContactRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/contact",
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

export const updateCustomerContact = async (
    payload: TUpdateCustomerContactRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/contact/${payload.id}`,
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

export const deleteCustomerContact = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/contact/${id}`
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

// List Evaluate of customer id
export const getCustomerEvaluates = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/evaluate/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const {
            data,
            totalRowCount,
        }: TResponsePageDto<TCustomerEvaluateDto[]> = response.data;

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

export const createCustomerEvaluate = async (
    payload: TCreateCustomerEvaluateRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/evaluate",
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

export const updateCustomerEvaluate = async (
    payload: TUpdateCustomerEvaluateRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/evaluate/${payload.id}`,
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

export const deleteCustomerEvaluate = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/evaluate/${id}`
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

// List Classify of customer id
export const getCustomerClassifies = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/classify/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const {
            data,
            totalRowCount,
        }: TResponsePageDto<TCustomerClassifyDto[]> = response.data;

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

export const createCustomerClassify = async (
    payload: TCreateCustomerClassifyRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/classify",
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

export const updateCustomerClassify = async (
    payload: TUpdateCustomerClassifyRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/classify/${payload.id}`,
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

export const deleteCustomerClassify = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/classify/${id}`
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

// List Major of customer id
export const getCustomerMajors = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/major/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const { data, totalRowCount }: TResponsePageDto<TCustomerMajorDto[]> =
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

export const createCustomerMajor = async (
    payload: TCreateCustomerMajorRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/major",
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

export const updateCustomerMajor = async (
    payload: TUpdateCustomerMajorRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/major/${payload.id}`,
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

export const deleteCustomerMajor = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/major/${id}`
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

// List Route of customer id
export const getCustomerRoutes = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    try {
        const response = await privateInstance.get(
            `/customerinfo/route/${idCustomer}`,
            {
                params: {
                    start,
                    size,
                    search,
                },
            }
        );

        const { data, totalRowCount }: TResponsePageDto<TCustomerRouteDto[]> =
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

export const createCustomerRoute = async (
    payload: TCreateCustomerRouteRequest
) => {
    try {
        const response = await privateInstance.post(
            "/customerinfo/route",
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

export const updateCustomerRoute = async (
    payload: TUpdateCustomerRouteRequest
) => {
    try {
        const response = await privateInstance.put(
            `/customerinfo/route/${payload.id}`,
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

export const deleteCustomerRoute = async (id: number) => {
    try {
        const response = await privateInstance.delete(
            `/customerinfo/route/${id}`
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

export const exportCustomerData = async () => {
    try {
        const response = await privateInstance.get("/customer/export");
        const { status, message, data }: TResponseDto<TApiResponseExportProps> =
            response.data;

        return { status, message, data };
    } catch (err) {
        console.error(err);

        if (request.isAxiosError(err) && err.response) {
            const { status, message }: TResponseDto<null> = err.response.data;
            console.log(status, message);
        }

        return false;
    }
};
