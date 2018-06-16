﻿using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MicLevelIndicator : MonoBehaviour
    {
        private Image _indic;

        private void OnEnable()
        {
            //indic = GameObject.Find("Cube");
            _indic = GameObject.Find("MicIndic").GetComponent<Image>();
        }

        private void Update()
        {
            var level = MicInputAdapter.FinalLevel;
            //_indic.transform.localScale = new Vector3(1,level,1);
            _indic.rectTransform.sizeDelta = new Vector2(200*level, 30);
        }
    }
}