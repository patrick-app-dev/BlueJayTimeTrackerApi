namespace TaskTimeTrackerApi.Commands.Projects
{
    using Boxed.AspNetCore;

    public interface IDeleteProjectCommand : IAsyncCommand<int>
    {
    }
}
