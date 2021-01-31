public static class Constants
{
    // Tile types
    public static string TILE_TYPE_CAMP = "CAMP";
    public static string TILE_TYPE_GROUND = "GROUND";
    public static string TILE_TYPE_OBSTACLE = "OBSTACLE";
    public static string TILE_TYPE_NPC = "NPC";
    public static string TILE_TYPE_PATH = "PATH";

    // UI properties
    public static int ICON_SEPARATION = 40;

    // Camp positions
    public static Tile[] CAMP_POSITIONS = {
        new Tile(4, 1, Constants.TILE_TYPE_CAMP),
        new Tile(5, 1, Constants.TILE_TYPE_CAMP),
        new Tile(4, 2, Constants.TILE_TYPE_CAMP),
        new Tile(5, 2, Constants.TILE_TYPE_CAMP),
    };
}
