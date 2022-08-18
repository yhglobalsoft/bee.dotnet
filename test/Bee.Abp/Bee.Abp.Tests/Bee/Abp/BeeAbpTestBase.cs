namespace Bee.Abp;

public abstract class  BeeAbpTestBase : AbpIntegratedTest<BeeAbpTestBaseModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}