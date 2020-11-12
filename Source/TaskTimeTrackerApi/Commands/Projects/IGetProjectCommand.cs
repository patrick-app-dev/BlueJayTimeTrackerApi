namespace TaskTimeTrackerApi.Commands.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;

    public interface IGetProjectCommand : IAsyncCommand<int>
    {
    }
}
