using CrudApiWithAuthentication.Models;

namespace CrudApiWithAuthentication.Interface
{
    public interface IJWTservices
    {
        Tokens Authenticate(UserLogin userLogin);

    }
}
