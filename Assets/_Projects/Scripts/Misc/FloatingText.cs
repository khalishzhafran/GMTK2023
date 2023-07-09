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
        private Animator animator;

        private void Awake()
        {
            floatingText = GetComponent<TextMeshProUGUI>();
            animator = GetComponent<Animator>();
        }

        public void ShowText(string text)
        {
            SetText(text);
            Show();
        }

        private void SetText(string newText)
        {
            floatingText.text = newText;
        }

        private void Show()
        {
            animator.SetBool("Show", true);
        }

        private void Hide()
        {
            animator.SetBool("Show", false);
        }
    }
}
