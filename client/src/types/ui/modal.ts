import { TWidthProps } from "./common";
import {
    TCreateEmployeeRequest,
    TUpdateEmployeeRequest,
} from "../request/employee";
import {
    TCreateCustomerRequest,
    TDeliveryCustomerRequest,
    TUpdateCustomerRequest,
} from "../request/customer";

/**
 * Utilities type
 */
export type TModalProps = {
    isOpen: boolean;
    onClose: () => void;
    width?: TWidthProps;
    height?: string;
    title?: string;
    description?: string;
    children: React.ReactNode;
};

/**
 * Employee modal
 */
export type TCreateEmployeeModalProps = Omit<TModalProps, "children"> & {
    onSubmit: (payload: TCreateEmployeeRequest) => Promise<boolean>;
    isLoading: boolean;
};

export type TUpdateEmployeeModalProps = Omit<TModalProps, "children"> & {
    onSubmit: (payload: TUpdateEmployeeRequest) => Promise<boolean>;
    prevData: TUpdateEmployeeRequest | null;
    isLoading: boolean;
};

/**
 * Category modal
 */
export type TCreateCategoryModalProps<T> = Omit<TModalProps, "children"> & {
    onSubmit: (payload: T) => Promise<boolean>;
    isLoading: boolean;
};

export type TUpdateCategoryModalProps<T> = Omit<TModalProps, "children"> & {
    onSubmit: (payload: T) => Promise<boolean>;
    prevData: T | null;
    isLoading: boolean;
};

/**
 * Customer modal
 */
export type TCreateCustomerModalProps = Omit<TModalProps, "children"> & {
    onSubmit: (payload: TCreateCustomerRequest) => Promise<boolean>;
    isLoading: boolean;
};

export type TUpdateCustomerModalProps = Omit<TModalProps, "children"> & {
    onSubmit: (payload: TUpdateCustomerRequest) => Promise<boolean>;
    prevData: TUpdateCustomerRequest | null;
    isLoading: boolean;
};

export type TDeliveryCustomerModalProps = Omit<TModalProps, "children"> & {
    onSubmit: (payload: TDeliveryCustomerRequest) => Promise<boolean>;
    idCustomers: number[] | [];
    isLoading: boolean;
};

export type TInforCustomerModalProps = Omit<TModalProps, "children"> & {
    idCustomer: number | null;
};

export type TCreateCustomerInfoModalProps<T> = Omit<TModalProps, "children"> & {
    onSubmit: (payload: T) => Promise<boolean>;
    isLoading: boolean;
    idCustomer: number | null;
};

export type TUpdateCustomerInfoModalProps<T> = Omit<TModalProps, "children"> & {
    onSubmit: (payload: T) => Promise<boolean>;
    prevData: T | null;
    isLoading: boolean;
};
