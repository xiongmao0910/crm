export type TCreateCustomerRequest = {
    idQuocGia: number;
    idCity: number;
    code: string;
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    taxCode: string;
    phone: string;
    fax: string;
    email: string;
    website: string;
    note: string;
    idUserCreate: number;
};

export type TUpdateCustomerRequest = Omit<
    TCreateCustomerRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TDeleteCustomerRequest = {
    id: number;
    idUserDelete: number | null;
    flagDel: boolean;
    lyDoXoa: string;
};

export type TChooseCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
};

export type TDeliveryCustomerRequest = {
    idNhanVienSale: number;
    idUserGiaoViec: number;
    idCustomers: number[];
    thongTinGiaoViec: string;
};

export type TUndeliveryCustomerRequest = {
    idCustomers: number[];
};

export type TAcceptCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
};

export type TDenyCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
    lyDoTuChoi: string;
};

export type TCreateCustomerImExRequest = {
    date: string;
    type: string;
    vessel: string;
    term: string;
    code: string;
    commd: string;
    vol: string;
    unt: string;
    idUserCreate: number;
    idFromPort: number;
    idToPort: number;
    idFromCountry: number;
    idToCountry: number;
    idCustomer: number;
};

export type TUpdateCustomerImExRequest = Omit<
    TCreateCustomerImExRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerOperationalRequest = {
    idLoaiTacNghiep: number;
    noiDung: string;
    idUserCreate: number;
    idCustomer: number;
    thoiGianThucHien: string;
    idNguoiLienHe: number;
    khachHangPhanHoi: string;
    ngayPhanHoi: string;
};

export type TUpdateCustomerOperationalRequest = Omit<
    TCreateCustomerOperationalRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerContactRequest = {
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    enumGioiTinh: number;
    handPhone: string;
    homePhone: string;
    email: string;
    note: string;
    idCustomer: number;
    bankAccountNumber: string;
    bankBranchName: string;
    bankAddress: string;
    chat: string;
    flagDaiDien: boolean;
    chucVu: string;
};

export type TUpdateCustomerContactRequest = TCreateCustomerContactRequest & {
    id: number;
};

export type TCreateCustomerEvaluateRequest = {
    idCustomer: number;
    idCustomerType: number;
    idUserCreate: number;
    ghiChu: string;
};

export type TUpdateCustomerEvaluateRequest = Omit<
    TCreateCustomerEvaluateRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerClassifyRequest = {
    idCustomer: number;
    idPhanLoaiKhachHang: number;
};

export type TUpdateCustomerClassifyRequest = TCreateCustomerClassifyRequest & {
    id: number;
};

export type TCreateCustomerMajorRequest = {
    idCustomer: number;
    idNghiepVu: number;
};

export type TUpdateCustomerMajorRequest = TCreateCustomerMajorRequest & {
    id: number;
};

export type TCreateCustomerRouteRequest = {
    idQuocGiaDi: number;
    idQuocGiaDen: number;
    idCangDi: number;
    idCangDen: number;
    idCustomer: number;
    idLoaiHinhVanChuyen: number;
};

export type TUpdateCustomerRouteRequest = TCreateCustomerRouteRequest & {
    id: number;
};
