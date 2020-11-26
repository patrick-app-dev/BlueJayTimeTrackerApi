namespace TaskTimeTrackerApi.Commands.Projects
{
    using Boxed.AspNetCore;
    using TaskTimeTrackerApi.ViewModels;

    public interface IPutProjectCommand : IAsyncCommand<int, SaveProject>
    {
    }
}
