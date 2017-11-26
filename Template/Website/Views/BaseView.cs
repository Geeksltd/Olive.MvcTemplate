using Domain;
using Olive.Mvc;

public abstract class BaseView<TModel> : RazorPage<TModel>
{
    /// <summary>
    /// Gets the user for the current HTTP request.
    /// </summary>
    public new User User => base.User?.Identity.Extract<User>() ?? new AnonymousUser();
}