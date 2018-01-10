using Microsoft.Practices.Unity;
using MobileSchoolRegisterAppApi.Controllers;
using Repository.IRepo;
using Repository.Models.Contexts;
using Repository.Repo;
using System.Web.Http;
using Unity.WebApi;

namespace MobileSchoolRegisterAppApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<ICourseRepo, CourseRepo>();
            container.RegisterType<ITeacherRepo, TeacherRepo>();
            container.RegisterType<IStudentRepo, StudentRepo>();
            container.RegisterType<ISchoolRegisterContext, SchoolRegisterContext>();
            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}