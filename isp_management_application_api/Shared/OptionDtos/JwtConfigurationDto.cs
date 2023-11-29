namespace Shared.OptionDtos
{
    public class JwtConfiguration
    {
        public string SigningSecretKey { get; set; } = string.Empty;
        public string EncriptionSecretKey { get; set; } = string.Empty;
    }
}
