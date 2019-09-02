using System;
using AutoMapper;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using LastSeenWeb.AngularFront.MappingProfiles;
using LastSeenWeb.AngularFront.Services;
using LastSeenWeb.AngularFront.Services.Implementation;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Core.Services.Implementation;
using LastSeenWeb.Data.Identity;
using LastSeenWeb.Data.Identity.Models;
using LastSeenWeb.Data.Services;
using LastSeenWeb.Data.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace LastSeenWeb.AngularFront
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IHostingEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			Environment = hostingEnvironment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpClientService, HttpClientService>();
			services.AddSingleton<IAzureKicker, AzureKicker>();
			services.AddSingleton<IWebClientService, WebClientService>();
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<ILastSeenService, LastSeenService>();
			services.AddTransient<ILastSeenRepository, LastSeenRepository>();

			services.AddSingleton(new MapperConfiguration(e => e.AddProfile<AppMappingProfile>()));
			services.AddTransient(e => e.GetService<MapperConfiguration>().CreateMapper());

			services.AddDbContext<UsersDbContext>();
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<UsersDbContext>()
				.AddDefaultTokenProviders();

			services.AddSingleton<IMongoClient>(e =>
			{
				var configuration = e.GetService<IConfiguration>();
				return new MongoClient(configuration.GetConnectionString("LastSeenWebDb"));
			});

			services.AddIdentityServer()
				.AddSigningCredential(new SigningCredentials(
					new JsonWebKey(Configuration["IdentityJwk"]),
					SecurityAlgorithms.RsaSha256Signature))
				.AddOperationalStore(options =>
					options.ConfigureDbContext = builder =>
						builder.UseMySql(Configuration.GetConnectionString("UsersDbContext")))
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients(Configuration["ClientSecret"]))
				.AddAspNetIdentity<ApplicationUser>();
			services.AddTransient<IProfileService, IdentityClaimsProfileService>();
			services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.Authority = Configuration["Authority"];
				options.Audience = "lastseenapi";
#if !DEBUG
				options.RequireHttpsMetadata = true;
#else
				options.RequireHttpsMetadata = false;
#endif
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("LastSeenApiAccess", policy => policy.RequireClaim("LastSeenApiAccess", "true"));
			});

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

			services.AddLogging(e =>
			{
				e.AddDebug();
				e.AddAzureWebAppDiagnostics();
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env,
			ILoggerFactory loggerFactory, IAzureKicker azureKicker)
		{
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

#if !DEBUG
			app.UseHttpsRedirection();
#endif
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer("start");
				}
			});

			azureKicker.Start();
		}
	}
}
