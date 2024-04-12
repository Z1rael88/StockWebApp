using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ITokenService
{
    string Createtoken(AppUser appUser);
}