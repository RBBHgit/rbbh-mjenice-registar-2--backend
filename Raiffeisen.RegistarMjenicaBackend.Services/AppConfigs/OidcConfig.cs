namespace Raiffeisen.RegistarMjenicaBackend.Services.AppConfigs;

public class OidcConfig
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Authority { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string OpenIdConnectResponseType { get; set; }
    public bool GetClaimsFromUserInfoEndpoint { get; set; }
    public bool SaveTokens { get; set; }
    public bool MapInboundClaims { get; set; }
}