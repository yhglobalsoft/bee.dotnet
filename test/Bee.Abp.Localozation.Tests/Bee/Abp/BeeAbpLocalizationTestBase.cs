using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Testing;

namespace Bee.Abp
{

    public abstract class BeeAbpLocalizationTestBase : AbpIntegratedTest<BeeAbpLocalizationTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
