using Microsoft.AspNetCore.Mvc;
using WarehouseManagementService.Domain.Utilities;

namespace WarehouseManagementService.Controllers
{
    public abstract class CommonController : ControllerBase
    {
        protected ActionResult<T> ReplyCommonResponse<T>(CommonResponseType<T> response) where T : class
        {
            if (response.StatusCode == 204)
            {
                return NoContent();
            }

            bool flag = response.IsSuccessStatusCode();
            if (Is201SuccessStatusCode(response.StatusCode, flag))
            {
                return StatusCode(201, response.Resource);
            }

            if (flag)
            {
                if (response.Resource != null)
                {
                    return Ok(response.Resource);
                }

                return Ok();
            }

            if (response.Resource != null)
            {
                return StatusCode(response.StatusCode, response.Resource);
            }

            return StatusCode(response.StatusCode, response.Message);
        }

        private bool Is201SuccessStatusCode(int statusCode, bool successCode)
        {
            if (statusCode != 201)
            {
                if (successCode)
                {
                    return HttpMethods.IsPost(Request?.Method);
                }

                return false;
            }
            return true;
        }

    }
}
