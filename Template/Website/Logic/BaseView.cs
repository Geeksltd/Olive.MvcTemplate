using System;
using Domain;
using Olive;

public abstract class BaseView<TModel> : Olive.Mvc.RazorPage<TModel>
{
    /// <summary>
    /// Gets the user for the current HTTP request.
    /// </summary>
    public new User User => base.User.Extract().AwaitResult() ?? new AnonymousUser();
}