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
        private GameBoard.GameBoard _gameBoardScript;
        private BasePiece _selectedGamePieceScript;

        private void Start()
        {
            this._gameBoardScript = GameBoard.GameBoard.Board;
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
            
            if (!this._selectedGamePiece)
            {
                if (clickedObject.GetComponent<BoardTile>())
                    return;

                if (clickedObject.TryGetComponent<BasePiece>(out this._selectedGamePieceScript))
                {
                    if (this._selectedGamePieceScript.IsPieceActive)
                    {
                        this._selectedGamePiece = clickedObject;
                        this._selectedGamePieceScript.HighlightMovePath();
                        this._selectedGamePieceScript.HighlightThreatenedTiles();
                        return;
                    }
                    
                }
                    
            }
            
            if (this._selectedGamePiece == clickedObject)
            {
                this.DeselectGamePiece();
                return;
            }

            // TODO: Fix this and combine the two IFs
            // TODO MAke methods of the IFs, so it is easier to track and fix.
            // TODO Extract somehow the validation checks BEFORE calling the actions themselves. Maybe make actions as events that piece is listening for
            if (this._selectedGamePiece && clickedObject.TryGetComponent<BoardTile>(out var endTile))
            {
                // TODO: Fix this BS. Need to somehow handle who controls the flow and where. Shouldn't be getting the board here >.>
                var startTile = Utils.ConversionUtils.GetTileAtPoint(this._selectedGamePiece.GetComponent<BasePiece>().CurrentPieceCoordinates);
                
                this._selectedGamePiece.GetComponent<BasePiece>().MoveAction(startTile, endTile);
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