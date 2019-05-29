using System;
using System.IO;

using Apteka.Api.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Swashbuckle.AspNetCore.Swagger;

namespace Apteka.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AptekaDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/989
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Apteka API", Version = "v1" });
                //c.SchemaFilter<XmlSerializerSchemaFilter>();
                c.IncludeModelAnnotations();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Apteka.Api.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apteka API V1");
            });
        }
    }

    //public class XmlSerializerSchemaFilter : ISchemaFilter
    //{
    //    public void Apply(Schema schema, SchemaFilterContext context)
    //    {
    //        if (schema.Properties == null) return;
    //        //var elemAttr = context.SystemType.GetCustomAttribute<XmlRootAttribute>();
    //        //if (elemAttr != null)
    //        //{
    //            var map = context.SystemType.GetProperties().Select(prop => new
    //            {
    //                PropertyName = prop.Name.ToLower(),
    //                (prop.GetCustomAttribute(typeof(XmlElementAttribute)) as XmlElementAttribute)?.ElementName
    //            }).Where(x => x.ElementName != null).ToDictionary(x => x.PropertyName, x => x.ElementName);

    //            schema.Properties = schema.Properties
    //                .ToDictionary(kv => map.TryGetValue(kv.Key.ToLower(), out var key) ? key : kv.Key,
    //                    kv => kv.Value);
    //        //}
    //    }
    //}
}
