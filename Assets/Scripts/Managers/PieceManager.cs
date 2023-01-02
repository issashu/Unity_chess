using System;
using System.Collections.Generic;
using Defines;
using GameBoard;
using GamePieces;
using GamePieces.Drones;
using GamePieces.Humans;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PieceManager: MonoBehaviour
    {
        // TODO Those events might need to be called by Game Manager
        public static event EventHandler OnHumansWipe;
        public static event EventHandler OnDronesWipe;
        public static event EventHandler OnAICommandUnitsWipe;
        
        // TODO: Remove the serialize field. It is just for debugging the list
         [SerializeField] private List<GameObject> _spawnedHumanPieces;
         [SerializeField] private List<GameObject> _spawnedAIPieces;
         [SerializeField] private List<GameObject> _spawnedAICommandUnits;
         [SerializeField] private GameObject _gameBoard;
        
        private void Awake()
        { 
            this._spawnedHumanPieces = new List<GameObject>();
            this._spawnedAIPieces = new List<GameObject>();
            this._spawnedAICommandUnits = new List<GameObject>();
            this._gameBoard = GameObject.Find("GameBoard");
            // TODO Think of a way to search faster for any alive command units
        }
        
        private void Start()
        {
            SetupHumanGruntPieces();
            SetupHumanJumpshipPieces();
            SetupHumanTankPieces();
            
            SetupCommandUnitPieces(); 
            SetupDronePieces();
            SetupDreadnoughtPieces();

            BasePiece.OnHealthZero += DestroyPiece;
        }

        private void Update()
        {
            if (this._spawnedHumanPieces.Count == 0)
            {
                OnHumansWipe?.Invoke(this, EventArgs.Empty);
            }
            
            if (this._spawnedAIPieces.Count == 0)
            {
                OnDronesWipe?.Invoke(this, EventArgs.Empty);
            }

            if (this._spawnedAICommandUnits.Count == 0)
            {
                OnAICommandUnitsWipe?.Invoke(this, EventArgs.Empty);
            }
        }

        //TODO Add delegate to call of of these methods at once.
        private void SetupHumanGruntPieces()
        {
            for (int i = 0; i < GameSettings.GRUNT_UNITS_AMMOUNT; i++)
            {
                var gruntPiece = new GameObject("Grunt" + (i+1));
                gruntPiece.AddComponent<GruntPiece>();
                gruntPiece.AddComponent<SpriteRenderer>();
                gruntPiece.GetComponent<SpriteRenderer>().sprite = gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>().UnitSprite;
                gruntPiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
                gruntPiece.AddComponent<BoxCollider2D>();
                gruntPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    gruntPiece.GetComponent<GruntPiece>().BoxColliderSettings["sizeX"],
                    gruntPiece.GetComponent<GruntPiece>().BoxColliderSettings["sizeY"]);
                gruntPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    gruntPiece.GetComponent<GruntPiece>().BoxColliderSettings["offsetX"],
                    gruntPiece.GetComponent<GruntPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = Random.Range(0, GameBoardConstants.HUMANS_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(gruntPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = gruntPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                gruntPiece.GetComponent<GruntPiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                gruntPiece.transform.parent = transform;

                this._spawnedHumanPieces.Add(gruntPiece);
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
                
                // TODO: Make method to grab tile by name. We use it multiple times 
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                chosenTile.GetComponent<BoardTile>().SetOccupant(tankPiece);
                var tilePosition = chosenTile.transform.position;

                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directly access structs...cop by value default...
                var piecePosition = tankPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                tankPiece.GetComponent<TankPiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                tankPiece.transform.parent = transform;

                this._spawnedHumanPieces.Add(tankPiece);
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
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(jumpshipPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = jumpshipPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                jumpshipPiece.GetComponent<JumpshipPiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                jumpshipPiece.transform.parent = transform;
                
                this._spawnedHumanPieces.Add(jumpshipPiece);
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

                int tileXPosition = GameBoardConstants.BOARD_HEIGHT -
                                    Random.Range(1, GameBoardConstants.DRONES_SPAWN_ROWS);
                Debug.Log(tileXPosition);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(dronePiece);

                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                var piecePosition = dronePiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.60f, 0.60f, 0.60f);
                dronePiece.GetComponent<DronePiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                dronePiece.transform.parent = transform;
                
                this._spawnedAIPieces.Add(dronePiece);
            }
        }
        private void SetupDreadnoughtPieces() {
            for (int i = 0; i < GameSettings.DREADNOUGHT_UNITS_AMMOUNT; i++)
            {
                var dreadnoughtPiece = new GameObject("Dreadnought" + (i+1));
                dreadnoughtPiece.AddComponent<DreadnoughtPiece>();
                dreadnoughtPiece.AddComponent<SpriteRenderer>();
                dreadnoughtPiece.GetComponent<SpriteRenderer>().sprite =
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().UnitSprite;
                dreadnoughtPiece.GetComponent<SpriteRenderer>().flipX = true;
                dreadnoughtPiece.GetComponent<SpriteRenderer>().sortingLayerName =
                    GameBoardConstants.PIECES_SPRITE_LAYER;
                dreadnoughtPiece.AddComponent<BoxCollider2D>();
                dreadnoughtPiece.GetComponent<BoxCollider2D>().size = new Vector2(
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["sizeX"],
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["sizeY"]);
                dreadnoughtPiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["offsetX"],
                    dreadnoughtPiece.GetComponent<DreadnoughtPiece>().BoxColliderSettings["offsetY"]);

                int tileXPosition = GameBoardConstants.BOARD_HEIGHT -
                                    Random.Range(1, GameBoardConstants.DRONES_SPAWN_ROWS);
                
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                var tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(dreadnoughtPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# ay can't directl access structs...cop by value default...
                // TODO: Add it as part of SetCurrentPosition - set coord and position in space :3
                var piecePosition = dreadnoughtPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.73f, 0.73f, 0.73f);
                dreadnoughtPiece.GetComponent<DreadnoughtPiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                dreadnoughtPiece.transform.parent = transform;
                
                this._spawnedAIPieces.Add(dreadnoughtPiece);
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

                int tileXPosition = GameBoardConstants.BOARD_HEIGHT - Random.Range(1, GameBoardConstants.DRONES_SPAWN_ROWS);
                int tileYPosition = Random.Range(0, GameBoardConstants.BOARD_WIDTH);
                var chosenTile = GameObject.Find($"Tile {tileXPosition} {tileYPosition}");
                var tilePosition = new Vector3(); 
                tilePosition = chosenTile.transform.position;
                chosenTile.GetComponent<BoardTile>().SetOccupant(ControlPiece);
                
                // TODO: Add a method in utils or something for transform position since we will have quite a bit. C# can't directly access structs...copy by value default...
                var piecePosition = ControlPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
                piecePosition.localScale = new Vector3(0.90f, 0.90f, 0.90f);
                ControlPiece.GetComponent<CommandUnitPiece>().SetCurrentPosition(tileXPosition, tileYPosition);
                ControlPiece.transform.parent = transform;

                this._spawnedAICommandUnits.Add(ControlPiece);
            } 
        }

        public bool arePiecesSameteam(BasePiece one, BasePiece other)
        {
            return one.PieceFaction == other.PieceFaction;
        }

        private void DestroyPiece(object sender, BasePiece deadPiece)
        {
            var faction = deadPiece.PieceFaction;
            switch (faction)
            {
                case 1: // Drones
                    this._spawnedAIPieces.Remove(deadPiece.gameObject);
                    break;
                
                case 2: // Humans
                    this._spawnedHumanPieces.Remove(deadPiece.gameObject);
                    break;
            }
            var boardTilePieceIsOn =
                GameObject.Find($"Tile {deadPiece.CurrentPieceCoordinates.x} {deadPiece.CurrentPieceCoordinates.y}");
            boardTilePieceIsOn.GetComponent<BoardTile>().ClearOccupant();
            deadPiece.OnDestroy();
            Destroy(deadPiece.gameObject);
        }

        public List<GameObject> AiPieces => this._spawnedAIPieces;
        public List<GameObject> HumanPieces => this._spawnedHumanPieces;
    }
}