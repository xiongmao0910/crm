import { Suspense } from "react";
import { Routes, Route, Outlet, Navigate } from "react-router-dom";

import {
    Business,
    City,
    Country,
    Customer,
    Dashboard,
    Department,
    EditProfile,
    Employee,
    LogIn,
    Major,
    Office,
    Operational,
    Port,
    Position,
    Profile,
    Report,
    Transportstation,
    TypeOfCustomer,
} from "../pages";
import { CategoryWrapper, CrmWrapper, NotFound } from "../layout";
import { Loading } from "../components";
import { TAuthContextProps, useAuth } from "../contexts";

const PrivateRoutes = () => {
    const { currentUser }: TAuthContextProps = useAuth();

    return currentUser != null ? <Outlet /> : <Navigate to="/auth/login" />;
};

export const PublicRoutes = () => {
    const { currentUser }: TAuthContextProps = useAuth();

    return currentUser == null ? <Outlet /> : <Navigate to="/" />;
};

export const CrmRoutes = () => {
    return (
        <Suspense
            fallback={
                <div className="w-screen h-screen flex items-center justify-center">
                    <Loading size="3xl" />
                </div>
            }
        >
            <Routes>
                <Route element={<PublicRoutes />}>
                    <Route path="/auth/login" element={<LogIn />} />
                </Route>
                <Route element={<PrivateRoutes />}>
                    <Route element={<CrmWrapper />}>
                        <Route path="/" element={<Dashboard />} />
                        <Route path="/employee" element={<Employee />} />
                        <Route path="/customer" element={<Customer />} />
                        <Route path="/report" element={<Report />} />
                        <Route path="/:username" element={<Profile />} />
                        <Route
                            path="/:username/setting"
                            element={<EditProfile />}
                        />
                        <Route element={<CategoryWrapper />}>
                            <Route
                                path="/category/position"
                                element={<Position />}
                            />
                            <Route
                                path="/category/department"
                                element={<Department />}
                            />
                            <Route
                                path="/category/office"
                                element={<Office />}
                            />
                            <Route path="/category/city" element={<City />} />
                            <Route
                                path="/category/country"
                                element={<Country />}
                            />
                            <Route path="/category/port" element={<Port />} />
                            <Route
                                path="/category/business"
                                element={<Business />}
                            />
                            <Route
                                path="/category/transportstation"
                                element={<Transportstation />}
                            />
                            <Route
                                path="/category/operational"
                                element={<Operational />}
                            />
                            <Route path="/category/major" element={<Major />} />
                            <Route
                                path="/category/type-of-customer"
                                element={<TypeOfCustomer />}
                            />
                        </Route>
                    </Route>
                </Route>
                <Route
                    path="*"
                    element={
                        <div className="h-screen">
                            <NotFound />
                        </div>
                    }
                />
            </Routes>
        </Suspense>
    );
};
