namespace TaskTimeTrackerApi.Commands
{
    using TaskTimeTrackerApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
