using DotNetCoreRESTApi.MiniKpay.Database.Models;
using DotNetCoreRESTApi.MiniKpay.Database.RequestModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreRESTApi.MiniKpay.BusinessLogic.Features.Customer;

public class CustomerService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblCustomer> GetCustomers()
    {
       var lst =  _db.TblCustomers.ToList();
        return lst;
    }

    public int CreateCustomer(CustomerRequestModel reqModel)
    {
        _db.Add(new TblCustomer
        {
            CustomerCode = Ulid.NewUlid().ToString(),
            CustomerName = reqModel.CustomerName,
            MobileNo = reqModel.MobileNo,
        });
        var result = _db.SaveChanges();
        return result;
    }
}
