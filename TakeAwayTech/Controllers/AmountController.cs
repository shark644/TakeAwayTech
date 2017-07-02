using System.Web.Http;
using TakeAwayTech.Classes;

namespace TakeAwayTech.Controllers
{
    public class AmountController : ApiController
    {
        private readonly IAmountParser _amountParser;

        //Constructor 
        public AmountController(IAmountParser amountParser)
        {
            _amountParser = amountParser;
        }

        // GET amount in words

        [Route("api/Amount/{amount:double}/")]
        public string Get(double amount)
        {
            return _amountParser.GetAmountInWords(amount);
        }
    }
}
