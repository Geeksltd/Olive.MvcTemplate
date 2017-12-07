namespace Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Olive.Entities;

    public class RoleStore : IRoleStore<string>
    {
        public Task<IdentityResult> CreateAsync(string role, CancellationToken token) =>
            throw new NotSupportedException();

        public Task<IdentityResult> DeleteAsync(string role, CancellationToken token) =>
            throw new NotSupportedException();

        public void Dispose() { }

        public Task<string> FindByIdAsync(string roleId, CancellationToken token) =>
            Task.FromResult(roleId);

        public Task<string> FindByNameAsync(string role, CancellationToken token) =>
            Task.FromResult(role);

        public Task<string> GetNormalizedRoleNameAsync(string role, CancellationToken token) =>
            Task.FromResult(role);

        public Task<string> GetRoleIdAsync(string role, CancellationToken token) =>
            Task.FromResult(role);

        public Task<string> GetRoleNameAsync(string role, CancellationToken cancellationToken) =>
            Task.FromResult(role);

        public Task SetNormalizedRoleNameAsync(string role, string normalizedName, CancellationToken token) =>
            Task.CompletedTask;

        public Task SetRoleNameAsync(string role, string roleName, CancellationToken token) =>
            Task.CompletedTask;

        public Task<IdentityResult> UpdateAsync(string role, CancellationToken token) =>
            Task.FromResult(IdentityResult.Success);
    }
}
