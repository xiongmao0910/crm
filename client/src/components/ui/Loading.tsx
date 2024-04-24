import { useMemo } from "react";
import ReactLoading from "react-loading";

import { TLoadingProps } from "../../types";

const Loading = ({
    onlyIcon = false,
    size = "base",
    message = "Đang tải dữ liệu",
    type = "spin",
    color = "#2563eb",
}: TLoadingProps) => {
    const loadingSize = useMemo(() => {
        let result = 0;

        switch (size) {
            case "base":
                result = 16;
                break;
            case "xs":
                result = 8;
                break;
            case "sm":
                result = 12;
                break;
            case "lg":
                result = 20;
                break;
            case "xl":
                result = 24;
                break;
            case "2xl":
                result = 28;
                break;
            case "3xl":
                result = 32;
                break;
            case "4xl":
                result = 48;
                break;
            case "5xl":
                result = 64;
                break;
            default:
                break;
        }

        return result;
    }, [size]);

    if (onlyIcon) {
        return (
            <>
                <ReactLoading
                    type={type}
                    color={color}
                    width={loadingSize}
                    height={loadingSize}
                />
            </>
        );
    }

    return (
        <div className="w-full h-full flex flex-col justify-center items-center">
            <ReactLoading
                type={type}
                color={color}
                width={loadingSize}
                height={loadingSize}
            />
            <p>{message}</p>
        </div>
    );
};

export default Loading;
