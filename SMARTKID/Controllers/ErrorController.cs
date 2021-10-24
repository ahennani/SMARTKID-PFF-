using SMARTKID.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // Loggers To Shows Infos in VS Output Like errors, Tests...
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [Route("Error/{StatusCode}")]
        public IActionResult Index(int StatusCode)
        {
            // Allows us to get the URL of the current page..
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var model = new StatusResultViewModel();
            switch (StatusCode)
            {
                case 404:
                    {
                        model.Num = "404";
                        model.Title = "OPPS!! PAGE NOT BE FOUND";
                        model.Message = $"Sorry but the page you are looking for does not exist, have been removed, " +
                                        $"Name changed or is temporarity unavailable.";

                        this._logger.LogError(
                                                    $"\n404 Message Error: {model.Message}," +
                                                    $" \nPath Error: {statusCodeResult.OriginalPath}" +
                                                    $" \nQuery String: {statusCodeResult.OriginalQueryString}\n\n"
                                               );
                    }
                    break;
                case 503:
                    {
                        model.Num = "503";
                        model.Title = "OPPS!! SERVRE ERROR";
                        model.Message = $"The server encountered an error and could not complete your request, Please try again later or refresh the page..";

                        this._logger.LogError(
                                                    $"\n404 Message Error: {model.Message}," +
                                                    $" \nPath Error: {statusCodeResult.OriginalPath}" +
                                                    $" \nQuery String: {statusCodeResult.OriginalQueryString}\n\n"
                                               );
                    }
                    break;
                default:
                    {
                        model.Num = "XXX";
                        model.Title = "OPPS!! SORRY!!!";
                        model.Message = $"Sorry we can not serve your request at the moment, Please come later!!..";

                        this._logger.LogError(
                                                    $"\n\nMessage Error: {model.Message}," +
                                                    $" \nPath Error: {statusCodeResult.OriginalPath}" +
                                                    $" \nQuery String: {statusCodeResult.OriginalQueryString}\n\n"
                                               );
                    }
                    break;
            }

            return View("NotFound", model);
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var model = new StatusResultViewModel();
            model.Num = "X_X";
            model.Title = "OPPS!! SORRY!!!";
            model.Message = $"Sorry we can not serve your request at the moment, Please come later!!..";
            var exceptionSatuts = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (!(exceptionSatuts == null))
            {
                this._logger.LogError(
                                        $"\n\n\n\n\n" +
                                        $"\nMessage Error: {exceptionSatuts.Error.Message}," +
                                        $"\nPath Error: {exceptionSatuts.Path}",
                                        $"\nStack Trace: {exceptionSatuts.Error.StackTrace}" +
                                        $"\n\n\n\n\n\n"
                                     );
            }

            return View(model);
        }
    }
}
