namespace Bee.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class GenerateDtoAttribute : Attribute
{
    public GenerateDtoAttribute()
    {
    }
}