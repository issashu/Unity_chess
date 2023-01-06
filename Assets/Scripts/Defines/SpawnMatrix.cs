namespace Defines
{
    public struct SpawnMatrix
    {
        /*This would be best loaded from a predefined settings file (maybe json or csv), but just dumped here a hardcoded
         spawn Matrix for now. UGLY, but does the trick for the exercise*/
     
        /*-----------MEMBERS-------------------*/
        public static char[,] easyMatrix = new char[GameBoardConstants.BOARD_HEIGHT, GameBoardConstants.BOARD_WIDTH];
        public static char[,] normalMatrix = new char[GameBoardConstants.BOARD_HEIGHT, GameBoardConstants.BOARD_WIDTH];
        public static char[,] hardMatrix = new char[GameBoardConstants.BOARD_HEIGHT, GameBoardConstants.BOARD_WIDTH];
        
        /*-----------METHODS-------------------*/
        public static void InitialiseSpawnMatrix()
        {
            for (int i = 0; i < GameBoardConstants.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < GameBoardConstants.BOARD_WIDTH; j++)
                {
                    easyMatrix[i, j] = 'x';
                    normalMatrix[i, j] = 'x';
                    hardMatrix[i, j] = 'x';
                }
            }
        }

        public static void SetSpawns()
        {
            /*Human side hardcoded*/
            easyMatrix[0, 1] = normalMatrix[0, 1] = hardMatrix[0, 1] = GameSettings.JUMPSHIP_LETTER;
            easyMatrix[0, 6] = normalMatrix[0, 6] = hardMatrix[0, 6] = GameSettings.JUMPSHIP_LETTER;
            easyMatrix[0, 3] = normalMatrix[0, 3] = hardMatrix[0, 3] = GameSettings.TANK_LETTER;
            
            for (int x = 1; x < GameSettings.HUMAN_MAX_ROW; x++)
            {
                for (int y = 1; y < GameBoardConstants.BOARD_WIDTH; y++)
                {
                    easyMatrix[x, y] = GameSettings.GRUNT_LETTER;
                    normalMatrix[x, y] = GameSettings.GRUNT_LETTER;
                    hardMatrix[x, y] = GameSettings.GRUNT_LETTER;
                }
            }
            
            /*AI side hardcoded*/
            easyMatrix[7, 4] = normalMatrix[7, 4] = hardMatrix[7, 4] = GameSettings.CONTROL_UNIT_LETTER;
            easyMatrix[7, 5] = normalMatrix[7, 5] = hardMatrix[7, 5] = GameSettings.DREADNOUGHT_LETTER;
            normalMatrix[5, 0] = hardMatrix[5, 0] = GameSettings.DRONE_LETTER;
            normalMatrix[5, 1] = hardMatrix[5, 1] = GameSettings.DRONE_LETTER;
            normalMatrix[5, 2] = hardMatrix[5, 2] = GameSettings.DREADNOUGHT_LETTER;
            normalMatrix[6, 2] = hardMatrix[6, 2] = GameSettings.CONTROL_UNIT_LETTER;
            hardMatrix[5, 6] = GameSettings.CONTROL_UNIT_LETTER;
            hardMatrix[4, 6] = GameSettings.DREADNOUGHT_LETTER;
            hardMatrix[6, 7] = GameSettings.DRONE_LETTER;
            hardMatrix[4, 7] = GameSettings.DRONE_LETTER;
            
            for (int x = 7; x < GameSettings.DRONES_MAX_ROW; x++)
            {
                for (int y = 3; y < GameBoardConstants.BOARD_WIDTH; y++)
                {
                    easyMatrix[x, y] = GameSettings.DRONE_LETTER;
                    normalMatrix[x, y] = GameSettings.DRONE_LETTER;
                    hardMatrix[x, y] = GameSettings.DRONE_LETTER;
                }
            }
        }
    }
}