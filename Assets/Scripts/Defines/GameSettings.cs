using System.Collections.Generic;

namespace Defines
{
    public struct GameSettings
    {
        
        /*-------GAME SETUP SETTINGS----------*/

        /*-------HUMAN PLAYER SETTINGS----------*/
        public const int GRUNT_UNITS_AMMOUNT = 4;
        public const int JUMPSHIP_UNITS_AMMOUNT = 2;
        public const int TANK_UNITS_AMMOUNT = 1;
        public const int HUMAN_MIN_ROW = 0;
        public const int HUMAN_MAX_ROW = 2;

        public const char GRUNT_LETTER = 'G';
        public const char JUMPSHIP_LETTER = 'J';
        public const char TANK_LETTER = 'T';
        
        /*---------DRONES PLAYER SETTINGS----------*/
        
        public const int DRONE_UNITS_AMMOUNT = 2;
        public const int DREADNOUGHT_UNITS_AMMOUNT = 1;
        public const int CONTROL_UNITS_AMMOUNT = 1;
        public const int DRONES_MIN_ROW = 4;
        public const int DRONES_MAX_ROW = 8;
        
        public const char DRONE_LETTER = 'D';
        public const char DREADNOUGHT_LETTER = 'R';
        public const char CONTROL_UNIT_LETTER = 'C';
        
        /*--------MISC GAME SETTINGS-----------*/
        public const float DEFAULT_AI_WAIT_TIMER = 3f;
        public const int GAME_DIFFICULTY = 1;
    }
}