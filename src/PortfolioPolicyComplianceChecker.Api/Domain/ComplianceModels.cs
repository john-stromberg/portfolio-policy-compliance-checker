namespace PortfolioPolicyComplianceChecker.Api.Domain;

public sealed class PolicyCheckRequest
{
    public string HouseholdName { get; set; } = string.Empty;
    public int TargetEquityPercent { get; set; }
    public int CurrentEquityPercent { get; set; }
    public int DriftThresholdPercent { get; set; } = 5;
    public int ConcentrationLimitPercent { get; set; } = 15;
    public List<HoldingInput> Holdings { get; set; } = [];
    public List<string> RestrictedTickers { get; set; } = [];
}

public sealed class HoldingInput
{
    public string Ticker { get; set; } = string.Empty;
    public decimal MarketValue { get; set; }
}

public sealed class PolicyException
{
    public string Rule { get; set; } = string.Empty;
    public string Severity { get; set; } = "Warning";
    public string Message { get; set; } = string.Empty;
}

public sealed class PolicyCheckResult
{
    public string HouseholdName { get; set; } = string.Empty;
    public bool IsCompliant { get; set; }
    public List<PolicyException> Exceptions { get; set; } = [];
    public bool RebalanceTriggered { get; set; }
}
