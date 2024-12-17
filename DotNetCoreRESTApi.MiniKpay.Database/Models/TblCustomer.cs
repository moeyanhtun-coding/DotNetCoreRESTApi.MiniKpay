using System;
using System.Collections.Generic;

namespace DotNetCoreRESTApi.MiniKpay.Database.Models;

public partial class TblCustomer
{
    public int CustomerId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;
}
