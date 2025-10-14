using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _Scripts.Managers
{
    public class MenuScript : MonoBehaviour
    {
        [SerializeField] private bool musicIsMuted = false;
        [SerializeField] private float defaultMusicVolume = 0.5f;
        
        public AudioSource musicSource;
        

        private void Start()
        {
            if (!PlayerPrefs.HasKey("MusicIsMuted"))
            {
                musicIsMuted = false;
                PlayerPrefs.SetInt("MusicIsMuted", 0);
            }
            else
            {
                ToggleMuteMusic(PlayerPrefs.GetInt("MusicIsMuted") == 1);
            }
            
        }


        public void OnMuteMusicClick(bool musicIsMutedButton)
        {
            Debug.Log(musicIsMuted);
            ToggleMuteMusic(musicIsMutedButton);
        }

        private void ToggleMuteMusic(bool choice)
        {
                musicIsMuted = choice;
                PlayerPrefs.SetInt("MusicIsMuted", musicIsMuted ? 1 : 0);
                if (musicIsMuted)
                {
                    musicSource.Pause();
                }
                else
                {
                    musicSource.volume = defaultMusicVolume;
                    musicSource.Play();
                }
        }

        public void OnStartGameClick()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}