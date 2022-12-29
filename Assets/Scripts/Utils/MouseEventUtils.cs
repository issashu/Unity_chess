using System;
using Defines;
using GameBoard;
using GamePieces;
using Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class MouseEventUtils : MonoBehaviour
    {
        private const int _leftMouseButton = 0;
        private RaycastHit2D _target;
        private GameObject selectedGameObject;

        public void Update()
        {
            if (Input.GetMouseButtonDown(_leftMouseButton))
            {
                var board = GameObject.Find("GameBoard");
                board.GetComponent<GameBoard.GameBoard>().ClearBoardColors();
                _target = GetMouseRealTarget();
                var extractedObject = ExtractTargetedObject(_target);
                Debug.Log(extractedObject);
                HandleSelectedObject(extractedObject);
                
            } 
        }
        //So we don't need to instantiate the class to use the methods
        public static RaycastHit2D GetMouseRealTarget()
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

        public void HandleSelectedObject(GameObject clickedObject)
        {
            // TODO: Fix this
            if (clickedObject == null)
            {
                return;
            }
            // Add check for current or opposing team
            if (clickedObject.TryGetComponent<BasePiece>(out BasePiece gamePiece))
            {
                gamePiece.HighlightMovePath();
                selectedGameObject = clickedObject;
                return;
            }

            if (clickedObject.TryGetComponent<BoardTile>(out BoardTile tile) && selectedGameObject)
            {
                var moveLocation = ConversionUtils.WorldPositionFromCoordinates(tile.XCoordinate, tile.YCoordinate);
                selectedGameObject.GetComponent<BasePiece>().MoveAction(moveLocation);
                selectedGameObject = null;
                return;
            }
            // TODO: Add logic for selecting enemy unit and attacking
        }
    }
}