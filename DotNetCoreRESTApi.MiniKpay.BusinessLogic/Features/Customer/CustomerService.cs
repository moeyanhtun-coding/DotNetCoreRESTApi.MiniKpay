using System.Security.Cryptography;
using DotNetCoreRESTApi.MiniKpay.Database.Models;
using DotNetCoreRESTApi.MiniKpay.Database.RequestModel.Customer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreRESTApi.MiniKpay.BusinessLogic.Features.Customer;

public class CustomerService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblCustomer> GetCustomers()
    {
        var lst = _db.TblCustomers.ToList();
        return lst;
    }

    public int CreateCustomer(CustomerRequestModel reqModel)
    {
        string customerCode = Ulid.NewUlid().ToString();
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        var hashed = HashValue(reqModel.PinCode, salt);
        _db.TblCustomers.Add(new TblCustomer
        {
            CustomerCode = customerCode,
            CustomerName = reqModel.CustomerName,
            MobileNo = reqModel.MobileNo,
            PinCode = hashed,
            Salt = Convert.ToBase64String(salt)
        });
        _db.TblCustomerBalances.Add(new TblCustomerBalance
        {
            CustomerCode = customerCode,
            Balance = 10000
        });
        var result = _db.SaveChanges();
        return result;
    }

    public int UpdatePinCode(string customerCode, PinCodeRequestModel reqModel)
    {
        var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerCode == customerCode)!;
        var validatePinCode = VerifyPinCode(reqModel.oldPinCode, item.PinCode, item.Salt);
        if (!validatePinCode)
            return 0;
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        var hashed = HashValue(reqModel.newPinCode, salt);

        item.Salt = Convert.ToBase64String(salt);
        item.PinCode = hashed;
        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();
        return result;
    }

    public string HashValue(string pinCode, byte[] salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: pinCode,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
            ));
        return hashed;
    }

    public bool VerifyPinCode(string oldPinCode, string storedHash, string storedSalt)
    {
        byte[] salt = Convert.FromBase64String(storedSalt);
        string hashedPinCode = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: oldPinCode,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
            ));
        return hashedPinCode == storedHash;
    }
}
