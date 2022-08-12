using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace Bee.Abp.Apm.Interceptor;

public static class ActivityInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (ShouldIntercept(context.ImplementationType))
        {
            context.Interceptors.TryAdd<ActivityAbpInterceptor>();
        }
     
    }


    private static bool ShouldIntercept(Type type)
    {
        if (DynamicProxyIgnoreTypes.Contains(type))
        {
            return false;
        }
        
        if (ShouldActivityTypeByDefaultOrNull(type) == true)
        {
            return true;
        }

        if (type.GetMethods().Any(m => m.IsDefined(typeof(ActivityAttribute), true)))
        {
            return true;
        }
        
        return false;

    }
    
    public static bool? ShouldActivityTypeByDefaultOrNull(Type type)
    {
        

        if (type.IsDefined(typeof(ActivityAttribute), true))
        {
            return true;
        }

        if (type.IsDefined(typeof(DisableActivityAttribute), true))
        {
            return false;
        }

        if (typeof(IActivityEnabled).IsAssignableFrom(type))
        {
            return true;
        }

        return null;
    }
}