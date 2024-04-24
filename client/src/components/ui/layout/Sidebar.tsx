import { Link, NavLink } from "react-router-dom";
import { FaTachometerAlt, FaUsers, FaHouseUser } from "react-icons/fa";
import { BiSolidCategory, BiSolidReport } from "react-icons/bi";

import { TAuthContextProps, useAuth } from "../../../contexts";
import { categories } from "../../../constants";
import LogoPreview from "../../../assets/images/logo-preview.png";

const Sidebar = () => {
    const { currentUser }: TAuthContextProps = useAuth();

    return (
        <aside className="sidebar">
            <div className="sidebar-container">
                <Link className="flex items-center gap-4" to="/">
                    <div className="sidebar-logo">
                        <img
                            className="img-fluid"
                            src={LogoPreview}
                            alt="Logo preview"
                        />
                    </div>
                    <span className="hidden lg:block font-bold text-4xl leading-none">
                        Logo
                    </span>
                </Link>
                <ul className="sidebar-list sm:py-12">
                    <li className="sidebar-item">
                        <NavLink className="sidebar-link" to="/" end>
                            <span className="sidebar-link-icon">
                                <FaTachometerAlt />
                            </span>
                            <span className="sidebar-link-text">dashboard</span>
                        </NavLink>
                    </li>
                    {(currentUser?.permission.includes("1048576") ||
                        currentUser?.permission.includes("5000")) && (
                        <li className="sidebar-item">
                            <NavLink className="sidebar-link" to="/employee">
                                <span className="sidebar-link-icon">
                                    <FaUsers />
                                </span>
                                <span className="sidebar-link-text">
                                    nhân viên
                                </span>
                            </NavLink>
                        </li>
                    )}
                    <li className="sidebar-item">
                        <NavLink className="sidebar-link" to="/customer">
                            <span className="sidebar-link-icon">
                                <FaHouseUser />
                            </span>
                            <span className="sidebar-link-text">
                                khách hàng
                            </span>
                        </NavLink>
                    </li>
                    <li className="sidebar-item">
                        <NavLink className="sidebar-link" to="/report">
                            <span className="sidebar-link-icon">
                                <BiSolidReport />
                            </span>
                            <span className="sidebar-link-text">báo cáo</span>
                        </NavLink>
                    </li>
                    {(currentUser?.permission.includes("1048576") ||
                        currentUser?.permission.includes("6000")) && (
                        <li className="sidebar-item">
                            <NavLink
                                className="sidebar-link"
                                to={categories[0].path}
                            >
                                <span className="sidebar-link-icon">
                                    <BiSolidCategory />
                                </span>
                                <span className="sidebar-link-text">
                                    danh mục
                                </span>
                            </NavLink>
                        </li>
                    )}
                </ul>
            </div>
        </aside>
    );
};

export default Sidebar;
