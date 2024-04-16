using BlogProject.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogProject.Presentation.Controllers
{
    public class BaseController : Controller
    {
        IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Notify(string message, string title = "Sweet alert Demo"
            , NotificationType type = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = type.ToString(),
                type = type.ToString(),
                provider = _configuration["NotificationProvider"]
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }
    }
}
