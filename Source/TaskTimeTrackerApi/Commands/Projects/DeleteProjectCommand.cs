namespace TaskTimeTrackerApi.Commands.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteProjectCommand : IDeleteProjectCommand
    {
        private readonly IProjectRepository projectRepository;
        public DeleteProjectCommand(IProjectRepository projectRepository) => this.projectRepository = projectRepository;
        public async Task<IActionResult> ExecuteAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var project = await this.projectRepository.GetAsync(projectId, cancellationToken).ConfigureAwait(false);
            if (project is null)
            {
                return new NotFoundResult();
            }

            await this.projectRepository.RemoveAsync(project, cancellationToken).ConfigureAwait(false);

            return new NoContentResult();
        }
    }
}
