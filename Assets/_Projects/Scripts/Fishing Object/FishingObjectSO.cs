using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK {
    [CreateAssetMenu(fileName = "New FishingObjectSO", menuName = "GMTK/Fishing Object/FishingObjectSO")]
    public class FishingObjectSO : ScriptableObject {
        public new string name;
        public Sprite sprite;

        public float weight;
        public float value;



        public void OnValidate() {
            name = name.Trim();
        }
    }
}
