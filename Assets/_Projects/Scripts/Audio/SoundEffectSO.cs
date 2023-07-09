using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(menuName = "GMTK/Audio/SoundEffectSO")]
    public class SoundEffectSO : ScriptableObject
    {
        public AudioClip ReelIn;
        public AudioClip ReelInMiddle;
        public AudioClip ReelInEnd;
        public AudioClip ReelFailed;
        public AudioClip AnglerReactionVarieties_Mad;
        public AudioClip AnglerReactionVarieties_Happy;
        public AudioClip AcceptButton;
        public AudioClip BackButton;
        public AudioClip MoodPointGained;
        public AudioClip MoodPointLost;
        public AudioClip ThrowTrash;
        public AudioClip HookClicked;
        public AudioClip HookDrop;
    }
}
