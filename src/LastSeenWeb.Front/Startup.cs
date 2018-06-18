using LastSeenWeb.Core;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Core.Services.Implementation;
using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

			services.AddMvc();

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

			//var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
			//loggerFactory.AddAzureWebAppDiagnostics();
		}
	}
}
