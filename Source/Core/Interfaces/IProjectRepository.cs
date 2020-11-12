namespace Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Models;

    public interface IProjectRepository 
    {
        Task<Project> GetAsync(int id, CancellationToken cancellationToken);
        Task<List<Project>> GetAllAsync(int? first,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);
        Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken);

        Task<Project> AddAsync(Project entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<Project> entities, CancellationToken cancellationToken);
        Task RemoveAsync(Project entity, CancellationToken cancellationToken);
        Task RemoveRangeAsync(IEnumerable<Project> entities, CancellationToken cancellationToken);
        Task<List<Project>> GetProjectsReverseAsync(int? last, DateTimeOffset? createdAfter, DateTimeOffset? createdBefore, CancellationToken cancellationToken);
        Task<bool> GetHasNextPageAsync(
            int? first,
            DateTimeOffset? createdAfter,
            CancellationToken cancellationToken);

        Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

        Task<Project> UpdateAsync(Project project, CancellationToken cancellationToken);
    }
}
