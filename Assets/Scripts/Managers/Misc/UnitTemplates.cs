using AI;
using Defines;
using GamePieces.Drones;
using GamePieces.Humans;
using UnityEngine;
using Utils;


namespace Managers.Misc
{
    public struct UnitTemplates 
    {
     
        public static GameObject SetupHumanGruntPieces(int tileXPosition, int tileYPosition, int unitID)
        { 
            var gruntPiece = new GameObject("Grunt" + (unitID+1));
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
                
                var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
                chosenTile.SetOccupant(gruntPiece.GetComponent<GamePieces.Humans.GruntPiece>());
                
                var tilePosition = chosenTile.transform.position;
                var piecePosition = gruntPiece.transform;
                piecePosition.position = new Vector2(tilePosition.x, tilePosition.y); 
                gruntPiece.GetComponent<GruntPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

                return gruntPiece;
        }
        
        public static GameObject SetupHumanTankPieces(int tileXPosition, int tileYPosition, int unitID)
        {
            var tankPiece = new GameObject("Tank" + (unitID+1));
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

            var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
            chosenTile.SetOccupant(tankPiece.GetComponent<GamePieces.Humans.TankPiece>());
                
            var tilePosition = chosenTile.transform.position;
            var piecePosition = tankPiece.transform;
            piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
            tankPiece.GetComponent<TankPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

            return (tankPiece);
        }
        
        public static GameObject SetupHumanJumpshipPieces(int tileXPosition, int tileYPosition, int unitID)
        { 
            var jumpshipPiece = new GameObject("Jumpship" + (unitID+1));
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
                
            var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
            chosenTile.SetOccupant(jumpshipPiece.GetComponent<GamePieces.Humans.JumpshipPiece>());
                
            var tilePosition = chosenTile.transform.position;
            var piecePosition = jumpshipPiece.transform;
            piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
            piecePosition.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            jumpshipPiece.GetComponent<JumpshipPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

            return jumpshipPiece;
        }
        
        public static GameObject SetupDronePieces(int tileXPosition, int tileYPosition, int unitID)
        {
            var dronePiece = new GameObject("Drone" + (unitID + 1));
            dronePiece.AddComponent<DronePiece>();
            dronePiece.AddComponent<DroneDecisionLogic>();
            dronePiece.GetComponent<DronePiece>().UnitAiBehaviourLogic = dronePiece.GetComponent<DroneDecisionLogic>();
            dronePiece.AddComponent<SpriteRenderer>();
            dronePiece.GetComponent<SpriteRenderer>().sprite = dronePiece.GetComponent<DronePiece>().UnitSprite;
            dronePiece.GetComponent<SpriteRenderer>().flipX = true;
            dronePiece.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
            dronePiece.AddComponent<BoxCollider2D>();
            dronePiece.GetComponent<BoxCollider2D>().size = new Vector2(
                dronePiece.GetComponent<DronePiece>().BoxColliderSettings["sizeX"],
                dronePiece.GetComponent<DronePiece>().BoxColliderSettings["sizeY"]);
            dronePiece.GetComponent<BoxCollider2D>().offset = new Vector2(
                dronePiece.GetComponent<DronePiece>().BoxColliderSettings["offsetX"], dronePiece.GetComponent<DronePiece>().BoxColliderSettings["offsetY"]);

                
            var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
            chosenTile.SetOccupant(dronePiece.GetComponent<DronePiece>());
                
            var tilePosition = chosenTile.transform.position;
            var piecePosition = dronePiece.transform;
            piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
            piecePosition.localScale = new Vector3(0.60f, 0.60f, 0.60f);
            dronePiece.GetComponent<DronePiece>().SetCurrentPosition(tileXPosition, tileYPosition);

            return dronePiece;
            
        }
        
        public static GameObject SetupDreadnoughtPieces(int tileXPosition, int tileYPosition, int unitID) 
        {
            var dreadnoughtPiece = new GameObject("Dreadnought" + (unitID+1));
            dreadnoughtPiece.AddComponent<DreadnoughtPiece>();
            dreadnoughtPiece.AddComponent<DreadnoughtDecisionLogic>();
            dreadnoughtPiece.GetComponent<DreadnoughtPiece>().UnitAiBehaviourLogic = dreadnoughtPiece.GetComponent<DreadnoughtDecisionLogic>();
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
                
            var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
            chosenTile.SetOccupant(dreadnoughtPiece.GetComponent<DreadnoughtPiece>());
                
            var tilePosition = chosenTile.transform.position;
            var piecePosition = dreadnoughtPiece.transform;
            piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
            piecePosition.localScale = new Vector3(0.73f, 0.73f, 0.73f);
            dreadnoughtPiece.GetComponent<DreadnoughtPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

            return dreadnoughtPiece;
             
        }
        
        public static GameObject SetupCommandUnitPieces(int tileXPosition, int tileYPosition, int unitID) 
        {
            var ControlUnit = new GameObject("Command Unit" + (unitID));
            ControlUnit.AddComponent<CommandUnitPiece>();
            ControlUnit.AddComponent<CommandUnitDecisionLogic>();
            ControlUnit.GetComponent<CommandUnitPiece>().UnitAiBehaviourLogic = ControlUnit.GetComponent<CommandUnitDecisionLogic>();
            ControlUnit.AddComponent<SpriteRenderer>();
            ControlUnit.GetComponent<SpriteRenderer>().sprite = ControlUnit.GetComponent<CommandUnitPiece>().UnitSprite;
            ControlUnit.GetComponent<SpriteRenderer>().flipX = true;
            ControlUnit.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.PIECES_SPRITE_LAYER;
            ControlUnit.AddComponent<BoxCollider2D>();
            ControlUnit.GetComponent<BoxCollider2D>().size = new Vector2(
                ControlUnit.GetComponent<CommandUnitPiece>().BoxColliderSettings["sizeX"],
                ControlUnit.GetComponent<CommandUnitPiece>().BoxColliderSettings["sizeY"]);
            ControlUnit.GetComponent<BoxCollider2D>().offset = new Vector2(
                ControlUnit.GetComponent<CommandUnitPiece>().BoxColliderSettings["offsetX"],
                ControlUnit.GetComponent<CommandUnitPiece>().BoxColliderSettings["offsetY"]);
                
            var chosenTile = ConversionUtils.GetTileAtCoordinates(tileXPosition, tileYPosition);
            chosenTile.SetOccupant(ControlUnit.GetComponent<CommandUnitPiece>());
                
            var tilePosition = chosenTile.transform.position;
            var piecePosition = ControlUnit.transform;
            piecePosition.position = new Vector2(tilePosition.x, tilePosition.y);
            piecePosition.localScale = new Vector3(0.90f, 0.90f, 0.90f);
            ControlUnit.GetComponent<CommandUnitPiece>().SetCurrentPosition(tileXPosition, tileYPosition);

            return ControlUnit;
        }
        
    }
}