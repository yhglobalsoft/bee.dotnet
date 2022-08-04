namespace Bee.Abp;

/// <summary>
/// 异常编码常量
/// </summary>
public static class BeeAbpErrorCodes
{
    // 验证 01XX
    public const string Invalid = BeeAbpConsts.NameSpace + ":0101";
    public const string Null = BeeAbpConsts.NameSpace + ":0102";
    public const string NullOrEmpty = BeeAbpConsts.NameSpace + ":0103";
    public const string NullOrWhiteSpace = BeeAbpConsts.NameSpace + ":0104";
    public const string LengthGreater = BeeAbpConsts.NameSpace + ":0105";
    public const string LengthLess = BeeAbpConsts.NameSpace + ":0106";
    public const string CollectionNullOrEmpty = BeeAbpConsts.NameSpace + ":0107";
        
    public const string OutOfRange = BeeAbpConsts.NameSpace + ":0108";
    public const string Greater = BeeAbpConsts.NameSpace + ":0109";
    public const string GreaterEqual = BeeAbpConsts.NameSpace + ":0110";
    public const string Less = BeeAbpConsts.NameSpace + ":0111";
    public const string LessEqual = BeeAbpConsts.NameSpace + ":0112";
    public const string EmptyGuid = BeeAbpConsts.NameSpace + ":0113";
        
    // IO 验证 03XX
    public const string DirectoryNotFound = BeeAbpConsts.NameSpace + ":0301";
    public const string FileNotFound = BeeAbpConsts.NameSpace + ":0302";
        
    // 常见业务验证 10XX
    public const string DuplicateCode = BeeAbpConsts.NameSpace + ":1001";
    public const string CacheKeyInvalid = BeeAbpConsts.NameSpace + ":1002";
    public const string EntityNotFound = BeeAbpConsts.NameSpace + ":1003";
        
}