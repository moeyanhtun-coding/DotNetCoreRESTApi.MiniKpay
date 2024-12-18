using DotNetCoreRESTApi.MiniKpay.BusinessLogic.Features.Customer;
using DotNetCoreRESTApi.MiniKpay.Database.Models;
using DotNetCoreRESTApi.MiniKpay.Database.RequestModel.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreRESTApi.MiniKpay.Controllers.Customer;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService = new CustomerService();

    [HttpGet]
    public IActionResult GetCustomers()
    {
        var customerList = _customerService.GetCustomers();
        if (customerList.Count is 0)
        {
            return Ok(new { message = "CustomerList is Empty"});
        }
        return Ok(customerList);
    }

    [HttpPost]
    public IActionResult CreateCustomer(CustomerRequestModel reqModel)
    {
        var result = _customerService.CreateCustomer(reqModel);
        if (result is 0)
        {
            return BadRequest(new { message = "Customer Creation Fail" });
        }
        return Ok(new { message = "Customer Creation Successful" });
    }

    [HttpPatch("{customerCode}")]
    public IActionResult UpdatePinCode(string customerCode, PinCodeRequestModel reqModel)
    {
        try
        {
            var result = _customerService.UpdatePinCode(customerCode, reqModel);
            if (result is 0)
            {
                return BadRequest(new { message = "PinCode Update Fail" });
            }
            return Ok(new { message = "PinCode Update Successful" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }
}
