using System.Collections.Generic;
using System.Drawing;
using Defines;
using GameBoard;
using GamePieces.Drones;
using GamePieces.Humans;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //PRIVATE:
        [SerializeField] private List<GameObject> _spawnedHumanUnits;
        [SerializeField] private List<GameObject> _spawnedDroneUnits;
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
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(gruntPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = gruntPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                
                _spawnedHumanUnits.Add(gruntPiece);
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
                
                // TODO: Make methjod to grab tile by name. We use it multiple times 
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                chosenTile.GetComponent<BoardTile>().SetOccupant(tankPiece);
                var tilePosition = chosenTile.transform.position;

                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = tankPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                tankPiece.GetComponent<TankPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

                _spawnedHumanUnits.Add(tankPiece);
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
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(jumpshipPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = jumpshipPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                
                _spawnedHumanUnits.Add(jumpshipPiece);
            }
        }

        private void SetupDronePieces()
        {
            for (int i = 0; i < GameSettings.DRONE_UNITS_AMMOUNT; i++)
            {
                var dronePiece = new GameObject("Drone" + (i + 1));
                dronePiece.AddComponent<DronePiece>();
                dronePiece.AddComponent<SpriteRenderer>();
                dronePiece.GetComponent<SpriteRenderer>().sprite = dronePiece.GetComponent<DronePiece>().UnitSprite;
                dronePiece.GetComponent<SpriteRenderer>().flipX = true;
                dronePiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                dronePiece.AddComponent<BoxCollider2D>();
                dronePiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    dronePiece.GetComponent<DronePiece>().BoxColliderSettings["sizeX"],
                    dronePiece.GetComponent<DronePiece>().BoxColliderSettings["sizeY"]);
                dronePiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    dronePiece.GetComponent<DronePiece>().BoxColliderSettings["offsetX"],
                    dronePiece.GetComponent<DronePiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = (GameBoardConstants.BOARD_HEIGHT - 1) -
                                    Random.Range(0, GameBoardConstants.DRONES_SPAWN_ROWS);
                Debug.Log(tileXPosition);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(dronePiece);

                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = dronePiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);


                _spawnedDroneUnits.Add(dronePiece);
            }
        }

        private void SetupDreadnoughtPieces() {
            for (int i = 0; i < GameSettings.DREADNOUGHT_UNITS_AMMOUNT; i++)
            {
                var dreadnoughtPiece = new GameObject("Dreadnought" + (i+1));
                dreadnoughtPiece.AddComponent<DreadnoughtPiece>();
                dreadnoughtPiece.AddComponent<SpriteRenderer>();
                dreadnoughtPiece.GetComponent<SpriteRenderer>().sprite = dreadnoughtPiece.GetComponent<DreadnoughtPiece>().UnitSprite;
                dreadnoughtPiece.GetComponent<SpriteRenderer>().flipX = true;
                dreadnoughtPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                dreadnoughtPiece.AddComponent<BoxCollider2D>();
                dreadnoughtPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["sizeX"],
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["sizeY"]);
                dreadnoughtPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["offsetX"],
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = GameBoardConstants.BOARD_HEIGHT - Random.Range(0, GameBoardConstants.DRONES_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(dreadnoughtPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = dreadnoughtPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                
                
                _spawnedDroneUnits.Add(dreadnoughtPiece);
            } 
        }
        
        private void SetupCommandUnitPieces() {
            for (int i = 0; i < GameSettings.CONTROL_UNITS_AMMOUNT; i++)
            {
                var ControlPiece = new GameObject("Command Unit" + (i+1));
                ControlPiece.AddComponent<CommandUnitPiece>();
                ControlPiece.AddComponent<SpriteRenderer>();
                ControlPiece.GetComponent<SpriteRenderer>().sprite = ControlPiece.GetComponent<CommandUnitPiece>().UnitSprite;
                ControlPiece.GetComponent<SpriteRenderer>().flipX = true;
                ControlPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                ControlPiece.AddComponent<BoxCollider2D>();
                ControlPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    ControlPiece.GetComponent<CommandUnitPiece>().BoxColliderSettings["sizeX"],
                    ControlPiece.GetComponent<CommandUnitPiece>().BoxColliderSettings["sizeY"]);
                ControlPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    ControlPiece.GetComponent<CommandUnitPiece>().BoxColliderSettings["offsetX"],
                    ControlPiece.GetComponent<CommandUnitPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = GameBoardConstants.BOARD_HEIGHT - Random.Range(0, GameBoardConstants.DRONES_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find("Tile: " + tileXPosition + " " + tileYPosition);
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(ControlPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = ControlPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                
                
                _spawnedDroneUnits.Add(ControlPiece);
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
            SetupDronePieces();
            SetupDreadnoughtPieces();
            SetupCommandUnitPieces();
        }

        // Update is called once per frame
        void Update()
        {
            
            
        }
    }
}

