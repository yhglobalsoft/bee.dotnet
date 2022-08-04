using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Bee.Abp.Localization;

namespace Bee.Abp.Dto
{
    /// <summary>
    /// 分页查询时使用的Dto类型
    /// </summary>
    public class BeeQueryPagingInput : IValidatableObject
    {
        public const int MaxPageSize = 100000;
        
        /// <summary>
        /// 当前页面.默认从1开始
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页多少条.每页显示多少记录
        /// </summary>
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// 跳过多少条
        /// </summary>
        public int SkipCount => (PageIndex - 1) * PageSize;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }
        
        /// <summary>
        /// 排序规则 desc/asc
        /// </summary>
        public string SortOrder { get; set; }
        
        public BeeQueryPagingInput()
        {
        }

        /// <summary>
        /// 实例化 <see cref="BeeQueryPagingInput"/> 对象
        /// </summary>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页多少条</param>
        public BeeQueryPagingInput(int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localization = validationContext.GetRequiredService<IStringLocalizer<BeeAbpLocalizationResource>>();
            if (PageIndex < 1)
            {
                yield return new ValidationResult(
                    localization["Bee.Abp:0069"],
                    new[] { "PageIndex"}
                );
            }
            
            if (PageSize > MaxPageSize)
            {
                yield return new ValidationResult(
                    localization["Bee.Abp:0070"],
                    new[] { "PageSize"}
                );
            }
        }
    }
}