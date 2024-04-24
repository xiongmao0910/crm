export const gender = ["Nam", "Nữ", "Khác", ""];

export const categories = [
    {
        label: "chức vụ",
        path: "/category/position",
    },
    {
        label: "phòng ban",
        path: "/category/department",
    },
    {
        label: "văn phòng",
        path: "/category/office",
    },
    {
        label: "thành phố",
        path: "/category/city",
    },
    {
        label: "quốc gia",
        path: "/category/country",
    },
    {
        label: "cảng",
        path: "/category/port",
    },
    {
        label: "loại doanh nghiệp",
        path: "/category/business",
    },
    {
        label: "loại hình vận chuyển",
        path: "/category/transportstation",
    },
    {
        label: "loại tác nghiệp",
        path: "/category/operational",
    },
    {
        label: "nghiệp vụ",
        path: "/category/major",
    },
    {
        label: "phân loại khách hàng",
        path: "/category/type-of-customer",
    },
].sort((a, b) => {
    let comparition = 0;

    if (a.label.toUpperCase() > b.label.toUpperCase()) {
        comparition = 1;
    }
    if (a.label.toUpperCase() < b.label.toUpperCase()) {
        comparition = -1;
    }

    return comparition;
});
