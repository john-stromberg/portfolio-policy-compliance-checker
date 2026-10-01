using PortfolioPolicyComplianceChecker.Api.Domain;

namespace PortfolioPolicyComplianceChecker.Api.Application.Contracts;

public interface IComplianceService
{
    PolicyCheckResult Evaluate(PolicyCheckRequest request);
}
