import { lazy } from "react";

export const LogIn = lazy(() => import("./auth/LogIn"));

export const Dashboard = lazy(() => import("./crm/Dashboard"));
export const Employee = lazy(() => import("./crm/Employee"));
export const Customer = lazy(() => import("./crm/Customer"));
export const Report = lazy(() => import("./crm/Report"));

export const Position = lazy(() => import("./crm/category/Position"));
export const Department = lazy(() => import("./crm/category/Department"));
export const Office = lazy(() => import("./crm/category/Office"));
export const City = lazy(() => import("./crm/category/City"));
export const Country = lazy(() => import("./crm/category/Country"));
export const Port = lazy(() => import("./crm/category/Port"));
export const Business = lazy(() => import("./crm/category/Business"));
export const Transportstation = lazy(
    () => import("./crm/category/Transportstation")
);
export const Operational = lazy(() => import("./crm/category/Operational"));
export const Major = lazy(() => import("./crm/category/Major"));
export const TypeOfCustomer = lazy(
    () => import("./crm/category/TypeOfCustomer")
);

export const Profile = lazy(() => import("./profile/Profile"));
export const EditProfile = lazy(() => import("./profile/EditProfile"));
