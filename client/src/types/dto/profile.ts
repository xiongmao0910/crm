export type TProfileDto = {
    id: number;
    username: string;
    active: boolean;
    permission: string;
    idNhanVien: number;
    idChucVu: number;
    chucvu: string;
    idPhongBan: number;
    phongban: string;
    idVanPhong: number;
    vanphong: string;
    manhanvien: string;
    hoten: string;
    hotenen: string;
    namsinh: string;
    gioitinh: number;
    quequan: string;
    diachi: string;
    soCMT: string;
    noiCapCMT: string;
    ngayCapCMT: string;
    didong: string;
    email: string;
    photoURL: string;
    ghichu: string;
    createDate: string;
    editDate: string;
    soLuongKH: number;
    flagDelete: boolean;
    idUserDelete: number | null;
    dateDelete: string;
};
