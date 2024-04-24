namespace locy_api.Models.Requests
{
    public class DeleteCustomerRequest
    {
        public long Id { get; set; }
        public long? IdUserDelete { get; set; }
        public required bool FlagDel { get; set; }
        public string? LyDoXoa { get; set; }
    }
}
