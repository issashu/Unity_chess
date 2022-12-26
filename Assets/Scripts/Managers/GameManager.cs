using Humans = GamePieces.Humans;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //PRIVATE:
   
    
        //PUBLIC:
    

        //UNITY SPECIFIC:
        // Start is called before the first frame update
        void Start()
        {
            var gameBoard = new GameObject("GameBoard");
            gameBoard.AddComponent<GameBoard.GameBoard>();

            var tankPiece = new GameObject("Tank");
            tankPiece.AddComponent<Humans.TankPiece>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

