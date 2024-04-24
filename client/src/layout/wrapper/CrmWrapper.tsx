import { Outlet } from "react-router-dom";

import { CommonProvider } from "../../contexts";
import { Header, Sidebar } from "../../components";

const CrmWrapper = () => {
    return (
        <CommonProvider>
            <div className="wrapper">
                {/* Sidebar */}
                <Sidebar />
                {/* Main */}
                <main className="main">
                    <Header />
                    <Outlet />
                </main>
            </div>
        </CommonProvider>
    );
};

export default CrmWrapper;
