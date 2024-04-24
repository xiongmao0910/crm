import axios from "axios";

const baseURL =
    import.meta.env.VITE_APP_BASE_URL || "https://localhost:1009/api/v1";

export const publicInstance = axios.create({
    baseURL,
    headers: {
        "Content-Type": "application/json",
        "X-Custom-Header": "foobar",
    },
});

export const privateInstance = axios.create({
    baseURL,
    headers: {
        "Content-Type": "application/json",
        "X-Custom-Header": "foobar",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
    withCredentials: true,
});

// Request interceptors for API calls
privateInstance.interceptors.request.use(
    (config) => {
        config.headers["Authorization"] = `Bearer ${localStorage.getItem(
            "token"
        )}`;
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);
