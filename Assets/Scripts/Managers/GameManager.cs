using System.Collections.Generic;
using Defines;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //PRIVATE:
        [SerializeField] private List<GameObject> _spawnedUnits;
        private int _gameDifficulty;

        private void SetupHumanGruntPieces()
        {
            for (int i = 0; i < GameSettings.GRUNT_UNITS_AMMOUNT; i++)
            {
                int tileXPosition;
                int tileYPosition;
                
                var gruntPiece = new GameObject("Grunt" + (i+1));
                gruntPiece.AddComponent<GamePieces.Humans.GruntPiece>();
                gruntPiece.AddComponent<SpriteRenderer>();
                gruntPiece.GetComponent<SpriteRenderer>().sprite = gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().UnitSprite;
                gruntPiece.layer = GameBoardConstants.PIECES_SPRITE_LAYER;
                
                tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
                var tilePosition = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition).transform.position;
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = gruntPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                
                _spawnedUnits.Add(gruntPiece);
            }
        }
        private void SetupHumanTankPieces()
        {
            for (int i = 0; i < GameSettings.TANK_UNITS_AMMOUNT; i++)
            {
                int tileXPosition;
                int tileYPosition;
                
                var tankPiece = new GameObject("Tank" + (i+1));
                tankPiece.AddComponent<GamePieces.Humans.TankPiece>();
                tankPiece.AddComponent<SpriteRenderer>();
                tankPiece.GetComponent<SpriteRenderer>().sprite = tankPiece.GetComponent<GamePieces.Humans.TankPiece>().UnitSprite;
                tankPiece.layer = GameBoardConstants.PIECES_SPRITE_LAYER;
                
                tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
                var tilePosition = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition).transform.position;
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = tankPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                
                _spawnedUnits.Add(tankPiece);
            }
        }
        private void SetupHumanJumpshipPieces()
        {
            for (int i = 0; i < GameSettings.JUMPSHIP_UNITS_AMMOUNT; i++)
            {
                int tileXPosition;
                int tileYPosition;
                
                var jumpshipPiece = new GameObject("Jumpship" + (i+1));
                jumpshipPiece.GameObject().AddComponent<SpriteRenderer>();
                jumpshipPiece.AddComponent<GamePieces.Humans.JumpshipPiece>();
                //jumpshipPiece.AddComponent<SpriteRenderer>();
                jumpshipPiece.GetComponent<SpriteRenderer>().sprite = jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().UnitSprite;
                jumpshipPiece.layer = GameBoardConstants.PIECES_SPRITE_LAYER;

                tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
                var tilePosition = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition).transform.position;
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = jumpshipPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                
                _spawnedUnits.Add(jumpshipPiece);
            }
        }

        //PUBLIC:
    

        //UNITY SPECIFIC:
        // Start is called before the first frame update
        void Start()
        {
            var gameBoard = new GameObject("GameBoard");
            gameBoard.AddComponent<GameBoard.GameBoard>();
            
            SetupHumanGruntPieces();
            SetupHumanTankPieces();
            SetupHumanJumpshipPieces();
        }

        // Update is called once per frame
        void Update()
        {
           // Debug.Log(Input.mousePosition);
        }
    }
}

