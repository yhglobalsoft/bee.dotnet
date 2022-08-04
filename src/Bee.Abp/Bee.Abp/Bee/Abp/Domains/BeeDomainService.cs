using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Localization;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Bee.Abp.Domains;

public abstract class BeeDomainService : DomainService
{
    protected Type ObjectMapperContext { get; set; }

    /// <summary>
    /// 工作单元管理器
    /// </summary>
    protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

    /// <summary>
    /// 分布式事件总线
    /// </summary>
    protected IDistributedEventBus DistributedEventBus => LazyServiceProvider.LazyGetRequiredService<IDistributedEventBus>();

    /// <summary>
    /// 本地事件总线
    /// </summary>
    protected ILocalEventBus LocalEventBus => LazyServiceProvider.LazyGetRequiredService<ILocalEventBus>();
    
    /// <summary>
    /// 对象映射器
    /// </summary>
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

    #region Localization
    private IStringLocalizer _localizer;

    private Type _localizationResource = typeof(DefaultResource);

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    protected IStringLocalizer L
    {
        get
        {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }

    protected Type LocalizationResource
    {
        get
        {
            return _localizationResource;
        }
        set
        {
            _localizationResource = value;
            _localizer = null;
        }
    }

    protected virtual IStringLocalizer CreateLocalizer()
    {
        if (LocalizationResource != null)
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }

        return StringLocalizerFactory.CreateDefaultOrNull() ??
        throw new AbpException(message: L["Bee.Abp:0010"]);
    }
    #endregion

}