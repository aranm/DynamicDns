using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Dynamic.Dns.UserInterface.Infrastructure.Windsor.Attributes;

namespace Dynamic.Dns.UserInterface.Infrastructure.Windsor.Injection
{
    public class InjectionPropertyComponentModelHelper : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            foreach (var property in model.Properties)
            {
                if (property.Property.GetCustomAttributes(inherit: true).Any(x => x is InjectAttribute))
                {
                    property.Dependency.IsOptional = false;
                }
            }
        }
    }
}
