using UnityEngine;

namespace _Scripts.Enemies
{
    public class Windy : WaypointFollower
    {
       
 

        private void Awake()
        {
            speed = 7f;
            waitTime = 2f;
            if (waypoints.Length == 0)
            {
                Debug.LogError("No points assigned for Windy movement.");
            }
        }




    }
}