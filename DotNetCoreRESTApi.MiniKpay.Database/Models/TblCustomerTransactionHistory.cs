using System;
using System.Collections.Generic;

namespace DotNetCoreRESTApi.MiniKpay.Database.Models;

public partial class TblCustomerTransactionHistory
{
    public int CustomerTransactionHistoryId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }
}
