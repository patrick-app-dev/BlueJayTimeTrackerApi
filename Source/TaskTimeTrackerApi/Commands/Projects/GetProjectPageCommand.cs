namespace TaskTimeTrackerApi.Commands.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using TaskTimeTrackerApi.ViewModels;
    using Core.Models;
    using TaskTimeTrackerApi.Constants;

    public class GetProjectPageCommand : IGetProjectPageCommand
    {
        private const string LinkHttpHeaderName = "Link";
        private readonly IProjectRepository projectRepository;
        private readonly IMapper<Core.Models.Project, ViewModels.Project> projectMapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;
        private const int DefaultPageSize = 3;

        public GetProjectPageCommand(IProjectRepository projectRepository, IMapper<Core.Models.Project, ViewModels.Project> projectMapper,
            IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator)
        {
            this.projectRepository = projectRepository;
            this.projectMapper = projectMapper;
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }
        public async Task<IActionResult> ExecuteAsync(PageOptions pageOptions, CancellationToken cancellationToken = default)
        {
            if (pageOptions is null)
            {
                throw new ArgumentNullException(nameof(pageOptions));
            }

            pageOptions.First = !pageOptions.First.HasValue && !pageOptions.Last.HasValue ? DefaultPageSize : pageOptions.First;
            var createdAfter = Cursor.FromCursor<DateTimeOffset?>(pageOptions.After);
            var createdBefore = Cursor.FromCursor<DateTimeOffset?>(pageOptions.Before);

            var getProjectsTask = this.GetProjectsAsync(pageOptions.First, pageOptions.Last, createdAfter, createdBefore, cancellationToken);
            var getHasNextPageTask = this.GetHasNextPageAsync(pageOptions.First, createdAfter, createdBefore, cancellationToken);
            var getHasPreviousPageTask = this.GetHasPreviousPageAsync(pageOptions.Last, createdAfter, createdBefore, cancellationToken);
            var totalCountTask = this.projectRepository.GetTotalCountAsync(cancellationToken);

            await Task.WhenAll(getProjectsTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask).ConfigureAwait(false);
            var projects = await getProjectsTask.ConfigureAwait(false);
            var hasNextPage = await getHasNextPageTask.ConfigureAwait(false);
            var hasPreviousPage = await getHasPreviousPageTask.ConfigureAwait(false);
            var totalCount = await totalCountTask.ConfigureAwait(false);

            if (projects is null)
            {
                return new NotFoundResult();
            }

            var (startCursor, endCursor) = Cursor.GetFirstAndLastCursor(projects, x => x.Created);
            var projectViewModels = this.projectMapper.MapList(projects);

            var connection = new Connection<ViewModels.Project>()
            {
                PageInfo = new PageInfo()
                {
                    Count = projectViewModels.Count,
                    HasNextPage = hasNextPage,
                    HasPreviousPage = hasPreviousPage,
                    NextPageUrl = hasNextPage ? new Uri(this.linkGenerator.GetUriByRouteValues(
                        this.httpContextAccessor.HttpContext,
                        ProjectsControllerRoute.GetProjectPage,
                        new PageOptions()
                        {
                            First = pageOptions.First,
                            Last = pageOptions.Last,
                            After = endCursor,
                        })) : null,
                    PreviousPageUrl = hasPreviousPage ? new Uri(this.linkGenerator.GetUriByRouteValues(
                        this.httpContextAccessor.HttpContext,
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            First = pageOptions.First,
                            Last = pageOptions.Last,
                            Before = startCursor,
                        })) : null,
                    FirstPageUrl = new Uri(this.linkGenerator.GetUriByRouteValues(
                        this.httpContextAccessor.HttpContext,
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            First = pageOptions.First ?? pageOptions.Last,
                        })),
                    LastPageUrl = new Uri(this.linkGenerator.GetUriByRouteValues(
                        this.httpContextAccessor.HttpContext,
                        ProjectsControllerRoute.GetProjectPage,
                        new PageOptions()
                        {
                            Last = pageOptions.First ?? pageOptions.Last,
                        })),
                },
                TotalCount = totalCount,
            };
            connection.Items.AddRange(projectViewModels);

            this.httpContextAccessor.HttpContext.Response.Headers.Add(
                LinkHttpHeaderName,
                connection.PageInfo.ToLinkHttpHeaderValue());

            return new OkObjectResult(connection);
        }

        private Task <List<Core.Models.Project>> GetProjectsAsync(int? first, int? last, DateTimeOffset? createdAfter, DateTimeOffset? createdBefore, CancellationToken cancellationToken)
        {
            Task<List<Core.Models.Project>> getProjectsTask;
            if (first.HasValue)
            {
                getProjectsTask = this.projectRepository.GetAllAsync(first, createdAfter, createdBefore, cancellationToken);
            }
            else
            {
                getProjectsTask = this.projectRepository.GetProjectsReverseAsync(last, createdAfter, createdBefore, cancellationToken);
            }

            return getProjectsTask;
        }
        private async Task<bool> GetHasNextPageAsync(
              int? first,
              DateTimeOffset? createdAfter,
              DateTimeOffset? createdBefore,
              CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await this.projectRepository
                    .GetHasNextPageAsync(first, createdAfter, cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (createdBefore.HasValue)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await this.projectRepository
                    .GetHasPreviousPageAsync(last, createdBefore, cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (createdAfter.HasValue)
            {
                return true;
            }

            return false;
        }
    }
}
