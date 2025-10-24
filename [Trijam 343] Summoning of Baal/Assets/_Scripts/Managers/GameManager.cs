using System;
using UnityEngine;

namespace a
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            Debug.Log("Game Manager Started");
        }
    }
}