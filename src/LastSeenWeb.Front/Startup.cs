using AutoMapper;
using LastSeenWeb.Core;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Core.Services.Implementation;
using LastSeenWeb.Data.Identity.Models;
using LastSeenWeb.Data.Services;
using LastSeenWeb.Data.Services.Implementation;
using LastSeenWeb.Front.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LastSeenWeb.Front
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			Environment = hostingEnvironment;
		}

		public IConfiguration Configuration { get; }
		public IHostingEnvironment Environment { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ApplicationSettings>();

			services.AddDbContext<UsersDbContext>();
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<UsersDbContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Auth/Login";
			});

			services.AddSingleton<IEmailSender, EmailSender>();
			services.AddSingleton<IWebClientService, WebClientService>();
			services.AddSingleton<IAzureKicker, AzureKicker>();
			services.AddSingleton<ILastSeenService, LastSeenService>();
			services.AddSingleton<ILastSeenRepository, LastSeenRepository>();
			services.AddSingleton(CreateMapperConfiguration());
			services.AddTransient(e => e.GetService<MapperConfiguration>().CreateMapper());

			services
				.AddMvc()
				.AddRazorOptions(e => e.ViewLocationFormats.Add("/Pages/{0}.cshtml"));

			services.Configure<MvcOptions>(options =>
			{
				if (Environment.IsProduction())
				{
					options.Filters.Add(new RequireHttpsAttribute());
				}
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc();

			var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
			loggerFactory.AddAzureWebAppDiagnostics();

			serviceProvider.GetService<IAzureKicker>().Start();
		}

		private static MapperConfiguration CreateMapperConfiguration()
		{
			return new MapperConfiguration(e =>
			{
				e.AddProfile<AppMappingProfile>();
			});
		}
	}
}
