using PortfolioPolicyComplianceChecker.Api.Application.Contracts;
using PortfolioPolicyComplianceChecker.Api.Domain;

namespace PortfolioPolicyComplianceChecker.Api.Application.Services;

public sealed class ComplianceService : IComplianceService
{
    public PolicyCheckResult Evaluate(PolicyCheckRequest request)
    {
        var exceptions = new List<PolicyException>();

        // Drift check
        var drift = Math.Abs(request.CurrentEquityPercent - request.TargetEquityPercent);
        var rebalanceTriggered = drift > request.DriftThresholdPercent;
        if (rebalanceTriggered)
        {
            exceptions.Add(new PolicyException
            {
                Rule = "Allocation Drift",
                Severity = "High",
                Message = $"Equity drift is {drift}% vs threshold {request.DriftThresholdPercent}%."
            });
        }

        // Concentration check
        var total = request.Holdings.Sum(h => h.MarketValue);
        if (total > 0)
        {
            foreach (var h in request.Holdings)
            {
                var weight = (h.MarketValue / total) * 100m;
                if (weight > request.ConcentrationLimitPercent)
                {
                    exceptions.Add(new PolicyException
                    {
                        Rule = "Concentration",
                        Severity = "Medium",
                        Message = $"{h.Ticker} concentration {weight:F1}% exceeds limit {request.ConcentrationLimitPercent}%"
                    });
                }
            }
        }

        // Restricted holdings check
        var restrictedSet = request.RestrictedTickers
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Select(t => t.Trim().ToUpperInvariant())
            .ToHashSet();

        foreach (var h in request.Holdings)
        {
            if (restrictedSet.Contains(h.Ticker.Trim().ToUpperInvariant()))
            {
                exceptions.Add(new PolicyException
                {
                    Rule = "Restricted Holdings",
                    Severity = "High",
                    Message = $"Holding {h.Ticker} is restricted by IPS policy."
                });
            }
        }

        return new PolicyCheckResult
        {
            HouseholdName = request.HouseholdName,
            Exceptions = exceptions,
            IsCompliant = exceptions.Count == 0,
            RebalanceTriggered = rebalanceTriggered
        };
    }
}
