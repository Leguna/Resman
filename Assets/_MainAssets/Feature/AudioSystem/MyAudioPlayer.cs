using Events;
using UnityEngine;
using Utilities;

namespace AudioSystem
{
    public class MyAudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioClipData clipData;
        [SerializeField] AudioSource audioSource;

        private void Start()
        {
            EventManager.AddEventListener<PlayAudioEvent>(OnPlayAudioEvent);
        }

        private void OnPlayAudioEvent(PlayAudioEvent data)
        {
            switch (data.audioName)
            {
                case "cookingSound":
                    audioSource.clip = clipData.cookingSound;
                    break;
                case "servingSound":
                    audioSource.clip = clipData.servingSound;
                    break;
                case "comboAchievedSound":
                    audioSource.clip = clipData.comboAchievedSound;
                    break;
            }
            audioSource.Play();
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<PlayAudioEvent>(OnPlayAudioEvent);
        }
    }
}