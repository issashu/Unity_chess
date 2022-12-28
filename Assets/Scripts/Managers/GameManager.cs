using System.Collections.Generic;
using System.Drawing;
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
        // TODO: Move the settings logic within the specific unit script. The Manager will just take the number of units in 3 for cycles and attach scripts to them
        {
            for (int i = 0; i < GameSettings.GRUNT_UNITS_AMMOUNT; i++)
            {
                var gruntPiece = new GameObject("Grunt" + (i+1));
                gruntPiece.AddComponent<GamePieces.Humans.GruntPiece>();
                gruntPiece.AddComponent<SpriteRenderer>();
                gruntPiece.GetComponent<SpriteRenderer>().sprite = gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().UnitSprite;
                gruntPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                gruntPiece.AddComponent<BoxCollider2D>();
                gruntPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().BoxColliderSettings["sizeX"],
                    gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().BoxColliderSettings["sizeY"]);
                gruntPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().BoxColliderSettings["offsetX"],
                    gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
                var tilePosition = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition).transform.position;
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = gruntPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                
                _spawnedUnits.Add(gruntPiece);
            }
        }
        private void SetupHumanTankPieces()
        {
            for (int i = 0; i < GameSettings.TANK_UNITS_AMMOUNT; i++)
            {
                var tankPiece = new GameObject("Tank" + (i+1));
                tankPiece.AddComponent<GamePieces.Humans.TankPiece>();
                tankPiece.AddComponent<SpriteRenderer>();
                tankPiece.GetComponent<SpriteRenderer>().sprite = tankPiece.GetComponent<GamePieces.Humans.TankPiece>().UnitSprite;
                tankPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                tankPiece.AddComponent<BoxCollider2D>();
                tankPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    tankPiece.GetComponent<GamePieces.Humans.TankPiece>().BoxColliderSettings["sizeX"],
                    tankPiece.GetComponent<GamePieces.Humans.TankPiece>().BoxColliderSettings["sizeY"]);
                tankPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    tankPiece.GetComponent<GamePieces.Humans.TankPiece>().BoxColliderSettings["offsetX"],
                    tankPiece.GetComponent<GamePieces.Humans.TankPiece>().BoxColliderSettings["offsetY"]);
                
                
                int tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
                var tilePosition = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition).transform.position;
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = tankPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                
                _spawnedUnits.Add(tankPiece);
            }
        }
        private void SetupHumanJumpshipPieces()
        {
            for (int i = 0; i < GameSettings.JUMPSHIP_UNITS_AMMOUNT; i++)
            {
                var jumpshipPiece = new GameObject("Jumpship" + (i+1));
                jumpshipPiece.AddComponent<GamePieces.Humans.JumpshipPiece>();
                jumpshipPiece.AddComponent<SpriteRenderer>();
                jumpshipPiece.GetComponent<SpriteRenderer>().sprite = jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().UnitSprite;
                jumpshipPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                jumpshipPiece.AddComponent<BoxCollider2D>();
                jumpshipPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().BoxColliderSettings["sizeX"],
                    jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().BoxColliderSettings["sizeY"]);
                jumpshipPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().BoxColliderSettings["offsetX"],
                    jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                
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

