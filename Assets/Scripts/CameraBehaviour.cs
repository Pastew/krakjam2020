using DefaultNamespace;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<ScrewEventInvoker>().AddScrewBreakListener(() =>
        {
            print("Break listen");
            GetComponent<Shaker>().Shake();
        });
    }
}
