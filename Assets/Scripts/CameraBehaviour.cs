using DefaultNamespace;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<ScrewEventInvoker>().AddScrewBreakListener(() =>
        {
            GetComponent<Shaker>().Shake();
        });
    }
}
