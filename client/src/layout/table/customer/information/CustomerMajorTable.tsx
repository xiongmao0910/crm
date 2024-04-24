import { useState, useMemo, useCallback, FormEvent } from "react";
import { useQuery, useMutation, useQueryClient } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

import {
    TCreateCustomerMajorRequest,
    TCustomerMajorDto,
    TInfoCustomerTableProps,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TUpdateCustomerMajorRequest,
} from "../../../../types";
import {
    createCustomerMajor,
    deleteCustomerMajor,
    getCustomerMajors,
    updateCustomerMajor,
} from "../../../../api";
import { Button, Modal } from "../../../../components";
import {
    CreateCustomerMajorModal,
    UpdateCustomerMajorModal,
} from "../../../../modal";

type TColumn = TTableColumn<TCustomerMajorDto>;
type TData = TTableData<TCustomerMajorDto>;

const CustomerMajorTable = ({ idCustomer }: TInfoCustomerTableProps) => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdateCustomerMajorRequest | null>(null);
    const [idSelected, setIdSelected] = useState<number | null>(null);

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            ["majors", idCustomer],
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomerMajors({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
                idCustomer: idCustomer as number,
            }),
        onSuccess: (data: TResponsePageDto<TCustomerMajorDto[]>) => {
            if (data?.totalRowCount && data.totalRowCount !== totalRow) {
                setTotalRow(data.totalRowCount);
            }

            if (data?.data) {
                setTableData(data.data);
            }
        },
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            (idCustomer !== -1 || idCustomer !== null),
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const createMutation = useMutation({
        mutationFn: createCustomerMajor,
        onSuccess: (data) => {
            if (data) {
                refetch();
                closeCreateModal();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updateCustomerMajor,
        onSuccess: (data) => {
            if (data) {
                queryClient.invalidateQueries({
                    queryKey: [
                        ["majors", idCustomer],
                        search, //refetch when search changes
                        pagination.pageIndex, //refetch when pagination.pageIndex changes
                        pagination.pageSize, //refetch when pagination.pageSize changes
                    ],
                });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteCustomerMajor,
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
    const openCreateModal = useCallback(() => {
        setIsOpenCreateModal(true);
    }, []);

    const closeCreateModal = useCallback(() => {
        setIsOpenCreateModal(false);
    }, []);

    const handleCreate = useCallback(
        async (data: TCreateCustomerMajorRequest) => {
            const result = (await createMutation.mutateAsync(data)) || false;

            return result;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback((data: TUpdateCustomerMajorRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateCustomerMajorRequest) => {
            const result = (await updateMutation.mutateAsync(data)) || false;

            return result;
        },
        [updateMutation]
    );

    const openDeleteModal = useCallback(() => {
        setIsOpenDeleteModal(true);
    }, []);

    const closeDeleteModal = useCallback(() => {
        setIdSelected(null);
        setIsOpenDeleteModal(false);
    }, []);

    const handleDeleteModal = useCallback(
        async (id: number) => {
            const result = (await deleteMutation.mutateAsync(id)) || false;

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
                accessorKey: "idCustomer",
                header: "Mã khách hàng",
            },
            {
                accessorKey: "nghiepVu",
                header: "Nghiệp vụ",
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
        renderTopToolbarCustomActions: () => (
            <>
                <Button
                    buttonVariant="contained"
                    buttonSize="sm"
                    buttonLabel="tạo mới"
                    buttonRounded="lg"
                    buttonColor="blue"
                    onClick={openCreateModal}
                />
            </>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                <Tooltip arrow placement="left" title="Chỉnh sửa">
                    <IconButton
                        color="primary"
                        onClick={() => {
                            const { id } = row.original;
                            const data = tableData.find(
                                (e) => e.id === id
                            ) as TCustomerMajorDto;
                            const payload: TUpdateCustomerMajorRequest = {
                                id: data.id,
                                idCustomer: data.idCustomer,
                                idNghiepVu: data.idNghiepVu,
                            };
                            openUpdateModal(payload);
                        }}
                    >
                        <Edit />
                    </IconButton>
                </Tooltip>
                <Tooltip arrow placement="right" title="Yêu cầu xoá">
                    <IconButton
                        color="error"
                        onClick={() => {
                            const { id } = row.original as TCustomerMajorDto;
                            setIdSelected(id);
                            openDeleteModal();
                        }}
                    >
                        <Delete />
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
        <div className="section-table">
            <MaterialReactTable table={table} />
            <CreateCustomerMajorModal
                title="Thêm nghiệp vụ"
                isOpen={isOpenCreateModal}
                onClose={closeCreateModal}
                onSubmit={handleCreate}
                isLoading={createMutation.isLoading}
                idCustomer={idCustomer}
            />
            <UpdateCustomerMajorModal
                title="Chỉnh sửa nghiệp vụ"
                isOpen={isOpenUpdateModal}
                onClose={closeUpdateModal}
                onSubmit={handleUpdate}
                prevData={dataSelectedUpdate}
                isLoading={updateMutation.isLoading}
            />
            <Modal
                title="Xoá nghiệp vụ"
                description="bạn muốn thực hiện xoá nghiệp vụ này?"
                isOpen={isOpenDeleteModal}
                onClose={closeDeleteModal}
            >
                <form
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeleteModal(idSelected as number);
                    }}
                >
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
        </div>
    );
};

export default CustomerMajorTable;
