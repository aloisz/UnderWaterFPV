using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerController
{
    public class PC_OnWater : MonoBehaviour
    {
        private PC _pc;
        private void Start()
        {
            _pc = transform.GetComponent<PC>();
        }
        
        private void Update()
        {
        
        }
    }
}



