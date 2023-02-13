using UnityEngine;

public class CanThrow : MonoBehaviour
{
    public static bool canThrow = true;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        canThrow = true;
    }

}
