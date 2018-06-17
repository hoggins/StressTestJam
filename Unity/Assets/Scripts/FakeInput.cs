using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FakeInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public static bool IsActive;
        public static float Value;
        
        private Image _indic;

        private void Awake()
        {
            _indic = GameObject.Find("FakeIndic").GetComponent<Image>();
            _indic.enabled = false;
        }

        public void DoActivateFake()
        {
            IsActive = !IsActive;
                _indic.enabled = IsActive;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Value = 1;
        }
    }
}