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
        private static MouseEventUtils _instance;
        public static MouseEventUtils Instance => _instance;
        
        
        private const int _leftMouseButton = 0;
        private RaycastHit2D _target;
        private GameObject _selectedGamePiece;
        private GameBoard.GameBoard _gameBoardScript;
        private BasePiece _selectedGamePieceScript;

        private void Start()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            
            
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
                        this.SelectGamePiece();
                        return;
                    }
                }
            }
            
            if (this._selectedGamePiece == clickedObject)
            {
                this.DeselectGamePiece();
                return;
            }

            if (this._selectedGamePiece)
            {
                if (clickedObject.TryGetComponent<BoardTile>(out var clickedTile))
                {
                    this.ExecuteMoveAction(clickedTile);
                    this.DeselectGamePiece();
                    return;
                }

                if (clickedObject.TryGetComponent<BasePiece>(out var clickedPiece))
                {
                    this.ExecuteAttackAction(clickedPiece);
                    this.DeselectGamePiece();
                    return;
                }
            }
        }

        private void SelectGamePiece()
        {
            if (!this._selectedGamePieceScript)
                return;
            
            this._selectedGamePieceScript.ChangePieceColor(Color.yellow);
            this._selectedGamePieceScript.HighlightMovePath();
            this._selectedGamePieceScript.HighlightThreatenedTiles();
        }
        
        private void DeselectGamePiece()
        {
            if (!this._selectedGamePieceScript || !this._gameBoardScript)
                return;
            
            this._gameBoardScript.ClearBoardColors();
            this._selectedGamePieceScript.ChangePieceColor(Color.white);
            this._selectedGamePiece = null;
            this._selectedGamePieceScript = null;
        }
        
        private void ExecuteMoveAction(BoardTile destinationTile)
        {
            if (!this._selectedGamePieceScript)
                return;
            
            var startTile = Utils.ConversionUtils.GetTileAtPoint(this._selectedGamePieceScript.CurrentPieceCoordinates);
                
            this._selectedGamePieceScript.MoveAction(startTile, destinationTile);
        }

        private void ExecuteAttackAction(BasePiece attackTarget)
        {
            if (!this._selectedGamePieceScript)
                return;
            
            if (PieceManager.arePiecesSameTeam(attackTarget, this._selectedGamePieceScript))
                return;
            
            this._selectedGamePieceScript.AttackAction(attackTarget);
        }
    }
}