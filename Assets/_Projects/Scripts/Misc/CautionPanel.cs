using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.UI
{
    public class CautionPanel : MonoBehaviour
    {
        private Animator animator;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (canvasGroup.alpha == 0)
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                Invoke("SetToBlink", 1f);
            }
        }

        private void SetToBlink()
        {
            animator.SetBool("Blink", true);
        }

        public void Hide()
        {
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                animator.SetBool("Blink", false);
            }
        }
    }
}
