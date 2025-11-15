using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
    }
}
