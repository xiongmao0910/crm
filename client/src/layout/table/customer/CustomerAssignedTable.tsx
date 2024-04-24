import { FormEvent, useCallback, useMemo, useState } from "react";
import { useMutation, useQuery } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";

import { getCustomersAssigned, undeliveryCustomer } from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TCustomerDto,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TUndeliveryCustomerRequest,
} from "../../../types";
import { Button, Modal } from "../../../components";

type TColumn = TTableColumn<TCustomerDto>;
type TData = TTableData<TCustomerDto>;

const CustomerAssignedTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenUndeliveryModal, setIsOpenUndeliveryModal] =
        useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);

    const { currentUser }: TAuthContextProps = useAuth();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "customers",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomersAssigned({
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

    const undeliveryMutation = useMutation({
        mutationFn: undeliveryCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeUndeliveryModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openUndeliveryModal = useCallback(() => {
        setIsOpenUndeliveryModal(true);
    }, []);

    const closeUndeliveryModal = useCallback(() => {
        setIsOpenUndeliveryModal(false);
    }, []);

    const handleUndelivery = useCallback(
        async (data: TUndeliveryCustomerRequest) => {
            const result =
                (await undeliveryMutation.mutateAsync(data)) || false;

            return result;
        },
        [undeliveryMutation]
    );

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "enumGiaoNhan",
                header: "Trạng thái",
            },
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
                accessorKey: "nhanVien",
                header: "Nhân viên",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
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
                accessorKey: "nguoiTao",
                header: "Người tạo",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "thongTinGiaoViec",
                header: "Thông tin giao việc",
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
        renderTopToolbarCustomActions: ({ table }) => (
            <div className="flex items-center gap-2">
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("7000") ||
                    currentUser?.permission.includes("7080")) &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            buttonVariant="contained"
                            buttonSize="sm"
                            buttonLabel="huỷ giao khách"
                            buttonRounded="lg"
                            buttonColor="cyan"
                            onClick={() => {
                                const data = table
                                    .getSelectedRowModel()
                                    .flatRows.map(
                                        (row) => tableData[row.index].id
                                    );
                                setDataSelected(data);
                                openUndeliveryModal();
                            }}
                        />
                    )}
            </div>
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
                left: ["enumGiaoNhan"],
                right: ["mrt-row-actions"],
            },
        },
        enableRowSelection: true,
        manualFiltering: true,
        manualPagination: true,
        onGlobalFilterChange: setSearch,
        onRowSelectionChange: setRowSelection,
        onPaginationChange: setPagination,
        rowCount: totalRow,
        state: {
            globalFilter: search,
            isLoading,
            pagination,
            rowSelection: rowSelection,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });

    return (
        <>
            <MaterialReactTable table={table} />
            <Modal
                title="Huỷ giao khách hàng"
                description="bạn chắc chắn muốn huỷ giao khách hàng đã chọn"
                isOpen={isOpenUndeliveryModal}
                onClose={closeUndeliveryModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleUndelivery({
                            idCustomers: dataSelected,
                        });
                    }}
                >
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="huỷ giao"
                            buttonVariant="contained"
                            buttonColor="cyan"
                            buttonRounded="xl"
                            isLoading={undeliveryMutation.isLoading}
                            textIsLoading="đang huỷ giao"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonRounded="xl"
                            onClick={closeUndeliveryModal}
                        />
                    </div>
                </form>
            </Modal>
        </>
    );
};

export default CustomerAssignedTable;
