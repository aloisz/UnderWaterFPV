using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace PlayerController
{
    public class PC_OnLand : MonoBehaviour
    {
        private PC _pc;
        [SerializeField] private float force;
        private void Start()
        {
            _pc = transform.GetComponent<PC>();
        }
        
        private void Update()
        {
        
        }

        internal void Move(Vector3 dir)
        {
            if (_pc.rb.velocity.magnitude <= 8) _pc.rb.AddForce(dir * force, ForceMode.Force);
        }
    }

}


