using System.Collections;
using System.Collections.Generic;
using GameBoard;
using Unity.VisualScripting;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

public class ChessBoardManager : MonoBehaviour
{
    //PRIVATE:
   
    
    //PUBLIC:
    

    //UNITY SPECIFIC:
    // Start is called before the first frame update
    void Start()
    {
        var Board = new GameObject("GameBoard");
        Board.AddComponent<GameBoard.GameBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
