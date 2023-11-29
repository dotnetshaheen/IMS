using Domain.Attributes;

namespace Domain.Enums;

public enum RightsEnum
{
    [Right(AppFeaturesEnum.UserFeature)] User_View = 101_001,
    [Right(AppFeaturesEnum.UserFeature)] User_Create = 101_002
}
