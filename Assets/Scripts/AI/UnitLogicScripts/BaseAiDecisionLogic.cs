using System.Collections.Generic;
using UnityEngine;
using GamePieces;
using Managers;


namespace AI
{
    public class AIDecisionLogic : MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        protected List<GameObject> humanUnits;
        protected PieceManager pieceManager;
        protected AiController AiController;
        
        /*-----------METHODS-------------------*/
        public virtual void ExecuteUnitBehaviour(BasePiece droneUnit)
        {
            Debug.Log("Not implemented for Base CLass");
            /*All the kids need to have an override for this one*/
        }
        protected virtual void Start()
        {
            pieceManager = PieceManager.Instance;
            AiController = AiController.AIController;
            
            humanUnits = pieceManager.HumanPieces;
        }
    }
}