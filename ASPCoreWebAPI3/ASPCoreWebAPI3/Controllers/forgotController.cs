//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ASPCoreWebAPI3.Data;
//using ASPCoreWebAPI3.Models; // Import the model for the request body

//namespace ASPCoreWebAPI3.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ForgotController : ControllerBase
//    {
//        private readonly ForgotPassword _fp;

//        public ForgotController(ForgotPassword fp)
//        {
//            _fp = fp;
//        }

//        [HttpPut]
//        public IActionResult Forgot([FromBody] forgotmodel model)
//        {
//            try
//            {
//                _fp.Forgotpassword(model.email, model.newpassword);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
