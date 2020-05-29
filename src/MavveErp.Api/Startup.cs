using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MavveErp.Api.Domain.Handlers;
using MavveErp.Api.Domain.Repositories;
using MavveErp.Api.Infra.Contexts;
using MavveErp.Api.Infra.Repositories;
using MavveErp.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MavveErp.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<MavveErpContext>(options => options.UseNpgsql(Configuration.GetConnectionString("MavveErpContext")));

      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<UserHandler, UserHandler>();

      services.AddCors();
      services.AddControllers();

      services.AddSwaggerGen(c =>
        {
          c.SwaggerDoc(
              "v1",
              new OpenApiInfo
              {
                Title = "Mavve ERP API",
                Version = "v1",
                Description = "Especificações da API do Mavve ERP.",
                Contact = new OpenApiContact
                {
                  Email = "smuriano@gmail.com",
                  Name = "Sidney Muriano",
                  Url = new Uri("https://github.com/smuriano"),
                },
                License = new OpenApiLicense
                {
                  Name = "MIT License",
                  Url = new Uri("https://opensource.org/licenses/MIT"),
                }
              });

          c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
          {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Inserir o Bearer token para acessar está API",
          });

          c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
            {
             new OpenApiSecurityScheme
             {
                Reference = new OpenApiReference
                {
                 Type = ReferenceType.SecurityScheme,
                 Id = "bearer",
                }
             },
             new List<string>()
            },
          });
        });

      var key = Encoding.ASCII.GetBytes(Settings.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      }

      app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mavve ERP API V1");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
