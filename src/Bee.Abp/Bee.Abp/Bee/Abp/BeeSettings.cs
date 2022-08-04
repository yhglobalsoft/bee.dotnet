namespace Bee.Abp;

public static class BeeSettings
{
    /// <summary>
    /// 扩展属性Key
    /// </summary>
    public static class PropertyKey
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public const string GroupName = "Setting.Group";

        /// <summary>
        /// 前端控件类型
        /// </summary>
        public const string ControlType = "Type";

        /// <summary>
        /// 描述链接
        /// </summary>
        public const string DescriptionUrl = "DescriptionUrl";

    }

    /// <summary>
    /// 扩展属性Value
    /// </summary>
    public static class PropertyValue
    {
        /// <summary>
        /// 特性开关
        /// </summary>
        public const string FeatureSwitch = "Setting.FeatureSwitch";

        /// <summary>
        /// 前端控件类型
        /// </summary>
        public static class ControlType
        {
            /// <summary>
            /// 文本框
            /// </summary>
            public const string TextBox = "Text";

            /// <summary>
            /// 勾选框
            /// </summary>
            public const string CheckBox = "CheckBox";

            /// <summary>
            /// 数字
            /// </summary>
            public const string Number = "Number";
        }

    }

}