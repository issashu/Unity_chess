using System;
using System.Collections.Generic;
using UnityEngine;
using GamePieces;
using Managers;
using Utils;

namespace AI
{
    public class AIDecisionLogic : MonoBehaviour
    {
        protected List<GameObject> humanUnits;
        protected PieceManager pieceManager;
        protected AiController AiController;
        
        protected virtual void Start()
        {
            pieceManager = PieceManager.Instance;
            AiController = AiController.AIController;
            
            humanUnits = pieceManager.HumanPieces;
        }

        public virtual void ExecuteUnitBehaviour(BasePiece droneUnit)
        {
            Debug.Log("Not implemented for Base CLass");
            /*All the kids need to have an override for this one*/
        }
    }
}