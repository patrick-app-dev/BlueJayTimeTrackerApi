namespace TaskTimeTrackerApi.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ProjectsControllerRoute
    {
        public const string DeleteProject = ControllerName.Cars + nameof(DeleteProject);
        public const string GetProject = ControllerName.Cars + nameof(GetProject);
        public const string GetProjectPage = ControllerName.Cars + nameof(GetProjectPage);
        public const string HeadProject = ControllerName.Cars + nameof(HeadProject);
        public const string HeadProjectPage = ControllerName.Cars + nameof(HeadProjectPage);
        public const string OptionsProject = ControllerName.Cars + nameof(OptionsProject);
        public const string OptionsProjects = ControllerName.Cars + nameof(OptionsProjects);
        public const string PatchProject = ControllerName.Cars + nameof(PatchProject);
        public const string PostProject = ControllerName.Cars + nameof(PostProject);
        public const string PutProject = ControllerName.Cars + nameof(PutProject);
    }
}
