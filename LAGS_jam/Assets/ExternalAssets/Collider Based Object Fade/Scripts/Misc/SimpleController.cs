using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    [RequireComponent(typeof(CharacterController))]
    
    public class SimpleController : MonoBehaviour
    {
        [Range(0f, 8.0f)]
        public float moveSpeed = 4.5f;
        
        private Vector2 moveInput;
        private bool inputDetected;
        
        private CharacterController controller;
        private Transform cameraTransform;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            
            cameraTransform = Camera.main.transform;
        }
        
        private void Update()
        {
            GetMoveInput();
            
            if (inputDetected)
            {
                float targetAngle =  Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                Vector3 horizontal = moveDirection.normalized * moveSpeed * Time.deltaTime;
                Vector3 vertical = Vector3.down * 2.0f;
                
                controller.Move(horizontal + vertical);
            }
        }
        
        private void GetMoveInput()
        {
            int x = 0;
            int y = 0;
            
            if (Input.GetKey(KeyCode.W))
            {
                y ++;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                x --;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                y --;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                x ++;
            }
            
            moveInput = new Vector2(x, y);
            
            inputDetected = (x != 0 || y != 0);
        }
    }
}




