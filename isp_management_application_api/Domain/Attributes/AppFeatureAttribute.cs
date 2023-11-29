namespace Domain.Attributes;
public class AppFeatureAttribute : Attribute
{
    public string FeatureName { get; set; }
    public string Description { get; set; }
    public AppFeatureAttribute(string featureName)
    {
        FeatureName = featureName;
    }
}
