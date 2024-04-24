import { useMemo, useState } from "react";
import { NavLink, Outlet } from "react-router-dom";

import { categories } from "../../constants";
import { sortStringCb } from "../../utilities";

const CategoryWrapper = () => {
    const [search, setSearch] = useState<string>("");

    const filterCategories = useMemo(() => {
        if (search === "") return categories;

        const filterRs = categories
            .filter((c) =>
                c.label
                    .toLowerCase()
                    .replace(/\s+/g, "")
                    .includes(search.toLowerCase().replace(/\s+/g, ""))
            )
            .sort((a, b) => sortStringCb(a.label, b.label));

        return filterRs;
    }, [search]);

    return (
        <section className="section">
            <div className="section-container">
                <h2 className="section-title">quản lý danh mục</h2>
                <div className="category">
                    <div className="category-list">
                        <input
                            className="category-input"
                            type="text"
                            placeholder="Tìm kiếm danh mục..."
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                        />
                        {filterCategories.map((c, index) => (
                            <NavLink
                                className="category-item"
                                key={index}
                                to={c.path}
                                end={index === 0}
                            >
                                {c.label}
                            </NavLink>
                        ))}
                    </div>
                    <Outlet />
                </div>
            </div>
        </section>
    );
};

export default CategoryWrapper;
