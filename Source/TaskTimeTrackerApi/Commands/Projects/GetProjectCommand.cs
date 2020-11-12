namespace TaskTimeTrackerApi.Commands.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using TaskTimeTrackerApi.Mappers;
    using TaskTimeTrackerApi.ViewModels;

    public class GetProjectCommand : IGetProjectCommand
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper<Core.Models.Project, Project> projectMapper;
        private readonly IActionContextAccessor actionContextAccessor;

        public GetProjectCommand(IProjectRepository projectRepository,
            IMapper<Core.Models.Project, Project> projectMapper,
            IActionContextAccessor actionContextAccessor)
        {
            this.projectRepository = projectRepository;
            this.projectMapper = projectMapper;
            this.actionContextAccessor = actionContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var project = await this.projectRepository.GetAsync(projectId, cancellationToken).ConfigureAwait(false);
            if (project == null)
            {
                return new NotFoundResult();
            }
            var viewModel = this.projectMapper.Map(project);
            return new OkObjectResult(project);
        }
    }
}
