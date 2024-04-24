import { FormEvent, useCallback, useMemo, useState } from "react";
import { useMutation, useQuery } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";

import {
    deliveryCustomer,
    getCustomersReceived,
    undeliveryCustomer,
} from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TCustomerDto,
    TDeliveryCustomerRequest,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TUndeliveryCustomerRequest,
} from "../../../types";
import { Button, Modal } from "../../../components";
import { DeliveryCustomerModal } from "../../../modal";

type TColumn = TTableColumn<TCustomerDto>;
type TData = TTableData<TCustomerDto>;

const CustomerReceivedTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenReturnModal, setIsOpenReturnModal] = useState<boolean>(false);
    const [isOpenUndeliveryModal, setIsOpenUndeliveryModal] =
        useState<boolean>(false);
    const [isOpenDeliveryModal, setIsOpenDeliveryModal] =
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
            getCustomersReceived({
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

    const deliveryMutation = useMutation({
        mutationFn: deliveryCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeDeliveryModal();
            }
        },
    });

    const returnMutation = useMutation({
        mutationFn: undeliveryCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeReturnModal();
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

    const openDeliveryModal = useCallback(() => {
        setIsOpenDeliveryModal(true);
    }, []);

    const closeDeliveryModal = useCallback(() => {
        setIsOpenDeliveryModal(false);
    }, []);

    const handleDelivery = useCallback(
        async (data: TDeliveryCustomerRequest) => {
            const result = (await deliveryMutation.mutateAsync(data)) || false;

            return result;
        },
        [deliveryMutation]
    );

    const openReturnModal = useCallback(() => {
        setIsOpenReturnModal(true);
    }, []);

    const closeReturnModal = useCallback(() => {
        setIsOpenReturnModal(false);
    }, []);

    const handleReturn = useCallback(
        async (data: TUndeliveryCustomerRequest) => {
            const result = (await returnMutation.mutateAsync(data)) || false;

            return result;
        },
        [returnMutation]
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
                accessorKey: "nhanVien",
                header: "Nhân viên quản lý",
                size: 140,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "nguoiGiaoViec",
                header: "Người giao",
                size: 140,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "note",
                header: "Ghi chú",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
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
                {(table.getIsSomePageRowsSelected() ||
                    table.getIsAllPageRowsSelected()) && (
                    <>
                        {(currentUser?.permission.includes("1048576") ||
                            currentUser?.permission.includes("7000") ||
                            currentUser?.permission.includes("7080")) && (
                            <>
                                <Button
                                    buttonVariant="contained"
                                    buttonSize="sm"
                                    buttonLabel="chuyển khách"
                                    buttonRounded="lg"
                                    buttonColor="blue"
                                    onClick={() => {
                                        const data = table
                                            .getSelectedRowModel()
                                            .flatRows.map(
                                                (row) => tableData[row.index].id
                                            );
                                        setDataSelected(data);
                                        openDeliveryModal();
                                    }}
                                />
                            </>
                        )}
                        {(currentUser?.permission.includes("1048576") ||
                            currentUser?.permission.includes("7000") ||
                            currentUser?.permission.includes("7080")) &&
                            !table
                                .getSelectedRowModel()
                                .flatRows.every(
                                    (row) =>
                                        currentUser?.idNhanVien ===
                                        row.original.idNhanVienSale
                                ) && (
                                <Button
                                    buttonVariant="contained"
                                    buttonSize="sm"
                                    buttonLabel="huỷ giao"
                                    buttonRounded="lg"
                                    buttonColor="red"
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
                        {!currentUser?.permission.includes("1048576") &&
                            table
                                .getSelectedRowModel()
                                .flatRows.every(
                                    (row) =>
                                        currentUser?.idNhanVien ===
                                        row.original.idNhanVienSale
                                ) && (
                                <Button
                                    buttonVariant="contained"
                                    buttonSize="sm"
                                    buttonLabel="trả khách"
                                    buttonRounded="lg"
                                    buttonColor="orange"
                                    onClick={() => {
                                        const data = table
                                            .getSelectedRowModel()
                                            .flatRows.map(
                                                (row) => tableData[row.index].id
                                            );
                                        setDataSelected(data);
                                        openReturnModal();
                                    }}
                                />
                            )}
                    </>
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
            <DeliveryCustomerModal
                title="Chuyển giao khách hàng"
                isOpen={isOpenDeliveryModal}
                onClose={closeDeliveryModal}
                onSubmit={handleDelivery}
                idCustomers={dataSelected}
                isLoading={deliveryMutation.isLoading}
            />
            <Modal
                title="huỷ giao khách hàng"
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
                            buttonColor="teal"
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
            <Modal
                title="trả khách hàng"
                description="bạn chắc chắn muốn trả khách hàng đã chọn"
                isOpen={isOpenReturnModal}
                onClose={closeReturnModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleReturn({
                            idCustomers: dataSelected,
                        });
                    }}
                >
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="trả khách"
                            buttonVariant="contained"
                            buttonColor="teal"
                            buttonRounded="xl"
                            isLoading={returnMutation.isLoading}
                            textIsLoading="đang trả khách"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonRounded="xl"
                            onClick={closeReturnModal}
                        />
                    </div>
                </form>
            </Modal>
        </>
    );
};

export default CustomerReceivedTable;
