// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskTimeTrackerApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using TaskTimeTrackerApi.Commands.Projects;
    using TaskTimeTrackerApi.Constants;
    using TaskTimeTrackerApi.ViewModels;

    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionName.V1)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CA1062 // Validate arguments of public methods

    public class ProjectsController : ControllerBase
    {

        [HttpPost("", Name = ProjectsControllerRoute.PostProject)]
        [SwaggerResponse(StatusCodes.Status201Created, "The project was created.", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The project is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]

        public Task<IActionResult> PostAsync(
            [FromServices] IPostProjectCommand command,
            [FromBody] SaveProject project,
            CancellationToken cancellationToken) => command.ExecuteAsync(project, cancellationToken);

        /// <summary>
        /// Deletes the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="projectId">The cars unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the project was deleted or a 404 Not Found if a project with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{projectId}", Name = ProjectsControllerRoute.DeleteProject)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The project with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A project with the specified unique identifier was not found.", typeof(ProblemDetails))]
        public Task<IActionResult> DeleteAsync(
            [FromServices] IDeleteProjectCommand command,
            int projectId,
            CancellationToken cancellationToken) => command.ExecuteAsync(projectId, cancellationToken);

        /// <summary>
        /// Gets the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="projectId">The cars unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{projectId}", Name = ProjectsControllerRoute.GetProject)]
        [HttpHead("{projectId}", Name = ProjectsControllerRoute.HeadProject)]
        [SwaggerResponse(StatusCodes.Status200OK, "The prject with the specified unique identifier.", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The project has not changed since the date given in the If-Modified-Since HTTP header.", typeof(void))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A project with the specified unique identifier could not be found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        public Task<IActionResult> GetAsync(
            [FromServices] IGetProjectCommand command,
            int projectId,
            CancellationToken cancellationToken) => command.ExecuteAsync(projectId, cancellationToken);

        /// <summary>
        /// Gets a collection of project using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = ProjectsControllerRoute.GetProjectPage)]
        [HttpHead("", Name = ProjectsControllerRoute.HeadProjectPage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of projects for the specified page.", typeof(Connection<Project>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]

        public Task<IActionResult> GetPageAsync(
         [FromServices] IGetProjectPageCommand command,
         [FromQuery] PageOptions pageOptions,
         CancellationToken cancellationToken) => command.ExecuteAsync(pageOptions, cancellationToken);

        /// <summary>
        /// Updates an existing project with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="project">The project to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified unique identifier was not found.</returns>
        [HttpPut("{projectId}", Name = ProjectsControllerRoute.PutProject)]
        [SwaggerResponse(StatusCodes.Status200OK, "The project was updated.", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The project is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A project with the specified unique identifier could not be found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
        public Task<IActionResult> PutAsync(
            [FromServices] IPutProjectCommand command,
            int projectId,
            [FromBody] SaveProject project,
            CancellationToken cancellationToken) => command.ExecuteAsync(projectId, project, cancellationToken);


    }

}
