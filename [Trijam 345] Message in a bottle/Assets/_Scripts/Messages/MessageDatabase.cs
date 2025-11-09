using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Messages
{
    [CreateAssetMenu(fileName = "MessageDatabase", menuName = "Game/Message Database", order = 0)]
    public class MessageDatabase : ScriptableObject
    {
        [Tooltip("A list of messages that can be used in the game.")]
        public List<string> messages = new()
        {
            "TO BE OR+NOT TO BE",
            "UNITY IS FUN",
            "CODE AND PLAY",
            "GAME DEV LIFE",
            "MAKE IT WORK",
            "PLAY TO WIN",
            "STAY CURIOUS",
            "LEARN MORE",
            "NEVER GIVE UP",
            "WORK HARD",
            "STAY HUMBLE",
            "CREATE MORE",
            "DREAM BIG",
            "KEEP GOING",
            "TRUST SELF",
            "FOCUS UP",
            "SMILE DAILY",
            "BE POSITIVE",
            "STAY STRONG",
            "MOVE FORWARD",
            "GOOD VIBES",
            "DO YOUR BEST",
            "THINK BIG",
            "BE KIND",
            "HAVE FUN",
            "ENJOY LIFE",
            "LIVE HAPPY",
            "TRY AGAIN",
            "WIN THE DAY",
            "WORK SMART",
            "GO FOR IT",
            "KEEP MOVING",
            "SHINE BRIGHT",
            "BELIEVE MORE",
            "JUST START",
            "PLAY HARD",
            "FIND PEACE",
            "CHOOSE JOY",
            "LOVE LIFE",
            "STAY TRUE",
            "STAND TALL",
            "BE BRAVE",
            "NEVER QUIT",
            "CHASE DREAMS",
            "KEEP FAITH",
            "BE CREATIVE",
            "WORK TO WIN",
            "AIM HIGH",
            "BUILD STRONG",
            "CREATE JOY",
            "FOCUS NOW",
            "STAY HAPPY",
            "DO IT NOW",
            "BE BETTER",
            "TRUST FLOW",
            "TRY YOUR BEST",
            "KEEP HOPE",
            "RISE UP",
            "WORK TOGETHER",
            "CODE CREATE",
            "UNITY WINS",
            "PLAY MORE",
            "GO CREATE",
            "MAKE MAGIC",
            "BUILD MORE",
            "HAVE COURAGE",
            "ENJOY TODAY",
            "STAY CALM",
            "TEAM WORK",
            "NEVER FEAR",
            "LOVE WINS",
            "LIVE KIND",
            "WORK HARDER",
            "SMILE MORE",
            "CREATE FUN",
            "SHARE JOY",
            "TRY AGAIN+DO BETTER",
            "DREAM MORE+WORK HARD",
            "STAY COOL+KEEP GOING",
            "BE HONEST+WORK TRUE",
            "CHOOSE LOVE+STAY STRONG",
        };
        
        
        public string GetRandomMessage()
        {
            if (messages == null || messages.Count == 0)
            {
                Debug.LogWarning("No messages available in the database.");
                return string.Empty;
            }

            int randomIndex = Random.Range(0, messages.Count);
            return messages[randomIndex].ToUpper();
        }
    }
}