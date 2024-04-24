export const sortStringCb = (a: string, b: string) => {
    let comparition = 0;

    if (a.toUpperCase() > b.toUpperCase()) {
        comparition = 1;
    }
    if (a.toUpperCase() < b.toUpperCase()) {
        comparition = -1;
    }

    return comparition;
};

export const rgbToHex = (r: number, g: number, b: number) => {
    const result =
        "#" + ((1 << 24) | (r << 16) | (g << 8) | b).toString(16).slice(1);
    return result;
};

export const hexToRgb = (hex: string) => {
    const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);

    if (!result) return null;

    return {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16),
    };
};
