using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    public class SimpleFollow : MonoBehaviour
    {
        public Transform target;
        
        public Vector3 offset;
        
        private bool targetFound;
        
        private Vector3 velocity;
        
        private Vector3 currentPosition;
        
        private void OnEnable()
        {
            targetFound = target != null;
            
            currentPosition = target.position;
            
            transform.position = currentPosition;
        }
        
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                targetFound = target != null;
            }
        }
        
        private void Update()
        {
            if (targetFound)
            {
                currentPosition = Vector3.SmoothDamp(currentPosition, target.position, ref velocity, 0.15f);
                
                transform.position = currentPosition + offset;
            }
        }
    }
}




