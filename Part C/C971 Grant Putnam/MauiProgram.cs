using C971_Grant_Putnam.Models;
using C971_Grant_Putnam.ViewModels;
using C971_Grant_Putnam.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace C971_Grant_Putnam
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
                
                builder.Services.AddSingleton<DatabaseService>();
                builder.Services.AddTransient<AddEditTermViewModel>();
                builder.Services.AddTransient<AddEditTerm>();
                builder.Services.AddTransient<ViewCoursePage>();
                builder.Services.AddTransient<ViewCourseViewModel>();
                builder.Services.AddTransient<AddEditCourseViewModel>();
                builder.Services.AddTransient<AddEditCourse>();
                //builder.Services.AddTransient<CourseDetails>();
                builder.Services.AddSingleton<MainViewModel>();
                builder.Services.AddSingleton<MainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
