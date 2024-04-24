import { FormEvent, useCallback, useMemo, useState } from "react";
import { useMutation, useQuery } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { SettingsBackupRestore } from "@mui/icons-material";

import { deleteEmployee, getEmployeesDelete } from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TDeleteEmployeeRequest,
    TProfileDto,
    TResponsePageDto,
    TTableColumn,
    TTableData,
} from "../../../types";
import { gender } from "../../../constants";
import { Modal, Button } from "../../../components";

type TColumn = TTableColumn<TProfileDto>;
type TData = TTableData<TProfileDto>;

const EmployeeDeleteTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );

    const { currentUser }: TAuthContextProps = useAuth();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "employees",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getEmployeesDelete({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
            }),
        onSuccess: (data: TResponsePageDto<TProfileDto[]>) => {
            if (data?.totalRowCount && data.totalRowCount !== totalRow) {
                setTotalRow(data.totalRowCount);
            }

            if (data?.data) {
                setTableData(data.data);
            }
        },
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const deleteMutation = useMutation({
        mutationFn: deleteEmployee,
        onSuccess: (data) => {
            if (data) {
                refetch();
                closeDeleteModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openDeleteModal = useCallback(() => {
        setIsOpenDeleteModal(true);
    }, []);

    const closeDeleteModal = useCallback(() => {
        setIdSelectedDelete(null);
        setIsOpenDeleteModal(false);
    }, []);

    const handleDeleteModal = useCallback(
        async (data: TDeleteEmployeeRequest) => {
            const result = (await deleteMutation.mutateAsync(data)) || false;

            return result;
        },
        [deleteMutation]
    );

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "manhanvien",
                header: "Mã nhân sự",
                size: 80,
            },
            {
                accessorKey: "hoten",
                header: "Tên nhân viên",
                size: 140,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "chucvu",
                header: "Chức vụ",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "phongban",
                header: "Phòng ban",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "vanphong",
                header: "Văn phòng",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "namsinh",
                header: "Ngày sinh",
            },
            {
                accessorKey: "gioitinh",
                header: "Giới tính",
                accessorFn: (row) => gender[row.gioitinh ? row.gioitinh : 3],
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "quequan",
                header: "Quê quán",
                accessorFn: (row) => row.quequan,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "diachi",
                header: "Địa chỉ",
                accessorFn: (row) => row.diachi,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "didong",
                header: "Số điện thoại",
            },
            {
                accessorKey: "email",
                header: "Thư điện tử",
            },
            {
                accessorKey: "soLuongKH",
                header: "Số lượng khách hàng",
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableStickyHeader: true,
        enableStickyFooter: true,
        muiTableContainerProps: { sx: { maxHeight: "640px" } },
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("5000")) && (
                    <>
                        <Tooltip
                            arrow
                            placement="right"
                            title="Yêu cầu huỷ xoá"
                        >
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { idNhanVien } =
                                        row.original as TProfileDto;
                                    setIdSelectedDelete(idNhanVien);
                                    openDeleteModal();
                                }}
                            >
                                <SettingsBackupRestore />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
            </Box>
        ),
        renderEmptyRowsFallback: () => (
            <div className="px-2 py-6">
                <p className="section-subtitle first-letter:uppercase">
                    không có dữ liệu
                </p>
            </div>
        ),
        initialState: {
            pagination,
            columnPinning: {
                right: ["mrt-row-actions"],
            },
        },
        enableEditing: true,
        manualFiltering: true,
        manualPagination: true,
        onGlobalFilterChange: setSearch,
        onPaginationChange: setPagination,
        rowCount: totalRow,
        state: {
            globalFilter: search,
            isLoading,
            pagination,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });

    return (
        <>
            <MaterialReactTable table={table} />
            <Modal
                title="Huỷ xoá nhân viên"
                description="bạn muốn thực hiện huỷ xoá nhân viên này?"
                isOpen={isOpenDeleteModal}
                onClose={closeDeleteModal}
            >
                <form
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeleteModal({
                            idNhanVien: idSelectedDelete as number,
                            flagDelete: false,
                            idUserDelete: null,
                        });
                    }}
                >
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="huỷ xoá"
                            buttonVariant="contained"
                            buttonColor="red"
                            buttonRounded="xl"
                            isLoading={deleteMutation.isLoading}
                            textIsLoading="đang huỷ xoá"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            onClick={closeDeleteModal}
                        />
                    </div>
                </form>
            </Modal>
        </>
    );
};

export default EmployeeDeleteTable;
