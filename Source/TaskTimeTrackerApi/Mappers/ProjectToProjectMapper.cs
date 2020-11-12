namespace TaskTimeTrackerApi.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using TaskTimeTrackerApi.Constants;
    using TaskTimeTrackerApi.ViewModels;

    public class ProjectToProjectMapper : IMapper<Core.Models.Project, Project>
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProjectToProjectMapper(IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
            this.httpContextAccessor = httpContextAccessor;
        }
        public void Map(Core.Models.Project source, Project destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            destination.Description = source.Description;
            destination.ProjectId = source.ProjectId;
            destination.Title = source.Title;
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                ProjectsControllerRoute.GetProject,
                new { source.ProjectId }));
        }
    }
}
