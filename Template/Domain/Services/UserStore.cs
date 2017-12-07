namespace Domain
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Olive.Entities;

    public class UserStore : IUserStore<User>
    {
        static IDatabase Database => Entity.Database;

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            Database.Save(user.IsNew ? user : user.Clone(), SaveBehaviour.Default);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            Database.Delete(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose() { /*Nothing to do*/ }

        public Task<User> FindByIdAsync(string userId, CancellationToken token) =>
            Database.GetOrDefault<User>(userId);

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken token) =>
            Database.FirstOrDefault<User>(user => user.Name == normalizedUserName);

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken token) =>
            Task.FromResult(user.Name.Trim());

        public Task<string> GetUserIdAsync(User user, CancellationToken token) =>
            Task.FromResult(user.ID.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken token) =>
            Task.FromResult(user.Name);

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken token)
        {
            user.FirstName = normalizedName;
            user.LastName = string.Empty;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken token)
        {
            user.FirstName = userName;
            user.LastName = string.Empty;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken token)
        {
            Database.Save(user, SaveBehaviour.Default);
            return Task.FromResult(IdentityResult.Success);
        }
    }
}