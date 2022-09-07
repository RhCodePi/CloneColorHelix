using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rhcodepi
{
    public class Score : MonoBehaviour
    {
        [SerializeField] Text score;
        void Start()
        {

        }

        void Update()
        {
            SetScore();
        }

        void SetScore()
        {
            score.text = $"{GameManager.instance._Score}";
        }
    }
}

