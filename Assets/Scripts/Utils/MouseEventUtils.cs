using System;
using Defines;
using GameBoard;
using GamePieces;
using Managers;
using Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class MouseEventUtils : MonoBehaviour
    {
        private const int _leftMouseButton = 0;
        private RaycastHit2D _target;
        private GameObject _selectedGamePiece;
        private GameObject _gameBoardObject;
        private GameBoard.GameBoard _gameBoardScript;
        private BasePiece _selectedGamePieceScript;

        private void Start()
        {
            this._gameBoardObject = GameObject.Find("GameBoard");
            this._gameBoardScript = this._gameBoardObject.GetComponent<GameBoard.GameBoard>();
            this._selectedGamePiece = null;
            this._selectedGamePieceScript = null;
        }
        
        private void Update()
        {
            // TODO Make it an event in some Manager
            if (Input.GetMouseButtonDown(_leftMouseButton))
            {
                this._target = GetMouseRealTarget();
                var extractedObject = ExtractTargetedObject(_target);
                Debug.Log(extractedObject);
                HandleSelectedObject(extractedObject);
            }
        }

        private static RaycastHit2D GetMouseRealTarget()
        {
            RaycastHit2D rayTargetFromCamera = Physics2D.GetRayIntersection(
                ray: Camera.main.ScreenPointToRay(Input.mousePosition),
                distance: float.MaxValue,
                layerMask: (1 << 0));

            return rayTargetFromCamera;
        }

        private GameObject ExtractTargetedObject(RaycastHit2D rayTarget)
        {
            return rayTarget.transform.GameObject();
        }

        private void HandleSelectedObject(GameObject clickedObject)
        {
            if (clickedObject is null)
                return;

            if (!this._selectedGamePiece && clickedObject.GetComponent<BoardTile>())
                //TODO: Optional add some indication visual that a tile was clicked
                return;
            
            if (!this._selectedGamePiece && clickedObject.TryGetComponent<BasePiece>(out this._selectedGamePieceScript))
                this._selectedGamePiece = clickedObject;

            //if (PieceManager.arePiecesSameTeam(this.)

            if (!this._selectedGamePieceScript.IsPieceActive) 
                return;

            if (this._selectedGamePieceScript.AllowedActions["move"])
                this._selectedGamePieceScript.HighlightMovePath();
            
            if (this._selectedGamePieceScript.AllowedActions["attack"])
                this._selectedGamePieceScript.HighlightThreatenedTiles();
            
            // TODO: Fix this and combine the two IFs
            // TODO MAke methods of the IFs, so it is easier to track and fix.
            // TODO Extract somehow the validation checks BEFORE calling the actions themselves. Maybe make actions as events that piece is listening for
            if (this._selectedGamePiece && clickedObject.TryGetComponent<BoardTile>(out var endTile))
            {
                // TODO: Fix this BS. Need to somehow handle who controls the flow and where. Shouldn't be getting the board here >.>
                var startTile = GameObject.Find("GameBoard").GetComponent<GameBoard.GameBoard>()
                    .GetTileFromMatrix(this._selectedGamePiece.GetComponent<BasePiece>().CurrentPieceCoordinates.x,
                        this._selectedGamePiece.GetComponent<BasePiece>().CurrentPieceCoordinates.y);
                
                this._selectedGamePiece.GetComponent<BasePiece>().MoveAction(endTile);
                startTile.GetComponent<BoardTile>().ClearOccupant();
                this.DeselectGamePiece();
                return;
            }

            if (this._selectedGamePiece && clickedObject.TryGetComponent<BasePiece>(out var attackTarget))
            {
                if (PieceManager.arePiecesSameTeam(attackTarget, this._selectedGamePiece.GetComponent<BasePiece>()))
                    return;
                
                var damageDone = this._selectedGamePiece.GetComponent<BasePiece>().DamageDone;
                this._selectedGamePiece.GetComponent<BasePiece>().AttackAction(attackTarget, damageDone);
                this.DeselectGamePiece();
                return;
            }
        }

        private void DeselectGamePiece()
        {
            this._gameBoardScript.ClearBoardColors();
            this._selectedGamePiece = null;
            this._selectedGamePieceScript = null;
        }
    }
}