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
        [SerializeField] private float gravity;
        [SerializeField] private float[] drags;


        [SerializeField] private AnimationCurve forceAnim;
        [SerializeField] private AnimationCurve dragsAnim;
        private void Start()
        {
            _pc = transform.GetComponent<PC>();
        }
        
        private void Update()
        {
        
        }
        private void FixedUpdate()
        {
            Move();
            Gravity();
            Drag();
        }
        

        private void Move()
        {
            if (_pc.rb.velocity.magnitude <= 8) _pc.rb.AddForce(_pc.input.dir * forceAnim.Evaluate(_pc.rb.velocity.magnitude), ForceMode.Force);
        }
        
        private void Gravity()
        {
            _pc.rb.AddForce(Vector3.down * gravity); 
        }
        
        private void Drag()
        {
            _pc.rb.drag = drags[0];
        }
    }

}


