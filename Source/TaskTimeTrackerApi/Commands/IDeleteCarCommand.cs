namespace TaskTimeTrackerApi.Commands
{
    using Boxed.AspNetCore;

    public interface IDeleteCarCommand : IAsyncCommand<int>
    {
    }
}
