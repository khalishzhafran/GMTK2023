using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GMTK.Fisherman;

namespace GMTK.UI
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField] private Image fishermanProfile;
        [SerializeField] private TextMeshProUGUI fishermanName;

        private Fisher fisher;

        private void Start()
        {
            fisher = Fisher.Instance;

            fishermanName.text = fisher.fishermanName;
            // fishermanProfile.sprite = fisher.fishermanProfile;
        }
    }
}
