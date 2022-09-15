using System.Collections.Generic;

public interface IUserRepository {
    User Get (string phone);
    User Create (string phone, string password);
    User Update (UserChange user);

    void SaveToken (UserVerify user);
    UserVerify Verify (User1 user);
}