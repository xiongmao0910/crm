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

import { deleteCustomer, getCustomersDelete } from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TDeleteCustomerRequest,
    TCustomerDto,
    TResponsePageDto,
    TTableColumn,
    TTableData,
} from "../../../types";
import { Modal, Button } from "../../../components";

type TColumn = TTableColumn<TCustomerDto>;
type TData = TTableData<TCustomerDto>;

const CustomerDeleteTable = () => {
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
            "customers",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomersDelete({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
            }),
        onSuccess: (data: TResponsePageDto<TCustomerDto[]>) => {
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
        mutationFn: deleteCustomer,
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
        async (data: TDeleteCustomerRequest) => {
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
                accessorKey: "code",
                header: "Mã",
                size: 80,
            },
            {
                accessorKey: "nameVI",
                header: "Tên khách hàng",
                size: 140,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "addressVI",
                header: "Địa chỉ",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "thanhPho",
                header: "Thành phố",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "quocGia",
                header: "Quốc gia",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "phone",
                header: "Số điện thoại",
            },
            {
                accessorKey: "email",
                header: "Thư điện tử",
            },
            {
                accessorKey: "website",
                header: "Website",
            },
            {
                accessorKey: "note",
                header: "Ghi chú",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "nguoiXoa",
                header: "Người xoá",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "ngayXoa",
                header: "Ngày xoá",
            },
            {
                accessorKey: "lyDoXoa",
                header: "Lý do xoá",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
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
                    currentUser?.permission.includes("7000") ||
                    currentUser?.id === row.original.idUserCreate) && (
                    <>
                        <Tooltip
                            arrow
                            placement="right"
                            title="Yêu cầu huỷ xoá"
                        >
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id } = row.original as TCustomerDto;
                                    setIdSelectedDelete(id);
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
                title="Huỷ xoá khách hàng"
                isOpen={isOpenDeleteModal}
                onClose={closeDeleteModal}
            >
                <form
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeleteModal({
                            id: idSelectedDelete as number,
                            flagDel: false,
                            idUserDelete: null,
                            lyDoXoa: "",
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

export default CustomerDeleteTable;
