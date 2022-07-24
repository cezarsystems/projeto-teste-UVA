using CadUsuarioUVA.Business.Implementations;
using CadUsuarioUVA.Business.Interfaces;
using CadUsuarioUVA.DataAccess.Implementations;
using CadUsuarioUVA.DataAccess.Interfaces;
using CadUsuarioUVA.Repository.Implementations;
using CadUsuarioUVA.Repository.Interfaces;
using Unity;
using Unity.Lifetime;

namespace CadUsuarioUVA.IoC
{
    public class RegisterDependency
    {
        public static UnityContainer Register()
        {
            UnityContainer container = new UnityContainer();

            // Data Access
            container.RegisterType<IDbBasicOperations, DbBasicOperations>(new HierarchicalLifetimeManager());

            // Business
            container.RegisterType<IUsuarioBusiness, UsuarioBusiness>(new HierarchicalLifetimeManager());

            // Repository
            container.RegisterType<IUsuarioRepository, UsuarioRepository>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}