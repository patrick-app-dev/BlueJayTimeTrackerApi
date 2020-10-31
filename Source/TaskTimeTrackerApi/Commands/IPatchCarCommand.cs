namespace TaskTimeTrackerApi.Commands
{
    using TaskTimeTrackerApi.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;

    public interface IPatchCarCommand : IAsyncCommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
