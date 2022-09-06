using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rhcodepi
{
    public class WallFragment : MonoBehaviour
    {
        private MeshRenderer mRenderer;
        private void Start()
        {
            mRenderer = GetComponent<MeshRenderer>();
            ColorFragment();
        }
        void ColorFragment()
        {
            if (transform.CompareTag("Passes"))
            {
                /// set fragment passes color;
                if (PlayerPrefs.GetInt("Level") > 4)
                {
                    if (transform.position.z > GameManager.instance._ColorBumpZ)
                    {
                        GameManager.instance._PassesColor = GameObject.Find("ColorBump(Clone)").GetComponent<ColorBump>()._Color;
                        mRenderer.material.color = GameManager.instance._PassesColor;
                    }
                    else
                    {
                        GameManager.instance._PassesColor = Ball._CurrentColor;
                        mRenderer.material.color = Ball._CurrentColor;
                    }
                }
                else {
                    mRenderer.material.color = GameManager.instance._PassesColor;
                }
            }
            else
            {
                //set fragment cancel color
                if (GameManager.instance._CancelColor == GameManager.instance._PassesColor)
                {
                    GameManager.instance._CancelColor = GameManager.instance._Colors[Random.Range(0, GameManager.instance._Colors.Length)];
                }
                mRenderer.material.color = GameManager.instance._CancelColor;
            }
        }
    }
}


