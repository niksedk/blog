namespace SubIt.Features.Security
{
    public class RsaParametersJson
    {
        public string Modulus { get; internal set; }
        public string Exponent { get; internal set; }
        public string P { get; internal set; }
        public string Q { get; internal set; }
        public string DQ { get; internal set; }
        public string DP { get; internal set; }
        public string InverseQ { get; internal set; }
        public string D { get; internal set; }
    }
}
