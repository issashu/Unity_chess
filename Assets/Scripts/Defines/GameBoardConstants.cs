namespace Defines
{
    public struct GameBoardConstants
    {
        public const int BOARD_HEIGHT = 8;
        public const int BOARD_WIDTH = 8;
        public const float TILE_HEIGHT = 3f;
        public const float TILE_WIDTH = 3f;
        public const float HALF = 1 / 2f;
        public const float QUARTER = 1 / 4f;
        public const string PIECES_SPRITE_LAYER = "GamePieces"; //Sorting layers in Unity
        public const string BOARD_TILES_LAYER = "Board";
        public const string HP_TEXT_LAYER = "StatusText";

        public const int DRONES_SPAWN_ROWS = 4;
        public const int HUMANS_SPAWN_ROWS = 2;
    }
}