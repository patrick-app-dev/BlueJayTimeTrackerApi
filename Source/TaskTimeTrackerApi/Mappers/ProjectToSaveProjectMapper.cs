namespace TaskTimeTrackerApi.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using TaskTimeTrackerApi.Services;
    using TaskTimeTrackerApi.ViewModels;

    public class ProjectToSaveProjectMapper : IMapper<Core.Models.Project, SaveProject>, IMapper<SaveProject, Core.Models.Project>
    {
        private readonly IClockService clockService;

        public ProjectToSaveProjectMapper(IClockService clockService) => this.clockService = clockService;
        public void Map(Core.Models.Project source, SaveProject destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Description = source.Description;
            destination.Title = source.Title;
        }

        public void Map(SaveProject source, Core.Models.Project destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = this.clockService.UtcNow;
            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }

            destination.Description = source.Description;
            destination.Title = source.Title;
            destination.Modified = now;
        }
    }
}
