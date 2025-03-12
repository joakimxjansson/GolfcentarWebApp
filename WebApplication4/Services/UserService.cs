namespace WebApplication4.Services;
using WebApplication4.Data;

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
}
