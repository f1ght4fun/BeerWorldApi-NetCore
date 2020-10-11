using System.ComponentModel;

namespace BeerWorld.Enums
{
    public enum BeerType
    {
        [Description("ale")]
        Ale,

        [Description("dunkel")]
        Dunkel,

        [Description("lager")]
        Lager,

        [Description("stout")]
        Stout,

        [Description("weiss")]
        Weiss,

        [Description("wit")]
        Wit,
    }
}