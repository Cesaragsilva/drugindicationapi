namespace DrugIndication.Domain.Entities;

public class Program
{
    public string Id { get; set; } = null!;
    public string ProgramName { get; set; } = null!;
    public List<string> CoverageEligibilities { get; set; } = [];
    public string ProgramType { get; set; } = null!;

    public List<Requirement> Requirements { get; set; } = [];
    public List<Benefit> Benefits { get; set; } = [];
    public List<Form> Forms { get; set; } = [];
    public FundingInfo Funding { get; set; } = new();
    public List<ProgramDetails> Details { get; set; } = [];
}

public class Requirement
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}

public class Benefit
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}

public class Form
{
    public string Name { get; set; } = null!;
    public string Link { get; set; } = null!;
}

public class FundingInfo
{
    public string Evergreen { get; set; } = "false";
    public string CurrentFundingLevel { get; set; } = "Data Not Available";
}

public class ProgramDetails
{
    public string Eligibility { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string Renewal { get; set; } = null!;
    public string Income { get; set; } = null!;
}
