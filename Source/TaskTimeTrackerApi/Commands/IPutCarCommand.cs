namespace TaskTimeTrackerApi.Commands
{
    using TaskTimeTrackerApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
