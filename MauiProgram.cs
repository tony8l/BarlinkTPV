using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace BarlinkTPV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjGyl/VkV+XU9AclRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3hTc0VkWXledXdWQGVYVE91XA==");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>

                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inter.ttf", "Inter");
                    fonts.AddFont("Montserrat.ttf", "Montserrat");
                });
            builder.ConfigureSyncfusionCore();
            builder.Services.AddSingleton<GlobalData>();
            builder.Services.AddSingleton<ApiService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
