using System;
using Microsoft.AspNetCore.Mvc;
using Assistant.Core;

namespace Assistant.Api.Controllers
{
	public class AssistantBaseController : ControllerBase
    {
        protected ActionResult GetErrorResult<TResult>(OperationResult<TResult> result)
        {
            switch (result.Error.Code)
            {
                case Core.ErrorCode.NotFound:
                    return NotFound(result.Error.Message);
                case Core.ErrorCode.Unauthorized:
                    return Unauthorized(result.Error.Message);
                default:
                    return BadRequest(result.Error.Message);
            }
        }
    }
}

