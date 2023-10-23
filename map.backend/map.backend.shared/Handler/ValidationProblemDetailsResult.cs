using map.backend.shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace map.backend.shared.Handler
{
    public class ValidationProblemDetailsResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            //var keys = context.ModelState.Keys;
            string desc = "";
            var dic = context.ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            foreach (var item in dic)
            {
                foreach (var item1 in item.Value)
                    desc += (item1 + "\n");
            }
            //var problemDetails = new
            //{
            //    Status = StatusCodes.Status400BadRequest,
            //    Errors = dic
            //};
            message_response res = new message_response();
            res.resCode = StatusCodes.Status400BadRequest.ToString();
            res.resDesc = desc;
            var objectResult = new ObjectResult(res) { StatusCode = StatusCodes.Status400BadRequest };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
