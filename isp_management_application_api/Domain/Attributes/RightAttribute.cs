using Domain.Enums;
namespace Domain.Attributes;
public class RightAttribute : Attribute
{
    public int AppFeatureId { get; set; }
    public RightAttribute(AppFeaturesEnum appFeaturesEnum)
    {
        AppFeatureId = (int)appFeaturesEnum;
    }
}
