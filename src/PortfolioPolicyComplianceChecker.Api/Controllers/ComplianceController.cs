using Microsoft.AspNetCore.Mvc;
using PortfolioPolicyComplianceChecker.Api.Application.Contracts;
using PortfolioPolicyComplianceChecker.Api.Domain;

namespace PortfolioPolicyComplianceChecker.Api.Controllers;

[ApiController]
[Route("api/policy-compliance")]
public sealed class ComplianceController : ControllerBase
{
    private readonly IComplianceService _complianceService;

    public ComplianceController(IComplianceService complianceService)
    {
        _complianceService = complianceService;
    }

    [HttpPost("evaluate")]
    public ActionResult<PolicyCheckResult> Evaluate([FromBody] PolicyCheckRequest request)
    {
        return Ok(_complianceService.Evaluate(request));
    }
}
