namespace SPPMiNPO.DataType;

public record struct DistortingParameters(double V, double K, double Variance, double Gamma, double P) : IDistributionParameters;
