using AspLearningProject.Filters;
using AspLearningProject.Models;
using AspLearningProject.Models.DataLayer.DataContext;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.Interfacies;
using AspLearningProject.Models.MiddleWare;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AspLearningProject.AutoMapper;
using ILogger = Serilog.ILogger;
using EmailService;
using IEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;
using AspLearningProject.EmailService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using AspLearningProject.Models.DataLayer.Repository.Implementation;
using Microsoft.AspNetCore.Identity;



namespace AspLearningProject
{
    public class Startup
    {
        private readonly IConfiguration configuration;
       
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ConfigurationFeature>(x => configuration.GetSection("ConfigurationFeature").Get<ConfigurationFeature>());
            services.AddSingleton<CacheSettings>(x => configuration.GetSection("CacheSettings").Get<CacheSettings>());
           
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => configuration.Bind("AzureAd", options));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme,
                options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Administrator"));

            });
           
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
         
            services.AddTransient<IMemoryCaheImage, MemoryCaheImage>();
            services.AddScoped<IProductSettings, ProductSettings>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LogAttribute>();
            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddSingleton<EmailConfiguration>(x=> configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>());
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            services.AddMvc(options => options.Filters.Add(typeof(LogAttribute)));
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.AddMvc(options => options.Filters.Add(typeof(LogAttribute)));
            
            services.AddSwaggerGen(options => {
                                        options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API" });
                                    });
            services.AddControllers();
            services.AddAutoMapper(options =>
                options.AddProfile<AutoMapperProfile>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger, IHostApplicationLifetime hostApplicationLifetime, AppDbContext dataContext )
        {

            hostApplicationLifetime.ApplicationStarted.Register(() => 
            logger.Information($"The application {env.ApplicationName} started. Location: {Directory.GetCurrentDirectory()}"));
            
            if (env.IsDevelopment())
            {
                
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "v1"));
            }

            

            app.UseStaticFiles();

            dataContext.Database.Migrate();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ImageCacheMiddleware>();
            app.UseCors("CorsApi");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            app.UseEndpoints(routers => { routers.MapControllerRoute("Default", "{controller=Home}/{action=Index}"); });
           
        }

    }
}
