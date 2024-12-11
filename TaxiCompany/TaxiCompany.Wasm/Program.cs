using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxiCompany.Wasm.WebApi;
using TaxiCompany.Wasm.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ITaxiCompanyApiWrapper, TaxiCompanyApiWrapper>();
builder.Services.AddBlazorise(options => { options.Immediate = true; })
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();





































//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//	.AddInteractiveServerComponents();

//builder.Services.AddScoped(sp =>
//	new HttpClient
//	{
//		BaseAddress = new Uri(builder.Configuration["BasePath"]!)
//	});

//builder.Services.AddHttpClient();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddMudServices();

//builder.Services
//	.AddBlazorise(options =>
//	{
//		options.Immediate = true;
//	})
//	.AddBootstrap5Providers()
//	.AddFontAwesomeIcons();

//var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//	app.UseExceptionHandler("/Error", createScopeForErrors: true);
//	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//	app.UseHsts();
//}

//app.UseHttpsRedirection();

//app.UseStaticFiles();
//app.UseAntiforgery();

//app.MapRazorComponents<App>()
//	.AddInteractiveServerRenderMode();

//app.Run();
