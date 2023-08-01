using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;

        // Use this for initialization
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

       
    }
}