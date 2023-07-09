using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK {
    public class FishingObject : MonoBehaviour {
        public FishingObjectSO fishingObjectSO;



        public FishingObjectSO Data {
            get
            {
                return fishingObjectSO;
            }
        }



        public void Catch() {
            Destroy(gameObject);
        }
    }
}
