using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace PlayerController
{
    public class PCManagerOnLand : PC_Controller
    {
        [Space]
        [SerializeField] internal float minSpeedToWallRun;
        [SerializeField] internal float hitDist;
        [SerializeField] internal float hitRadius;
        
        private RaycastHit[] wallHits;
        [SerializeField] private bool[] wallHitsDetects;
        [SerializeField] private Vector3 hitWallNormalDir;
        internal override void Start()
        {
            base.Start();
            wallHits = new RaycastHit[2];
            wallHitsDetects = new bool[2];
        }
        
        internal override void Update()
        {
            base.Update();

            CheckBorder();

            foreach (var hit in wallHitsDetects)
            {
                int count = 0;
                if (hit) count++;
                if (count > 0)
                {
                    gravity = 0;
                    return;
                }
                else
                {
                    gravity = baseGravity;
                }
            }
        }


        private void CheckBorder()
        {
            if (CheckIfGrounded())
            {
                wallHitsDetects[0] = false;
                wallHitsDetects[1] = false;
                return;
            }
            if (_pcManager.rb.velocity.magnitude < minSpeedToWallRun)
            {
                wallHitsDetects[0] = false;
                wallHitsDetects[1] = false;
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                wallHitsDetects[i] = Physics.SphereCast(transform.position, hitRadius, i == 0 ? transform.right : -transform.right, out wallHits[i], hitDist);
                if(wallHitsDetects[i]) hitWallNormalDir = wallHits[i].normal;
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!Application.isPlaying) return;
            for (int i = 0; i < 2; i++)
            { 
                if (wallHitsDetects[i])
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(transform.localPosition + (i == 0 ? transform.right * (hitDist) : -transform.right * (hitDist)), hitRadius);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(transform.localPosition + (i == 0 ? transform.right * (hitDist) : -transform.right * (hitDist)), hitRadius);
                }
            }
        }
        #endif
        
        
    }

}


