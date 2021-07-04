using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.Service.Interface;

namespace LetsCode.Resistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : CustomControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("Traitors")]
        [SwaggerOperation(
            Summary = "Returns the Report containing the Percentage of Traitors",
            Description = "Returns the Report containing the Percentage of Traitors",
            OperationId = "Report.Traitors",
            Tags = new[] { "Report" })
        ]
        public async Task<IActionResult> Traitors()
        {
            var report = await _service.TraitorsReport();
            return Ok(report);
        }

        [HttpGet("Rebels")]
        [SwaggerOperation(
            Summary = "Returns the Report containing the Percentage of Rebels",
            Description = "Returns the Report containing the Percentage of Rebels",
            OperationId = "Report.Rebels",
            Tags = new[] { "Report" })
        ]
        public async Task<IActionResult> Rebels()
        {
            var report = await _service.RebelsReport();
            return Ok(report);
        }

        [HttpGet("AverageResource")]
        [SwaggerOperation(
            Summary = "Returns the Report containing the Resource Average by type per Rebel",
            Description = "Returns the Report containing the Resource Average by type per Rebel",
            OperationId = "Report.AverageResource",
            Tags = new[] { "Report" })
        ]
        public async Task<IActionResult> AverageResource()
        {
            var report = await _service.AverageResourceReport();
            return Ok(report);
        }

        [HttpGet("Losses")]
        [SwaggerOperation(
            Summary = "Returns the Report containing the LossesReport in Points due to Treason",
            Description = "Returns the Report containing the LossesReport in Points due to Treason",
            OperationId = "Report.Losses",
            Tags = new[] { "Report" })
        ]
        public async Task<IActionResult> Losses()
        {
            var report = await _service.LossesReport();
            return Ok(report);
        }
    }
}