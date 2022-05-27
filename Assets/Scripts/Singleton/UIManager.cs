using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }
    }

    public float screenWidth
    {
        get
        {
            return Screen.width;
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
