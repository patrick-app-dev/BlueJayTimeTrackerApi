namespace TaskTimeTrackerApi.Commands
{
    using TaskTimeTrackerApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetCarPageCommand : IAsyncCommand<PageOptions>
    {
    }
}
