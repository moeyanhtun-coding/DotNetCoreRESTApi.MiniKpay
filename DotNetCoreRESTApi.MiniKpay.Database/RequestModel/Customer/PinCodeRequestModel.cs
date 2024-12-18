using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreRESTApi.MiniKpay.Database.RequestModel.Customer
{
    public class PinCodeRequestModel
    {
        [Required(ErrorMessage ="Old PinCode is required")]
        public string oldPinCode { get; set; }

        [Required(ErrorMessage = "New PinCode is required.")]
        [StringLength(6, ErrorMessage = "PinCode must be exactly 6 characters.")]
        [MinLength(6, ErrorMessage = "PinCode must be exactly 6 characters.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PinCode must contain exactly 6 digits.")]
        public string newPinCode { get; set; }
    }
}
