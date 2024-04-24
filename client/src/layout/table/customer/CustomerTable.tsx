import { FormEvent, useCallback, useMemo, useRef, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import { BsInfoCircleFill } from "react-icons/bs";

import {
    chooseCustomer,
    createCustomer,
    deleteCustomer,
    deliveryCustomer,
    exportCustomerData,
    getCustomers,
    updateCustomer,
} from "../../../api";
import {
    CreateCustomerModal,
    DeliveryCustomerModal,
    InforCustomerModal,
    UpdateCustomerModal,
} from "../../../modal";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TChooseCustomerRequest,
    TCreateCustomerRequest,
    TCustomerDto,
    TDeleteCustomerRequest,
    TDeliveryCustomerRequest,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TUpdateCustomerRequest,
} from "../../../types";
import { Button, GroupInput, Modal } from "../../../components";
import { privateInstance } from "../../../configs";

type TColumn = TTableColumn<TCustomerDto>;
type TData = TTableData<TCustomerDto>;

const CustomerTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [isOpenInfoModal, setIsOpenInfoModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdateCustomerRequest | null>(null);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );
    const [idSelectedInfo, setIdSelectedInfo] = useState<number | null>(null);

    const [isOpenDeliveryModal, setIsOpenDeliveryModal] =
        useState<boolean>(false);
    const [isOpenChooseModal, setIsOpenChooseModal] = useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);

    const inputRef = useRef<HTMLInputElement | null>(null);

    const { currentUser }: TAuthContextProps = useAuth();

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "customers",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomers({
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

    const createMutation = useMutation({
        mutationFn: createCustomer,
        onSuccess: (data) => {
            if (data) {
                refetch();
                closeCreateModal();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updateCustomer,
        onSuccess: (data) => {
            if (data) {
                queryClient.invalidateQueries({ queryKey: "customers" });
                closeUpdateModal();
            }
        },
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

    const chooseMutation = useMutation({
        mutationFn: chooseCustomer,
        onSuccess: (data) => {
            if (data) {
                queryClient.invalidateQueries({ queryKey: "customers" });
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeChooseModal();
            }
        },
    });

    const deliveryMutation = useMutation({
        mutationFn: deliveryCustomer,
        onSuccess: (data) => {
            if (data) {
                queryClient.invalidateQueries({ queryKey: "customers" });
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeDeliveryModal();
            }
        },
    });

    const exportMutation = useMutation({
        mutationFn: exportCustomerData,
    });

    /**
     * * Handle events
     */
    const openCreateModal = useCallback(() => {
        setIsOpenCreateModal(true);
    }, []);

    const closeCreateModal = useCallback(() => {
        setIsOpenCreateModal(false);
    }, []);

    const handleCreate = useCallback(
        async (data: TCreateCustomerRequest) => {
            const result = (await createMutation.mutateAsync(data)) || false;

            return result;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback((data: TUpdateCustomerRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateCustomerRequest) => {
            const result = (await updateMutation.mutateAsync(data)) || false;

            return result;
        },
        [updateMutation]
    );

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

    const openChooseModal = useCallback(() => {
        setIsOpenChooseModal(true);
    }, []);

    const closeChooseModal = useCallback(() => {
        setIsOpenChooseModal(false);
    }, []);

    const handleChoose = useCallback(
        async (data: TChooseCustomerRequest) => {
            const result = (await chooseMutation.mutateAsync(data)) || false;

            return result;
        },
        [chooseMutation]
    );

    const openInfoModal = useCallback(() => {
        setIsOpenInfoModal(true);
    }, []);

    const closeInfoModal = useCallback(() => {
        setIsOpenInfoModal(false);
        setIdSelectedInfo(null);
    }, []);

    const handleExportData = async () => {
        const data = await exportMutation.mutateAsync();

        if (data && data.status) {
            const baseUrl = privateInstance.defaults.baseURL;
            const fileName = data.data.downloadUrl.split("/").slice(-1)[0];
            const url = `${baseUrl}/customer/export/download/${fileName}`;

            console.log(url);
            // Create a link element
            const link = document.createElement("a");
            link.href = url;
            link.download = fileName;

            // Trigger click event to initiate download
            link.click();
        }
    };

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
                accessorKey: "nhanVien",
                header: "Nhân viên",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
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
                    currentUser?.permission.includes("7020")) && (
                    <Button
                        buttonVariant="contained"
                        buttonSize="sm"
                        buttonLabel="tạo khách hàng"
                        buttonRounded="lg"
                        buttonColor="blue"
                        onClick={openCreateModal}
                    />
                )}
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("7000") ||
                    currentUser?.permission.includes("7080")) &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            buttonVariant="contained"
                            buttonSize="sm"
                            buttonLabel="giao khách"
                            buttonRounded="lg"
                            buttonColor="cyan"
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
                    )}
                {!currentUser?.permission.includes("1048576") &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            buttonVariant="contained"
                            buttonSize="sm"
                            buttonLabel="nhận khách"
                            buttonRounded="lg"
                            buttonColor="teal"
                            onClick={() => {
                                const data = table
                                    .getSelectedRowModel()
                                    .flatRows.map(
                                        (row) => tableData[row.index].id
                                    );
                                setDataSelected(data);
                                openChooseModal();
                            }}
                        />
                    )}
                <Button
                    buttonVariant="contained"
                    buttonSize="sm"
                    buttonLabel="export dữ liệu"
                    buttonRounded="lg"
                    buttonColor="green"
                    isLoading={exportMutation.isLoading}
                    onClick={handleExportData}
                />
            </div>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("7000") ||
                    currentUser?.id === row.original.idUserCreate) && (
                    <>
                        <Tooltip arrow placement="left" title="Chỉnh sửa">
                            <IconButton
                                color="primary"
                                onClick={() => {
                                    const { id } = row.original;
                                    const data = tableData.find(
                                        (e) => e.id === id
                                    ) as TCustomerDto;
                                    const payload: TUpdateCustomerRequest = {
                                        id: data.id,
                                        idQuocGia: data.idQuocGia,
                                        idCity: data.idCity,
                                        code: data.code,
                                        nameVI: data.nameVI,
                                        nameEN: data.nameEN,
                                        addressVI: data.addressVI,
                                        addressEN: data.addressEN,
                                        taxCode: data.taxCode,
                                        phone: data.phone,
                                        fax: data.fax,
                                        email: data.email,
                                        website: data.website,
                                        note: data.note,
                                    };
                                    openUpdateModal(payload);
                                }}
                            >
                                <Edit />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("7000") ||
                    currentUser?.id === row.original.idUserCreate) && (
                    <>
                        <Tooltip arrow placement="right" title="Yêu cầu xoá">
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id } = row.original as TCustomerDto;
                                    setIdSelectedDelete(id);
                                    openDeleteModal();
                                }}
                            >
                                <Delete />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
                <Tooltip arrow placement="left" title="Thông tin liên quan">
                    <IconButton
                        color="default"
                        onClick={() => {
                            const { id } = row.original;
                            setIdSelectedInfo(id as number);
                            openInfoModal();
                        }}
                    >
                        <BsInfoCircleFill />
                    </IconButton>
                </Tooltip>
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
                left: ["enumGiaoNhan"],
                right: ["mrt-row-actions"],
            },
        },
        enableEditing: true,
        enableRowSelection: (row) => {
            const statusRow = row.original.enumGiaoNhan as number;
            const isEnabled =
                statusRow === 0 || statusRow === 3 || statusRow === null
                    ? true
                    : false;
            return isEnabled;
        },
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
            <CreateCustomerModal
                title="Thêm khách hàng"
                width="7xl"
                isOpen={isOpenCreateModal}
                onClose={closeCreateModal}
                onSubmit={handleCreate}
                isLoading={createMutation.isLoading}
            />
            <UpdateCustomerModal
                title="Chỉnh sửa khách hàng"
                width="7xl"
                isOpen={isOpenUpdateModal}
                onClose={closeUpdateModal}
                onSubmit={handleUpdate}
                prevData={dataSelectedUpdate}
                isLoading={updateMutation.isLoading}
            />
            <Modal
                title="Xoá khách hàng"
                isOpen={isOpenDeleteModal}
                onClose={closeDeleteModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeleteModal({
                            id: idSelectedDelete as number,
                            flagDel: true,
                            idUserDelete: currentUser?.id as number,
                            lyDoXoa: inputRef.current?.value ?? "",
                        });
                    }}
                >
                    <GroupInput
                        ref={inputRef}
                        labelFor="lyDoXoa"
                        labelText="Lý do xoá"
                    />
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="xoá"
                            buttonVariant="contained"
                            buttonColor="red"
                            buttonRounded="xl"
                            isLoading={deleteMutation.isLoading}
                            textIsLoading="đang xoá"
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
            <Modal
                title="Nhận khách hàng"
                description="bạn chắc chắn muốn nhận khách hàng đã chọn"
                isOpen={isOpenChooseModal}
                onClose={closeChooseModal}
            >
                <form
                    className="grid gap-4"
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleChoose({
                            idNhanVienSale: currentUser?.idNhanVien as number,
                            idCustomers: dataSelected,
                        });
                    }}
                >
                    <div className="flex items-center gap-4">
                        <Button
                            buttonLabel="nhận"
                            buttonVariant="contained"
                            buttonColor="teal"
                            buttonRounded="xl"
                            isLoading={chooseMutation.isLoading}
                            textIsLoading="đang nhận"
                        />
                        <Button
                            type="button"
                            buttonLabel="huỷ"
                            buttonVariant="contained"
                            buttonRounded="xl"
                            onClick={closeChooseModal}
                        />
                    </div>
                </form>
            </Modal>
            <DeliveryCustomerModal
                title="Giao khách hàng"
                isOpen={isOpenDeliveryModal}
                onClose={closeDeliveryModal}
                onSubmit={handleDelivery}
                idCustomers={dataSelected}
                isLoading={deliveryMutation.isLoading}
            />
            <InforCustomerModal
                width="7xl"
                idCustomer={idSelectedInfo}
                title="Giao khách hàng"
                isOpen={isOpenInfoModal}
                onClose={closeInfoModal}
            />
        </>
    );
};

export default CustomerTable;
