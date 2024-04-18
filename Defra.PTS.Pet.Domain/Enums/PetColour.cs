using System.ComponentModel;

namespace Defra.PTS.Pet.Domain.Enums;

public enum PetColour
{
    // Dog
    [Description("Black")]
    Black = 1,

    [Description("Brown, tan or chocolate")]
    BrownTanOrChocolate = 2,

    [Description("Grey, silver or blue")]
    GreySilverOrBlue = 3,

    [Description("Red")]
    Red = 4,

    [Description("White, cream or sand")]
    WhiteCreamOrSand = 5,

    [Description("Gold or yellow")]
    GoldOrYellow = 6,

    [Description("Brindle (tiger striped)")]
    BrindleTigerStriped = 7,

    [Description("Merle (white flecks)")]
    MerleWhiteFlecks = 8,


    // Cat
    [Description("Black")]
    CatBlack = 9,

    [Description("White")]
    CatWhite = 10,

    [Description("Tabby")]
    CatTabby = 11,

    [Description("Tortoiseshell")]
    CatTortoiseshell = 12,

    [Description("Ginger")]
    CatGinger = 13,

    [Description("Calico")]
    CatCalico = 14,

    [Description("Grey or silver")]
    CatGreyOrSilver = 15,

    [Description("Tuxedo")]
    CatTuxedo = 16,

    // Ferret
    [Description("Sable")]
    FerretSable = 17,

    [Description("White (albino)")]
    FerretWhiteAlbino = 18,

    [Description("Black")]
    FerretBlack = 19,

    [Description("Black sable")]
    FerretBlackSable = 20,

    [Description("Chocolate")]
    FerretChocolate = 21,

    [Description("Cinnamon")]
    FerretCinnamon = 22,

    [Description("Champagne")]
    FerretChampagne = 23,

    [Description("Silver")]
    FerretSilver = 24,

    // Shared
    [Description("Other")]
    Other = 25
}
