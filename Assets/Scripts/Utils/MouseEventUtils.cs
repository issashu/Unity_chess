using System;
using UnityEngine;

namespace Utils
{
    public class MouseEventUtils : MonoBehaviour
    {
        private const int _leftMouseButton = 0;
        
        private RaycastHit2D _target;

        private void Awake()
        {
            
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(_leftMouseButton))
            {
                _target = GetMouseRealTarget();
                Debug.Log(_target.transform);
            } 
        }
        //So we don't need to instantiate the class to use the methods
        public static RaycastHit2D GetMouseRealTarget()
        {
            RaycastHit2D rayTargetFromCamera = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            return rayTargetFromCamera;
        } 
    }
}