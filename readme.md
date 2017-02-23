**Screencast of running application**
https://www.screencast.com/t/YNSuNZYlHy

### What's new in ASP.NET CORE API

* same base Controller for MVC and API which means ActionFilters are now can be used for both.

using Microsoft.AspNetCore.Mvc;
public class CustomerController : Controller




* Different attribute routing convention, you don't require to write controller name, instead it will automatically translated controller name. for example below it will be translated into http://sampleapp/api/customer    
```
[Route("api/[controller]")]
public class CustomerController : Controller
```

* **appsettings.json** 
Essentially it is replacement of web.config. Like we can add our database connection string in this appsetting.json file.

* **Startup.cs**
It configures the pipeline that handles the requests. 
- The Configure method is used to specify how the ASP.NET application will respond to HTTP requests. Also we configure middleware in this method.
```
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
	loggerFactory.AddConsole(Configuration.GetSection("Logging"));
	loggerFactory.AddDebug();

	//system will know MVC framework need to use to handle user requests
	app.UseMvc();

	//In the Configure method, insert middleware to expose the generated Swagger as JSON endpoint(s)
	app.UseSwagger();

	//Optionally insert the swagger-ui middleware if you want to expose interactive documentation, specifying the Swagger JSON endpoint(s) to power it from.
	app.UseSwaggerUi(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "sample aspnet core api V1");
	});
}
```
* The ConfigureServices method is optional, but must be called before Configure, as some features will be added before they can be wired up to the request pipeline. 
```
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{

	//register the dbcontext
	services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase());

	// Add framework services.
	services.AddMvc();

	services.AddSingleton<ICustomerModule, CustomerModule>();

	//In the ConfigureServices method of Startup.cs, register the Swagger generator, defining one or more Swagger documents.
	services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new Info { Title = "sample aspnet core api", Version = "v1" });
	});
}
```
* **Program.cs**
ASP.NET CORE is a console application. We created the webhost and other necessary configuration in program.cs to run the application. Also it is the main entry point of our application.
```
 public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
```
