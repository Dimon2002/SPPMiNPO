using SPPMiNPO.DataType;

namespace SPPMiNPO.Generators;

public class DistortingDistribution : IDistribution
{
    private readonly IDistributionParameters _parameters;
    private readonly HuberDistribution _huberDistribution;

    public IDistributionParameters Parameters => _parameters;

    public DistortingDistribution(IDistributionParameters parameters)
    {
        _parameters = parameters;
        _huberDistribution = new((HuberParameters)parameters);
    }

    public double GenerateValue()
    {
        return _huberDistribution.GenerateValue();
    }

    public double[] GenerateValues(int N)
    {
        IList<double> values = [];

        for (int i = 0; i < N; ++i)
        {
            var value = GenerateValue();

            if (i % 5 == 0)
            {
                value *= 10;
            }

            values.Add(value);
        }

        return [.. values];
    }
}
