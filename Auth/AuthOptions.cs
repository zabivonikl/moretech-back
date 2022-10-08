using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MoretechBack.Auth;

public static class AuthOptions
{
    const string Key = "Very secret key";
    
    public const int Lifetime = 60;
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}