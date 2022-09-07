using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace rhcodepi
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Vector3 offset;
        [SerializeField] Animator flashAnim;
        
        void LateUpdate()
        {
            transform.position = new Vector3(0, 0, Ball._PosZ) + offset;
        }

        public void SetFlash()
        {
            flashAnim.SetTrigger("Flash");
        }
    }
}

