using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace rhcodepi
{
    public class Helix : MonoBehaviour
    {
        private bool moveable = true;
        private float angle;
        private float lastDeltaAngle, lastTouchPos;
        [SerializeField] float angleVel= 1.7f;
        public static Helix instance;
        private void Awake() => instance = this;
        void Update()
        {
            Move();
        }

        void Move()
        {
            if(moveable && Touch.isPressing)
            {
                float touchPos = Input.mousePosition.x / (float) Screen.width;
                lastDeltaAngle = lastTouchPos - touchPos;
                angle += lastDeltaAngle * 360 * angleVel;
                lastTouchPos = touchPos;
            }
            else if(lastDeltaAngle != 0)
            {
                lastDeltaAngle -= (lastDeltaAngle * 5 * Time.deltaTime);
                angle += (lastTouchPos) * 360 * angleVel;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public float GetAnglesZ(float bound)
        {
            return transform.eulerAngles.z % bound;
        }
    }
}

