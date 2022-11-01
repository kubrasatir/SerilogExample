using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using SerilogExample.Exceptions;
using SerilogExample.Models;
using System;
using System.Diagnostics;
using System.Threading;

namespace SerilogExample.Controllers
{
    public class HomeController : Controller
    {
        private static int _callCount;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
        }

        public IActionResult Index()
        {
            var user = new UserIdentity() { Name = "Kübra" };
            _logger.LogInformation("Index çalıştı!{@UserIdentity}", user);
            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
            throw new BusinessException("test hata");
            _logger.LogInformation("hata alınd!{@UserIdentity} {@_callcount}", user, _callCount);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
