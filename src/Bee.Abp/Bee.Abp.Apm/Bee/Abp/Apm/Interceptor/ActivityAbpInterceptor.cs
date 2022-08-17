using System.Reflection;
using Elastic.Apm.Api;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace Bee.Abp.Apm.Interceptor;

public class ActivityAbpInterceptor : AbpInterceptor, ITransientDependency
{
    public override async Task InterceptAsync(IAbpMethodInvocation invocation)
    {
        if (ShouldIntercept(invocation.Method))
        {
            var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
           
            ISpan span = null;
            if (transaction != null)
            {
                span = transaction.StartSpan(invocation.Method.Name, nameof(ActivityAttribute));
            }

            await invocation.ProceedAsync();
            span?.End();
        }
        else
        {
            await invocation.ProceedAsync();
        }
    }

    private bool ShouldIntercept(MethodInfo methodInfo, bool defaultValue = false)
    {
        if (methodInfo == null)
        {
            return false;
        }

        if (methodInfo.IsDefined(typeof(DisableActivityAttribute), true))
        {
            return false;
        }

        if (methodInfo.IsDefined(typeof(ActivityAttribute), true))
        {
            Console.WriteLine($"ActivityAttribute:{methodInfo.Name}");
            return true;
        }

        var classType = methodInfo.DeclaringType;
        if (classType != null)
        {
            var shouldActivity = ActivityInterceptorRegistrar.ShouldActivityTypeByDefaultOrNull(classType);
            if (shouldActivity != null)
            {
                if (shouldActivity.Value)
                {
                    Console.WriteLine($"shouldActivity:{methodInfo.Name}");
                }

                return shouldActivity.Value;
            }
        }

        return defaultValue;
    }
}