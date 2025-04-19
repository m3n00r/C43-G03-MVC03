using Demo.BLL.profiles;
using Demo.BLL.Services.AttachementServices;
using Demo.BLL.Services.classes;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Data.Contexts;
using Demo.DLL.Models.IdentityModel;
using Demo.DLL.Repositories.Classes;
using Demo.DLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {

            #region C43-G03-MVC03



            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());


            });

            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //session02

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            //session03
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachementServices, AttachementServices>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();  
            #endregion
        }
    }
}
