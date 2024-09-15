using System;
using System.Collections;
using System.Collections.Generic;
using PlayerController;
using UnityEngine;


namespace PlayerController
{
    public class PC_Controller : MonoBehaviour
    {
        private PC_Manager _pcManager;
        
        [SerializeField] internal float lookSpeed = 2.0f;
        [SerializeField] internal float lookXLimit = 45.0f;

        [Space]
        [SerializeField] internal float walkSpeed = 6;
        [SerializeField] internal float runSpeed = 12;
    
        internal virtual void Start()
        {
            _pcManager = transform.GetComponent<PC_Manager>();
        }

        internal void Update()
        {
            HandleCameraRot();
        }

        private Vector3 HandleDirInput()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = _pcManager.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = _pcManager.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = _pcManager.moveDirection.y;
            _pcManager.moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            
            return _pcManager.moveDirection;
        }

        private void HandleCameraRot()
        {
            _pcManager.rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            _pcManager.rotationX = Mathf.Clamp(_pcManager.rotationX, -lookXLimit, lookXLimit);
            _pcManager.playerCamera.transform.localRotation = Quaternion.Euler(_pcManager.rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        internal virtual void FixedUpdate()
        {
            MovePC();
            Gravity();
            Drag();
        }
        

        internal virtual void MovePC()
        {
            _pcManager.rb.AddForce(HandleDirInput(), ForceMode.Force);
        }

        internal virtual bool CheckIfGrounded()
        {
            bool touchedGround = false;
            RaycastHit hit;
            
            if (!Physics.Raycast(transform.position, -transform.up, out hit, 1)) return touchedGround;
            Debug.DrawRay(transform.position, -transform.up * 1, Color.red);
            touchedGround = hit.transform.GetComponent<Collider>();
            
            return touchedGround;
        }
        
        internal virtual void Gravity()
        {
            _pcManager.rb.AddForce(Vector3.down * 9.81f, ForceMode.Force); 
        }
        
        internal virtual void Drag()
        {
            _pcManager.rb.drag = 0;
        }
    }
}



