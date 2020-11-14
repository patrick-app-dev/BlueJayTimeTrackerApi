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
    using TaskTimeTrackerApi.Constants;
    using TaskTimeTrackerApi.ViewModels;

    public class PostProjectCommand : IPostProjectCommand
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper<SaveProject, Core.Models.Project> saveProjectToProjectMapper;
        private readonly IMapper<Core.Models.Project, Project> projectToProjectVmMapper;

        public PostProjectCommand(IProjectRepository projectRepository, IMapper<SaveProject, Core.Models.Project> saveProjectToProject, IMapper<Core.Models.Project, Project> projectToProjectVmMapper)
        {
            this.projectRepository = projectRepository;
            this.saveProjectToProjectMapper = saveProjectToProject;
            this.projectToProjectVmMapper = projectToProjectVmMapper;
        }
        public async Task<IActionResult> ExecuteAsync(SaveProject saveProject, CancellationToken cancellationToken = default)
        {
            if (saveProject is null)
            {
                throw new ArgumentNullException(nameof(saveProject));
            }
            var project = this.saveProjectToProjectMapper.Map(saveProject);
            project = await this.projectRepository.AddAsync(project, cancellationToken).ConfigureAwait(false);
            var projectVm = this.projectToProjectVmMapper.Map(project);
            return new CreatedAtRouteResult(
                ProjectsControllerRoute.GetProject,
                new { projectId = projectVm.ProjectId },
                projectVm);

        }
    }
}
