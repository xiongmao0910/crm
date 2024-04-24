import {
    TCreateBusinessRequest,
    TCreateCityRequest,
    TCreateCountryRequest,
    TCreateCustomerClassifyRequest,
    TCreateCustomerContactRequest,
    TCreateCustomerEvaluateRequest,
    TCreateCustomerImExRequest,
    TCreateCustomerMajorRequest,
    TCreateCustomerOperationalRequest,
    TCreateCustomerRequest,
    TCreateCustomerRouteRequest,
    TCreateDepartmentRequest,
    TCreateEmployeeRequest,
    TCreateMajorRequest,
    TCreateOfficeRequest,
    TCreateOperationalRequest,
    TCreatePortRequest,
    TCreatePositionRequest,
    TCreateTransportationRequest,
    TCreateTypeOfCustomerRequest,
} from "../types";

export const initEmployee: TCreateEmployeeRequest = {
    username: "",
    password: "",
    permission: "",
    idChucVu: -1,
    idPhongBan: -1,
    idVanPhong: 0,
    manhanvien: "",
    hoten: "",
    namsinh: "",
    gioitinh: -1,
    quequan: "",
    diachi: "",
    soCMT: "",
    noiCapCMT: "",
    ngayCapCMT: "",
    photoURL: "",
    ghichu: "",
    soLuongKH: 0,
};

export const initPosition: TCreatePositionRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initDepartment: TCreateDepartmentRequest = {
    nameVI: "",
    nameEN: "",
    idVanPhong: -1,
};

export const initOffice: TCreateOfficeRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
    taxCode: "",
    idCountry: -1,
    idCity: -1,
};

export const initCountry: TCreateCountryRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initCity: TCreateCityRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
    idQuocGia: -1,
};

export const initPort: TCreatePortRequest = {
    code: "",
    taxCode: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    idQuocGia: -1,
    idCity: -1,
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
};

export const initBusiness: TCreateBusinessRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initTransportation: TCreateTransportationRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initMajor: TCreateMajorRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initTypeOfCustomer: TCreateTypeOfCustomerRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initOperational: TCreateOperationalRequest = {
    name: "",
    r: 0,
    g: 0,
    b: 0,
    ngayTuTraKhach: 0,
};

export const initCustomer: TCreateCustomerRequest = {
    idQuocGia: -1,
    idCity: -1,
    code: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    taxCode: "",
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
    idUserCreate: -1,
};

export const initCustomerContact: TCreateCustomerContactRequest = {
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    enumGioiTinh: -1,
    handPhone: "",
    homePhone: "",
    email: "",
    note: "",
    idCustomer: -1,
    bankAccountNumber: "",
    bankBranchName: "",
    bankAddress: "",
    chat: "",
    flagDaiDien: false,
    chucVu: "",
};

export const initCustomerOperational: TCreateCustomerOperationalRequest = {
    idLoaiTacNghiep: -1,
    noiDung: "",
    idUserCreate: -1,
    idCustomer: -1,
    thoiGianThucHien: "",
    idNguoiLienHe: -1,
    khachHangPhanHoi: "",
    ngayPhanHoi: "",
};

export const initCreateCustomerMajor: TCreateCustomerMajorRequest = {
    idNghiepVu: -1,
    idCustomer: -1,
};

export const initCreateCustomerClassify: TCreateCustomerClassifyRequest = {
    idCustomer: -1,
    idPhanLoaiKhachHang: -1,
};

export const initCreateCustomerEvaluate: TCreateCustomerEvaluateRequest = {
    idCustomer: -1,
    idCustomerType: -1,
    idUserCreate: -1,
    ghiChu: "",
};

export const initCreateCustomerRoute: TCreateCustomerRouteRequest = {
    idCustomer: -1,
    idCangDi: -1,
    idCangDen: -1,
    idQuocGiaDi: -1,
    idQuocGiaDen: -1,
    idLoaiHinhVanChuyen: -1,
};

export const initCreateCustomerImEx: TCreateCustomerImExRequest = {
    date: "",
    type: "",
    vessel: "",
    term: "",
    code: "",
    commd: "",
    vol: "",
    unt: "",
    idUserCreate: -1,
    idFromPort: -1,
    idToPort: -1,
    idFromCountry: -1,
    idToCountry: -1,
    idCustomer: -1,
};
