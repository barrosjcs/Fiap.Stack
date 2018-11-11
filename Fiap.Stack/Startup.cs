﻿using System;
using System.Text;
using Fiap.Stack.BLL;
using Fiap.Stack.BLL.Interfaces;
using Fiap.Stack.DAL;
using Fiap.Stack.DAL.Interfaces;
using Fiap.Stack.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fiap.Stack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var tokenSettings = new TokenSettings();
            new ConfigureFromConfigurationOptions<TokenSettings>(
                    Configuration.GetSection("TokenSettings")).Configure(tokenSettings);
            services.AddSingleton(tokenSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = tokenSettings.Audience,
                    ValidIssuer = tokenSettings.Issuer,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });


            services.AddTransient<IPerguntaDAL, PerguntaDAL>();
            services.AddTransient<IPerguntaBLL, PerguntaBLL>();
            services.AddTransient<IRespostaDAL, RespostaDAL>();
            services.AddTransient<IRespostaBLL, RespostaBLL>();
            services.AddTransient<IUsuarioDAL, UsuarioDAL>();
            services.AddTransient<IUsuarioBLL, UsuarioBLL>();
            services.AddTransient<ITagDAL, TagDAL>();
            services.AddTransient<ITagBLL, TagBLL>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
