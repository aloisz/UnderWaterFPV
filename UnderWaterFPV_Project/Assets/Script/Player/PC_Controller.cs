using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerController;
using UnityEngine;
using UnityEngine.Serialization;


namespace PlayerController
{
    public class PC_Controller : MonoBehaviour
    {
        internal PC_Manager _pcManager;
        
        [SerializeField] internal float lookSpeed = 2.0f;
        [SerializeField] internal float lookXLimit = 45.0f;

        [SerializeField] internal float gravity = 9.81f;
        internal float baseGravity;
        [SerializeField] internal float[] drags;
        
        [Space] 
        [SerializeField] internal float maxSpeedMagnitude;
        [ReadOnly][SerializeField] internal bool isRunning;
        [SerializeField] internal float walkSpeed = 6;
        [SerializeField] internal float runSpeed = 12;
        [ReadOnly][SerializeField] internal bool canJump;
        [ReadOnly][SerializeField] internal bool isJumping;
        [SerializeField] internal float jumpForce = 500;
    
        internal virtual void Start()
        {
            _pcManager = transform.GetComponent<PC_Manager>();
            baseGravity = gravity;
        }

        internal virtual void Update()
        {
            HandleCameraRot();
            
            isJumping = Input.GetKey(KeyCode.Space);
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }

        private Vector3 HandleDirInput()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float targetSpeed = _pcManager.canMove ? (isRunning ? runSpeed : walkSpeed) : 0;
            float curSpeedX = Input.GetAxis("Vertical");
            float curSpeedY = Input.GetAxis("Horizontal");
            
            Vector3 movementDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (movementDirection.magnitude > 1) movementDirection.Normalize();
            
            _pcManager.moveDirection = movementDirection * targetSpeed;
            
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
            Jump();
            Drag();
        }
        

        internal virtual void MovePC()
        {
            if(_pcManager.rb.velocity.magnitude > maxSpeedMagnitude) return;
            _pcManager.rb.AddForce(HandleDirInput(), ForceMode.Impulse);
        }

        internal virtual bool CheckIfGrounded()
        {
            canJump = false;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.2f))
            {
                Debug.DrawRay(transform.position, -transform.up * 1, Color.red);
                canJump = hit.transform.GetComponent<Collider>();
            };
            return canJump;
        }

        internal virtual void Jump()
        {
            if (!CheckIfGrounded()) return;
            
            
            if (isJumping)
            {
                _pcManager.rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
            }
        }
        
        internal virtual void Gravity()
        {
            _pcManager.rb.AddForce(Vector3.down * gravity, ForceMode.Force); 
        }
        
        internal virtual void Drag()
        {
            _pcManager.rb.drag = CheckIfGrounded() ? (_pcManager.rb.velocity.magnitude < 3 ? drags[0] : drags[1]) : drags[2];
        }
    }
}



