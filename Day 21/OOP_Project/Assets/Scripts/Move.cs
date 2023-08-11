    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Move : MonoBehaviour
    {
        private PlayerController _player;
        private BotController _bot;
        private void Start()
        {
            _player = GetComponent<PlayerController>();
            _bot = GetComponent<BotController>();
    
        }
        public void MoveVerhical(Verhical verhical)
        {
            verhical.Move();
        }
        // Update is called once per frame
        void Update()
        {
        // 
            if ( _player != null &&_player.isPlayer == true )
            {
                MoveVerhical(_player);
            }
            if (
            _bot != null && _bot.isPlayer == false  )
            {
                MoveVerhical(_bot);
            }
        }
    }
