import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "animate.css/animate.min.css";

import { AuthProvider, ThemeProvider } from "./contexts";
import { CrmRoutes } from "./routes";

function App() {
    return (
        <>
            <ThemeProvider>
                <AuthProvider>
                    <CrmRoutes />
                </AuthProvider>
            </ThemeProvider>
            <ToastContainer />
        </>
    );
}

export default App;
