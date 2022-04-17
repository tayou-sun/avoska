using System.Collections.Generic;

public interface IUserRepository {
    User Get (string phone, string password);
    User Create (string phone, string password);
    User Update (UserChange user);
}