export type TLogInRequest = {
    username: string;
    password: string;
};

export type TChangePasswordRequest = {
    id: number;
    password: string;
    newPassword: string;
};
