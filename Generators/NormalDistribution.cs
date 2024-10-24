using SPPMiNPO.DataType;

namespace SPPMiNPO.Generators;

public class NormalDistribution : IDistribution
{
    private readonly Random _random = new();

    public IDistributionParameters Parameters { get; }

    public NormalDistribution(NormalParameters? normalParameters = null)
    {
        Parameters = normalParameters ?? new NormalParameters();
    }

    public double Density(double x)
    {
        throw new NotImplementedException();
    }
    
    public double Generate()
    {
        var firstRandomValue = GetRandomValue();
        var secondRandomValue = GetRandomValue();

        return Math.Sqrt(-2 * Math.Log(firstRandomValue)) * Math.Cos(2 * Math.PI * secondRandomValue);
    }

    private double GetRandomValue()
    {
        return _random.NextDouble();
    }
}
