﻿namespace locy_api.Models.Requests
{
    public class UpdateTypeOfCustomerRequest
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
    }
}
