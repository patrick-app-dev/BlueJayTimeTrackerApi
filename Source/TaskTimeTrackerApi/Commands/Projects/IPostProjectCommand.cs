namespace TaskTimeTrackerApi.Commands.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;
    using TaskTimeTrackerApi.ViewModels;

    public interface IPostProjectCommand : IAsyncCommand<SaveProject>
    {
    }
}
