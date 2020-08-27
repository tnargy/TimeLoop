using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Death.Respawn(Instantiate((GameObject)Resources.Load("Player")));
    }
}
