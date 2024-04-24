import { Link, useParams } from "react-router-dom";
import { MdModeEdit } from "react-icons/md";

import { TAuthContextProps, useAuth } from "../../contexts";
import { NotFound } from "../../layout";
import ProfileBg from "../../assets/images/profile-bg.png";
import { gender } from "../../constants";

const Profile = () => {
    const { username } = useParams();

    const { currentUser }: TAuthContextProps = useAuth();

    if (
        currentUser?.username !== username ||
        currentUser?.username.toLowerCase() === "admin"
    ) {
        return (
            <div className="h-screen">
                <NotFound />
            </div>
        );
    }

    return (
        <>
            <section className="section">
                <div className="section-container">
                    <h2 className="section-title">Hồ sơ người dùng</h2>
                    <div className="overflow-hidden rounded-xl border border-stroke shadow-lg">
                        <div className="relative h-35 md:h-65">
                            <img
                                src={ProfileBg}
                                alt="profile cover"
                                className="h-full w-full rounded-tl-sm rounded-tr-sm object-cover object-center"
                            />
                        </div>
                    </div>
                    <div className="relative px-4 pb-6">
                        <div className="relative mx-auto -mt-[70px] h-40 w-40 rounded-full bg-white/20 p-1 backdrop-blur">
                            <img
                                src={
                                    currentUser?.photoURL ??
                                    "https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                                }
                                alt="avatar"
                                className="h-full w-full rounded-full object-cover"
                            />
                        </div>
                        <Link
                            to={`/${currentUser?.username}/setting`}
                            className="absolute flex items-center gap-1 top-[80px] right-4 leading-none text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm py-2 px-4 text-center first-letter:uppercase"
                        >
                            <MdModeEdit className="text-lg" />
                            <span className="first-letter:uppercase">
                                chỉnh sửa
                            </span>
                        </Link>
                        <div className="mt-4">
                            <div className="text-center">
                                <h3 className="mb-1.5 text-2xl font-semibold capitalize">
                                    {currentUser?.hoten}
                                </h3>
                                <p className="font-medium capitalize">
                                    {currentUser?.chucvu}
                                </p>
                            </div>
                            <ul className="grid grid-responsive gap-2 mt-4">
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        họ tên:
                                    </strong>
                                    <span className="capitalize">
                                        &nbsp; {currentUser?.hoten}
                                    </span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        chức vụ:
                                    </strong>
                                    <span className="capitalize">
                                        &nbsp; {currentUser?.chucvu}
                                    </span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        thư điện tử:
                                    </strong>
                                    <span>&nbsp; {currentUser?.email}</span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        giới tính:
                                    </strong>
                                    <span className="capitalize">
                                        &nbsp;{" "}
                                        {currentUser?.gioitinh != undefined &&
                                            gender[currentUser.gioitinh]}
                                    </span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        ngày sinh:
                                    </strong>
                                    <span>&nbsp; {currentUser?.namsinh}</span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        quê quán:
                                    </strong>
                                    <span>&nbsp; {currentUser?.quequan}</span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        số chứng minh thư:
                                    </strong>
                                    <span>&nbsp; {currentUser?.soCMT}</span>
                                </li>
                                <li className="px-4 py-2 leading-normal border-0 rounded-t-lg text-inherit">
                                    <strong className="capitalize">
                                        số điện thoại:
                                    </strong>
                                    <span>&nbsp; {currentUser?.didong}</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
};

export default Profile;
