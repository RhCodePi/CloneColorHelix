using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rhcodepi
{
    public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private static bool pressing;
        public static bool isPressing { get => pressing; set{}}
        public void OnPointerDown(PointerEventData eventData)
        {
            // Check the pressing in screen 
            pressing = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Check the pressing up screen 
            pressing = false;
        }
    }
}

