namespace TaskTimeTrackerApi.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskTimeTrackerApi.Controllers;

    public static class ProjectsControllerRoute
    {
        public const string DeleteProject = ProjectsControllerName.Projects + nameof(DeleteProject);
        public const string GetProject = ProjectsControllerName.Projects + nameof(GetProject);
        public const string GetProjectPage = ProjectsControllerName.Projects + nameof(GetProjectPage);
        public const string HeadProject = ProjectsControllerName.Projects + nameof(HeadProject);
        public const string HeadProjectPage = ProjectsControllerName.Projects + nameof(HeadProjectPage);
        public const string OptionsProject = ProjectsControllerName.Projects + nameof(OptionsProject);
        public const string OptionsProjects = ProjectsControllerName.Projects + nameof(OptionsProjects);
        public const string PatchProject = ProjectsControllerName.Projects + nameof(PatchProject);
        public const string PostProject = ProjectsControllerName.Projects + nameof(PostProject);
        public const string PutProject = ProjectsControllerName.Projects + nameof(PutProject);
    }
}
