using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
    public class PC_Input : MonoBehaviour
    {
        private PC _pc;
        private void Start()
        {
            _pc = transform.GetComponent<PC>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _pc.ChangeState(PC_State.OnLand);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _pc.ChangeState(PC_State.OnWater);
            }


            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(horizontal, 0, vertical);
            
            if (_pc.PCState == PC_State.OnLand)
            {
                _pc._pcOnLand.Move(dir.normalized);
            }
            else
            {
                
            }
        }
    }
}

