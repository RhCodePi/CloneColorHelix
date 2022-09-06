using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rhcodepi
{
    public class ColorBump : MonoBehaviour
    {
        private MeshRenderer mRenderer;
        private Color color;
        private void Awake() => mRenderer = GetComponent<MeshRenderer>();
        void Start()
        {
            if(PlayerPrefs.GetInt("Level") > 4)
                SetColor();
        }
        void SetColor()
        {
            transform.parent = null;
            color = GameManager.instance._Colors[Random.Range(0, GameManager.instance._Colors.Length)];
            mRenderer.material.color = color;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localPosition = new Vector3(0, 0.0012f, GameManager.instance._ColorBumpZ);
        }

        public Color _Color{get => color;}
    }

}
