namespace locy_api.Models.Requests
{
    public class AcceptCustomerRequest
    {
        public required long IDNhanVienSale { get; set; }
        public required long[] IdCustomers { get; set; }
    }
}
