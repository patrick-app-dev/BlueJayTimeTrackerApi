namespace EFDataAccess.MockData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Interfaces;
    using Core.Models;

    public class MockProjectRepository : IProjectRepository
    {
        private static readonly List<Project> Projects = new List<Project>()
        {
            new Project()
            {
                 Description = "Member Information Management System",
                 Title = "MIMS",
                 ProjectId = 1,
                 Created = DateTimeOffset.UtcNow.AddDays(-8),
                 Modified = DateTimeOffset.UtcNow.AddDays(-1)
            },
            new Project()
            {
                Description = "Api for online dues payment app",
                Title = "Payment Api",
                ProjectId = 2,
                 Created = DateTimeOffset.UtcNow.AddDays(-1),
                 Modified = DateTimeOffset.UtcNow.AddDays(-1)
            },
            new Project()
            {
                Description = "Allow lookup of Eligibility and Benefit Claims",
                Title = "Online Benefits Lookup",
                ProjectId = 3,
                 Created = DateTimeOffset.UtcNow.AddDays(-12),
                 Modified = DateTimeOffset.UtcNow.AddDays(-6)
            },
            new Project()
            {
                Description = "Application for booking members into Safety Training Courses",
                Title = "Training App",
                ProjectId = 4,
                 Created = DateTimeOffset.UtcNow.AddDays(-6),
                 Modified = DateTimeOffset.UtcNow.AddDays(-2)
            }

        };
        public Task<Project> AddAsync(Project entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentException("Project was not defined");
            }
            entity.ProjectId = Projects.Max(m => m.ProjectId) + 1;
            Projects.Add(entity);
            return Task.FromResult(entity);
        }

        public Task AddRangeAsync(IEnumerable<Project> entities, CancellationToken cancellationToken)
        {
            if (entities == null || ! entities.Any())
            {
                throw new ArgumentException("Projects are undefined or empty");
            }
            foreach(var p in entities)
            {
                p.ProjectId = Projects.Max(m => m.ProjectId) + 1;
                Projects.Add(p);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Find expression is null");
            }

            return Task.FromResult(Projects.Where(predicate.Compile()));

        }
        public Task<Project> GetAsync(int id, CancellationToken cancellationToken) => Task.FromResult(Projects.Where(p => p.ProjectId == id).FirstOrDefault());


        public Task<List<Project>> GetAllAsync(int? first, DateTimeOffset? createdAfter, DateTimeOffset? createdBefore, CancellationToken cancellationToken) =>
            Task.FromResult(Projects
                .OrderBy(x => x.Created)
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .If(first.HasValue, x => x.Take(first.Value))
                .ToList());

        public Task<List<Project>> GetProjectsReverseAsync(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Projects
                .OrderBy(x => x.Created)
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .If(last.HasValue, x => x.Take(last.Value))
                .ToList());


        public Task RemoveAsync(Project entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentException("Project is undefined.");
            }
            Projects.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<Project> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentException("No projects to remove have been defined");
            }
            foreach(var p in entities)
            {
                Projects.Remove(p);
            }
            return Task.CompletedTask;
        }
        public Task<bool> GetHasNextPageAsync(
            int? first,
            DateTimeOffset? createdAfter,
            CancellationToken cancellationToken) =>
            Task.FromResult(Projects
                .OrderBy(x => x.Created)
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .Skip(first.Value)
                .Any());

        public Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Projects
                .OrderBy(x => x.Created)
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .SkipLast(last.Value)
                .Any());

        public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) => Task.FromResult(Projects.Count);

        public Task<Project> UpdateAsync(Project project, CancellationToken cancellationToken)
        {
            if (project is null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var existingCar = Projects.FirstOrDefault(x => x.ProjectId == project.ProjectId);
            existingCar.Description = project.Description;
            existingCar.Title = project.Title;
            return Task.FromResult(project);
        }
    }
}
