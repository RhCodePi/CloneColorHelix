using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rhcodepi
{
    public class Ball : MonoBehaviour
    {
        private static float posZ = -8f;
        private float height = 0.58f;
        private bool move;
        [SerializeField] float speed, accleration;
        void Start() => move = false;
        
        void Update()
        {
            if(Touch.isPressing)
                move = true;
            if(move)
                Ball.posZ += speed * accleration;
            transform.position = new Vector3(0, height, Ball.posZ);
        }
    }

}
