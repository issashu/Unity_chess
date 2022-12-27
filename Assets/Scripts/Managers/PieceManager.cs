using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PieceManager : MonoBehaviour
    {
        // TODO: Remove the serialize field. It is just for debugging the list
        [SerializeField] private List<GameObject> _spawnedUnits;
        private void Awake()
        {
            _spawnedUnits = new List<GameObject>();
            
            var gruntPiece = new GameObject("Grunt");
            gruntPiece.AddComponent<GamePieces.Humans.GruntPiece>();
            gruntPiece.AddComponent<SpriteRenderer>();
            var piecePosition = gruntPiece.transform.position;
            var tileToSpawn = GameObject.Find("Tile: 0 0");
            
            
            
            
            
            
            _spawnedUnits.Add(gruntPiece);
            
            var tankPiece = new GameObject("Tank");
            tankPiece.AddComponent<GamePieces.Humans.TankPiece>();
            _spawnedUnits.Add(tankPiece);

            var jumpshipPiece = new GameObject("JumpShip");
            jumpshipPiece.AddComponent<GamePieces.Humans.JumpshipPiece>();
            _spawnedUnits.Add(jumpshipPiece);
        }
    }
}