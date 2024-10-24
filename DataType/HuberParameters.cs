namespace SPPMiNPO.DataType;

public record struct HuberParameters(double V, double K, double Variance, double Gamma, double P) : IDistributionParameters;