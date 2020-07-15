using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lista_asistencias_merino_espinoza.Startup))]
namespace lista_asistencias_merino_espinoza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
