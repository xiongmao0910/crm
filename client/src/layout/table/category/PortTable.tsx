import { FormEvent, useCallback, useMemo, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

import { createPort, deletePort, getPorts, updatePort } from "../../../api";
import { CreatePortModal, UpdatePortModal } from "../../../modal";
import { TAuthContextProps, useAuth } from "../../../contexts";
import {
    TCreatePortRequest,
    TPortDto,
    TResponsePageDto,
    TTableColumn,
    TTableData,
    TUpdatePortRequest,
} from "../../../types";
import { Button, Modal } from "../../../components";

type TColumn = TTableColumn<TPortDto>;
type TData = TTableData<TPortDto>;

const PortTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdatePortRequest | null>(null);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );

    const { currentUser }: TAuthContextProps = useAuth();

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "ports",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getPorts({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
            }),
        onSuccess: (data: TResponsePageDto<TPortDto[]>) => {
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
        mutationFn: createPort,
        onSuccess: (data) => {
            if (data) {
                refetch();
                closeCreateModal();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updatePort,
        onSuccess: (data) => {
            if (data) {
                queryClient.invalidateQueries({ queryKey: "ports" });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deletePort,
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
        async (data: TCreatePortRequest) => {
            const result = (await createMutation.mutateAsync(data)) || false;

            return result;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback((data: TUpdatePortRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdatePortRequest) => {
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
        async (index: number) => {
            const result = (await deleteMutation.mutateAsync(index)) || false;

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
            },
            {
                accessorKey: "nameVI",
                header: "Tên VI",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "nameEN",
                header: "Tên EN",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "addressVI",
                header: "Địa chỉ VI",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "addressEN",
                header: "Địa chỉ EN",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "quocGia",
                header: "Quốc gia",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "thanhPho",
                header: "Thành phố",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "phone",
                header: "Số điện thoại",
            },
            {
                accessorKey: "fax",
                header: "FAX",
            },
            {
                accessorKey: "email",
                header: "Thư điện tử",
            },
            {
                accessorKey: "website",
                header: "Trang web",
            },
            {
                accessorKey: "note",
                header: "Ghi chú",
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
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("6000")) && (
                    <Button
                        buttonVariant="contained"
                        buttonSize="sm"
                        buttonLabel="tạo mới"
                        buttonRounded="lg"
                        buttonColor="blue"
                        onClick={openCreateModal}
                    />
                )}
            </>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("6000")) && (
                    <>
                        <Tooltip arrow placement="left" title="Chỉnh sửa">
                            <IconButton
                                color="primary"
                                onClick={() => {
                                    const { id } = row.original;
                                    const data = tableData.find(
                                        (e) => e.id === id
                                    ) as TPortDto;

                                    openUpdateModal(data);
                                }}
                            >
                                <Edit />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
                {(currentUser?.permission.includes("1048576") ||
                    currentUser?.permission.includes("6000")) && (
                    <>
                        <Tooltip arrow placement="right" title="Yêu cầu xoá">
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id } = row.original as TPortDto;
                                    setIdSelectedDelete(id);
                                    openDeleteModal();
                                }}
                            >
                                <Delete />
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
            <CreatePortModal
                title="Thêm cảng"
                isOpen={isOpenCreateModal}
                onClose={closeCreateModal}
                onSubmit={handleCreate}
                isLoading={createMutation.isLoading}
            />
            <UpdatePortModal
                title="Chỉnh sửa cảng"
                isOpen={isOpenUpdateModal}
                onClose={closeUpdateModal}
                onSubmit={handleUpdate}
                prevData={dataSelectedUpdate}
                isLoading={updateMutation.isLoading}
            />
            <Modal
                title="Xoá cảng"
                description="bạn muốn thực hiện xoá cảng này?"
                isOpen={isOpenDeleteModal}
                onClose={closeDeleteModal}
            >
                <form
                    onSubmit={(e: FormEvent) => {
                        e.preventDefault();
                        handleDeleteModal(idSelectedDelete as number);
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
        </>
    );
};

export default PortTable;