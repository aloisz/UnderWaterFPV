using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerController
{
    public class PC : MonoBehaviour
    {
        [SerializeField] internal Transform eyePos;
        [SerializeField] internal PC_State PCState;

        internal PC_OnLand _pcOnLand;
        internal PC_OnWater _pcOnWater;
        internal Rigidbody rb;
        private void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
            _pcOnLand = transform.GetComponent<PC_OnLand>();
            _pcOnWater = transform.GetComponent<PC_OnWater>();
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
                _pcOnLand.enabled = true;
                _pcOnWater.enabled = false;
            }
            else
            {
                _pcOnLand.enabled = false;
                _pcOnWater.enabled = true;
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
