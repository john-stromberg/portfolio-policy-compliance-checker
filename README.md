> **Moved:** This project is maintained in [advisor-platform/projects/portfolio-policy-compliance-checker](https://github.com/john-stromberg/advisor-platform/tree/main/projects/portfolio-policy-compliance-checker).
> This repository is retained as a historical snapshot.

---

# Portfolio Policy Compliance Checker

Workflow-oriented advisor tool for IPS compliance monitoring and exception reporting.

## Stack
- .NET 10 ASP.NET Core Web API
- Lightweight web UI for policy checks

## MVP Features
- Allocation drift check vs threshold
- Concentration-limit checks by holding weight
- Restricted-holding detection
- Rebalance trigger flag
- Exception report output for advisor review

## Run
```bash
dotnet restore portfolio-policy-compliance-checker.slnx
dotnet run --project src/PortfolioPolicyComplianceChecker.Api/PortfolioPolicyComplianceChecker.Api.csproj
```

Open:
- `/` for UI
- `POST /api/policy-compliance/evaluate` for API
