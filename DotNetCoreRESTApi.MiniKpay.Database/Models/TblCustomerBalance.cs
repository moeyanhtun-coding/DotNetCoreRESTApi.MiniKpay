using System;
using System.Collections.Generic;

namespace DotNetCoreRESTApi.MiniKpay.Database.Models;

public partial class TblCustomerBalance
{
    public int CustomerBalanceId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public decimal Balance { get; set; }
}
