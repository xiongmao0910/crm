export type TCreateEmployeeRequest = {
    // Thông tin tài khoản
    username: string;
    password: string;
    permission: string;
    // Thông tin cá nhân
    idChucVu: number;
    idPhongBan: number;
    idVanPhong: number;
    manhanvien: string;
    hoten: string;
    namsinh: string;
    gioitinh: number;
    quequan: string;
    diachi: string;
    soCMT: string;
    noiCapCMT: string;
    ngayCapCMT: string;
    photoURL: string;
    ghichu: string;
    soLuongKH: number;
};

export type TUpdateEmployeeRequest = {
    // Thông tin tài khoản
    id: number;
    username: string;
    password: string;
    active: boolean;
    permission: string;
    // Thông tin cá nhân
    idNhanVien: number;
    idChucVu: number;
    idPhongBan: number;
    idVanPhong: number;
    manhanvien: string;
    hoten: string;
    namsinh: string;
    gioitinh: number;
    quequan: string;
    diachi: string;
    soCMT: string;
    noiCapCMT: string;
    ngayCapCMT: string;
    photoURL: string;
    ghichu: string;
    soLuongKH: number;
};

export type TDeleteEmployeeRequest = {
    idNhanVien: number;
    flagDelete: boolean;
    idUserDelete: number | null;
};
