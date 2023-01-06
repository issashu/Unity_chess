using Defines;
using Managers;
using Managers.Misc;

namespace GameBoard
{
    public class BoardSpawner
    {
        public static void easyBoardSpawner(int gameDifficulty)
        {
            var piecerManageInstance = PieceManager.Instance;
            char[,] spawnMatrix = new char[GameBoardConstants.BOARD_HEIGHT, GameBoardConstants.BOARD_WIDTH];
            
            switch (gameDifficulty)
            {
                case (int)Enums.GameDifficulty.Easy:
                    spawnMatrix = SpawnMatrix.easyMatrix;
                    break;
                
                case (int)Enums.GameDifficulty.Normal:
                    spawnMatrix = SpawnMatrix.normalMatrix;
                    break;
                
                case (int)Enums.GameDifficulty.Hard:
                    spawnMatrix = SpawnMatrix.hardMatrix;
                    break;
            }
            

            for (int x = 0; x < GameBoardConstants.BOARD_HEIGHT; x++)
            {
                for (int y = 0; y < GameBoardConstants.BOARD_WIDTH; y++)
                {
                    switch (spawnMatrix[x,y])
                    {
                        case GameSettings.GRUNT_LETTER:
                            piecerManageInstance.HumanPieces.Add(UnitTemplates.SetupHumanGruntPieces(x, y, (x + y)));
                            break;
                        case GameSettings.JUMPSHIP_LETTER:
                            piecerManageInstance.HumanPieces.Add(UnitTemplates.SetupHumanJumpshipPieces(x, y, (x + y)));
                            break;
                        case GameSettings.TANK_LETTER:
                            piecerManageInstance.HumanPieces.Add(UnitTemplates.SetupHumanTankPieces(x, y, (x + y)));
                            break;
                        case GameSettings.DRONE_LETTER:
                            piecerManageInstance.AiPieces.Add(UnitTemplates.SetupDronePieces(x, y, (x + y)));
                            break;
                        case GameSettings.DREADNOUGHT_LETTER:
                            piecerManageInstance.AiPieces.Add(UnitTemplates.SetupDreadnoughtPieces(x, y, (x + y)));
                            break;
                        case GameSettings.CONTROL_UNIT_LETTER:
                            piecerManageInstance.DroneCommandUnits.Add(UnitTemplates.SetupCommandUnitPieces(x, y, (x + y)));
                            break;
                    }
                }
            }
        }
    }
}