using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerController
{
    public class PC_Manager : MonoBehaviour
    {
        [SerializeField] internal Transform playerCamera;
        [SerializeField] internal Transform eyePos;
        [SerializeField] internal PC_State PCState;

        internal PCManagerOnLand PCManagerOnLand;
        internal PCManagerOnWater PCManagerOnWater;
        internal PC_Input input;
        internal Rigidbody rb;
        
        internal Vector3 moveDirection = Vector3.zero;
        internal float rotationX = 0;
        [SerializeField] internal bool canMove;
        private void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
            PCManagerOnLand = transform.GetComponent<PCManagerOnLand>();
            PCManagerOnWater = transform.GetComponent<PCManagerOnWater>();
            input = transform.GetComponent<PC_Input>();
            ChangeState(PC_State.OnLand);
        }
        
        
        private void Update()
        {
            StateHandler();
        }

        private void StateHandler()
        {
            switch (PCState)
            {
                case PC_State.OnLand:
                    break;
                case PC_State.OnWater:
                    
                    break;
                default:
                    Debug.LogError("STATE NOT FOUND");
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal PC_State ChangeState(PC_State newState)
        {
            if (newState == PC_State.OnLand)
            {
                PCManagerOnLand.enabled = true;
                PCManagerOnWater.enabled = false;
            }
            else
            {
                PCManagerOnLand.enabled = false;
                PCManagerOnWater.enabled = true;
            }
            
            return this.PCState = newState;
        }


        private void OnGUI()
        {
            GUI.color = Color.black;
            GUIStyle StatesLabel = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(),
                padding = new RectOffset(),
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };
                
            GUI.Label(new Rect(10, 10, 100, 20), rb.velocity.magnitude.ToString(), StatesLabel);
        }
    }
}


[System.Serializable]
public enum PC_State
{
    OnLand,
    OnWater
}
