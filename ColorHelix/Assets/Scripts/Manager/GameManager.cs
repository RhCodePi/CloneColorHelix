using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rhcodepi
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Color[] colors;
        private Color passesColor, cancelColor;
        void Awake()
        {
            GenerateColor();
        }

        void Update()
        {

        }

        void GenerateColor()
        {
            passesColor  = colors[Random.Range(0, colors.Length)];
            cancelColor = colors[Random.Range(0, colors.Length)];

            while(passesColor == cancelColor)
                cancelColor = colors[Random.Range(0, colors.Length)];
            
            Ball._CurrentColor = passesColor;
        }
    }

}
