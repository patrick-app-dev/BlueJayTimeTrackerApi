namespace TaskTimeTrackerApi
{
    using TaskTimeTrackerApi.Commands;
    using TaskTimeTrackerApi.Mappers;
    using TaskTimeTrackerApi.Repositories;
    using TaskTimeTrackerApi.Services;
    using TaskTimeTrackerApi.ViewModels;
    using Boxed.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using TaskTimeTrackerApi.Commands.Projects;
    using Core.Interfaces;
    using EFDataAccess.MockData;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IDeleteCarCommand, DeleteCarCommand>()
                .AddSingleton<IGetCarCommand, GetCarCommand>()
                .AddSingleton<IGetCarPageCommand, GetCarPageCommand>()
                .AddSingleton<IPatchCarCommand, PatchCarCommand>()
                .AddSingleton<IPostCarCommand, PostCarCommand>()
                .AddSingleton<IPutCarCommand, PutCarCommand>()
                .AddSingleton<IDeleteProjectCommand, DeleteProjectCommand>()
                .AddSingleton<IGetProjectCommand, GetProjectCommand>()
                .AddSingleton<IGetProjectPageCommand, GetProjectPageCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Models.Car, Car>, CarToCarMapper>()
                .AddSingleton<IMapper<Models.Car, SaveCar>, CarToSaveCarMapper>()
                .AddSingleton<IMapper<SaveCar, Models.Car>, CarToSaveCarMapper>()
                .AddSingleton<IMapper<Core.Models.Project, Project>, ProjectToProjectMapper>()
                .AddSingleton<IMapper<Core.Models.Project, SaveProject>, ProjectToSaveProjectMapper>()
                .AddSingleton<IMapper<SaveProject, Core.Models.Project>, ProjectToSaveProjectMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ICarRepository, CarRepository>()
            .AddSingleton<IProjectRepository, MockProjectRepository>();


        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
