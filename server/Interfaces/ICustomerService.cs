// ** library **
// ** architecture **
using locy_api.Models.Domains;
using locy_api.Models.DTOs;
using locy_api.Models.Requests;

namespace locy_api.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>?> GetCustomers(int Start = 0, int Size = 10, string Search = "", bool isDelete = false);
        Task<int> GetTotalCustomers(string Search = "", bool isDelete = false);
        Task<TblDmcustomer?> GetCustomerById(long id);
        Task<List<TblDmcustomer>?> GetCustomersData(int pageNumber = 0, int pageSize = 10);
        Task CreateCustomer(CreateCustomerRequest req);
        Task UpdateCustomer(TblDmcustomer data, UpdateCustomerRequest req);
        Task DeleteCustomer(TblDmcustomer data, DeleteCustomerRequest req);
        Task<bool> IsExistCodeCustomer(string code);
        Task<bool> IsExistTaxCodeCustomer(string code);

        Task<List<CustomerDto>?> GetCustomersAssigned(int Start = 0, int Size = 10, string Search = "", string permission = "", long idUser = 0);
        Task<int> GetTotalCustomersAssigned(string Search = "", string permission = "", long idUser = 0);
        Task<List<CustomerDto>?> GetCustomersDelivered(int Start = 0, int Size = 10, string Search = "", long idNhanVien = 0);
        Task<int> GetTotalCustomersDelivered(string Search = "", long idNhanVien = 0);
        Task<List<CustomerDto>?> GetCustomersUndelivered(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalCustomersUndelivered(string Search = "");
        Task<List<CustomerDto>?> GetCustomersReceived(int Start = 0, int Size = 10, string Search = "", string permission = "", long idNhanVien = 0);
        Task<int> GetTotalCustomersReceived(string Search = "", string permission = "", long idNhanVien = 0);

        Task<List<TblDmcustomer>?> GetCustomersByIdArray(long[] ids, long? IdNhanVien = null);
        Task ChooseCustomers(List<TblDmcustomer> data, ChooseCustomerRequest req);
        Task DeliveryCustomers(List<TblDmcustomer> data, DeliveryCustomerRequest req);
        Task UndeliveryCustomers(List<TblDmcustomer> data, UndeliveryCustomerRequest req);
        Task AcceptCustomers(List<TblDmcustomer> data, AcceptCustomerRequest req);
        Task DenyCustomers(List<TblDmcustomer> data, DenyCustomerRequest req);
    }
}
