using UnityEngine;

public class SaverGlobalControllers : MonoBehaviour
{
    private void Start()
    {
        var objs = GameObject.FindGameObjectsWithTag("GloabalControllers");

        if (objs.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
