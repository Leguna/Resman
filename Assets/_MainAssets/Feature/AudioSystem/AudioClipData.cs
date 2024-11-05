using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioClipData", menuName = "RestaurantGame/AudioClipData")]
    public class AudioClipData : ScriptableObject {
        public AudioClip cookingSound;
        public AudioClip servingSound;
        public AudioClip comboAchievedSound;
    }
}