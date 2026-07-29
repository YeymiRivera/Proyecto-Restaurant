using System.Security.Claims; 
using Microsoft.AspNetCore.Components.Authorization; 

namespace proyecto.Services;

public class CustomAuthStateProvider:AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity()); 
    private ClaimsPrincipal _currentUser; 
     ClaimsIdentity? identity;
    public CustomAuthStateProvider() { _currentUser = _anonymous; } 
    public override Task<AuthenticationState> GetAuthenticationStateAsync() => 
    Task.FromResult(new AuthenticationState(_currentUser)); 
    public void MarkUserAsAuthenticated(string email, string id_rol,string name,string id_user) 
    {
          identity = new ClaimsIdentity(new[] 
         { new Claim(ClaimTypes.Email, email), 
         new Claim(ClaimTypes.Role, id_rol),new Claim(ClaimTypes.Name, name),
         new Claim(ClaimTypes.NameIdentifier, id_user) }
          , "CustomAuthCervantes"); 
          _currentUser = new ClaimsPrincipal(identity); 
          NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()); 
          } 
          public void MarkUserAsLoggedOut() 
          { 
            _currentUser = _anonymous; NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
           } 
}