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
        protected List<GameObject> droneUnits;
        protected List<GameObject> commandUnits;
        
        protected PieceManager pieceManager;
        protected MouseEventUtils mouseEventUtils;
        protected AIActions aiActions;
        
        protected virtual void Awake()
        {
            
        }

        protected virtual void ExecuteUnitBehaviour(BasePiece droneUnit)
        {
            Debug.Log("Not implemented for Base CLass");
            /*All the kids need to have an override for this one*/
        }
        
        

        protected virtual void Start()
        {
            pieceManager = PieceManager.Instance;
            mouseEventUtils = MouseEventUtils.Instance;
            aiActions = AIActions.AiActions;
            
            humanUnits = pieceManager.HumanPieces;
            droneUnits = pieceManager.AiPieces;
            commandUnits = pieceManager.DroneCommandUnits;
            
        }
        
    }
}