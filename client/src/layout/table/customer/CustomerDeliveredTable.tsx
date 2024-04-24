import { FormEvent, useCallback, useMemo, useRef, useState } from "react";
import { useMutation, useQuery } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";

import {
    acceptCustomer,
    denyCustomer,
    getCustomersDelivered,
} from "../../../api";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TCustomerDto,
    TAcceptCustomerRequest,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TDenyCustomerRequest,
} from "../../../types";
import { Button, GroupInput, Modal } from "../../../components";

type TColumn = TTableColumn<TCustomerDto>;
type TData = TTableData<TCustomerDto>;

const CustomerDeliveredTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenAcceptModal, setIsOpenAcceptModal] = useState<boolean>(false);
    const [isOpenDenyModal, setIsOpenDenyModal] = useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);

    const { currentUser }: TAuthContextProps = useAuth();

    const inputRef = useRef<HTMLInputElement | null>(null);

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "customers",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomersDelivered({
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

    const acceptMutation = useMutation({
        mutationFn: acceptCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeAcceptModal();
            }
        },
    });

    const denyMutation = useMutation({
        mutationFn: denyCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeDenyModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openAcceptModal = useCallback(() => {
        setIsOpenAcceptModal(true);
    }, []);

    const closeAcceptModal = useCallback(() => {
        setIsOpenAcceptModal(false);
    }, []);

    const handleAccept = useCallback(
        async (data: TAcceptCustomerRequest) => {
            const result = (await acceptMutation.mutateAsync(data)) || false;

            return result;
        },
        [acceptMutation]
    );

    const openDenyModal = useCallback(() => {
        setIsOpenDenyModal(true);
    }, []);

    const closeDenyModal = useCallback(() => {
        setIsOpenDenyModal(false);
    }, []);

    const handleDeny = useCallback(
        async (data: TDenyCustomerRequest) => {
            const result = (await denyMutation.mutateAsync(data)) || false;

            return result;
        },
        [denyMutation]
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
                        <Button
                            buttonVariant="contained"
                            buttonSize="sm"
                            buttonLabel="nhận"
                            buttonRounded="lg"
                            buttonColor="blue"
                            onClick={() => {
                                const data = table
                                    .getSelectedRowModel()
                                    .flatRows.map(
                                        (row) => tableData[row.index].id
                                    );
                                setDataSelected(data);
                                openAcceptModal();
                            }}
                        />
                        <Button
                            buttonVariant="contained"
                            buttonSize="sm"
                            buttonLabel="từ chối"
                            buttonRounded="lg"
                            buttonColor="red"
                            onClick={() => {
                                const data = table
                                    .getSelectedRowModel()
                                    .flatRows.map(
                                        (row) => tableData[row.index].id
                                    );
                                setDataSelected(data);
                                openDenyModal();
                            }}
                        />
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
            <Modal
                title="Nhận khách hàng"
                description="bạn chắc chắn muốn nhận khách hàng đã chọn"
                isOpen={isOpenAcceptModal}
                onClose={closeAcceptModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleAccept({
                            idNhanVienSale: currentUser?.idNhanVien as number,
                            idCustomers: dataSelected,
                        });
                    }}
                >
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="nhận"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            isLoading={acceptMutation.isLoading}
                            textIsLoading="đang nhận"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonRounded="xl"
                            onClick={closeAcceptModal}
                        />
                    </div>
                </form>
            </Modal>
            <Modal
                title="Từ chối nhận khách hàng"
                isOpen={isOpenDenyModal}
                onClose={closeDenyModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeny({
                            idCustomers: dataSelected,
                            idNhanVienSale: currentUser?.idNhanVien as number,
                            lyDoTuChoi: inputRef.current?.value ?? "",
                        });
                    }}
                >
                    <GroupInput
                        ref={inputRef}
                        labelFor="lyDoTuChoi"
                        labelText="Lý do từ chối"
                    />
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="từ chối"
                            buttonVariant="contained"
                            buttonColor="red"
                            buttonRounded="xl"
                            isLoading={denyMutation.isLoading}
                            textIsLoading="đang từ chối"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonColor="blue"
                            buttonRounded="xl"
                            onClick={closeDenyModal}
                        />
                    </div>
                </form>
            </Modal>
        </>
    );
};

export default CustomerDeliveredTable;
