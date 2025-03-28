namespace DrugIndication.Application.Dtos
{
    public class JwtDto(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}
