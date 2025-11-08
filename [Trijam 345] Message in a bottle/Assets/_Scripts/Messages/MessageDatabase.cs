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
            "To Be Or +Not To Be",
            "All That +Glitters Is +Not Gold",
            "I think, +therefore I +am",
            "Thats one +small step"
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