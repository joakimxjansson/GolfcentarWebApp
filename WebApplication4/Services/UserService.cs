namespace WebApplication4.Services;

using WebApplication4.Data;

// Denna klass används för att hämta diverse info om inloggad användare (Se metodnamn) //Joakim
public class UserService {
    private readonly GolfContext _contexts;

    public UserService(GolfContext contexts) {
        _contexts = contexts;
    }

    public string GetUsername(int id) {
        var user = _contexts.Users.Find(id);
        return user.Username;
    }

    public string GetName(int id) {
        var user = _contexts.Users.Find(id);
        return user.FirstName;
    }

    public int GetSaldo(int id) {
        var user = _contexts.Users.Find(id);
        return user.Saldo;
    }

    public string GetImage(int id) {
        var user = _contexts.Users.Find(id);
        return user.UserImage;
    }

    public string GetEmail(int id) {
        var user = _contexts.Users.Find(id);
        return user.Email;
    }

    public int GetRole(int id) {
        var user = _contexts.Users.Find(id);
        return user.Admin;
    }
}