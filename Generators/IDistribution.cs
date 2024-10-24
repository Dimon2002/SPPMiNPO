using SPPMiNPO.DataType;

namespace SPPMiNPO.Generators;

public interface IDistribution : IGenerator
{
    IDistributionParameters Parameters { get; }
}
