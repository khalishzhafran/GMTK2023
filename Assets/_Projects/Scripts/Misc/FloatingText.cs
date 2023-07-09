using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GMTK.Misc
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float textFadeSpeed = 1f;
        private TextMeshProUGUI floatingText;

        private void Awake()
        {
            floatingText = GetComponent<TextMeshProUGUI>();
        }

        public void ShowText(string text)
        {
            SetText(text);
            StartCoroutine(ShowTextCoroutine());
        }

        private IEnumerator ShowTextCoroutine()
        {
            Show();
            yield return new WaitForSeconds(textFadeSpeed);
            Hide();
        }

        private void SetText(string newText)
        {
            floatingText.text = newText;
        }

        private void Show()
        {
            floatingText.enabled = true;
        }

        private void Hide()
        {
            floatingText.enabled = false;
        }
    }
}
