
namespace qlsv.Models;

public class UserToken
{
    public string Id { get; set; }
    public string AccessTokenId { get; set; }
    public string RefreshTokenId { get; set; }
    public string UserId { get; set; }
    // 
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public UserCustom User { get; set; }
}
