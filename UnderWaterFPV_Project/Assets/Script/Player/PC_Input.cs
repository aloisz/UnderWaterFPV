using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
    public class PC_Input : MonoBehaviour
    {
        private PC_Manager _pcManager;
        internal Vector3 dir;
        private void Start()
        {
            _pcManager = transform.GetComponent<PC_Manager>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _pcManager.ChangeState(PC_State.OnLand);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _pcManager.ChangeState(PC_State.OnWater);
            }
        }
    }
}


