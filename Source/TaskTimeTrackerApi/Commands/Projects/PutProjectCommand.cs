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
    using TaskTimeTrackerApi.ViewModels;

    public class PutProjectCommand : IPutProjectCommand
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper<Core.Models.Project, Project> projectToProjectMapper;
        private readonly IMapper<SaveProject, Core.Models.Project> saveProjectToProjectMapper;

        public PutProjectCommand(IProjectRepository projectRepository,
            IMapper<Core.Models.Project, Project> projectToProjectMapper,
            IMapper<SaveProject, Core.Models.Project> saveProjectToProjectMapper
            )
        {
            this.projectRepository = projectRepository;
            this.projectToProjectMapper = projectToProjectMapper;
            this.saveProjectToProjectMapper = saveProjectToProjectMapper;
        }
        public async Task<IActionResult> ExecuteAsync(int projectId, SaveProject saveProject, CancellationToken cancellationToken = default)
        {
            var project = await this.projectRepository.GetAsync(projectId, cancellationToken).ConfigureAwait(false);
            if (project is null)
            {
                return new NotFoundResult();
            }
            this.saveProjectToProjectMapper.Map(saveProject, project);
            project = await this.projectRepository.UpdateAsync(project, cancellationToken).ConfigureAwait(false);
            var projectViewModel = this.projectToProjectMapper.Map(project);

            return new OkObjectResult(projectViewModel);
        }
    }
}
