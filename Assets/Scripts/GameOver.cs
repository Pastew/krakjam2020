using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private int _errorsToLose = 5;
    private int _errors;

    void Start()
    {
        FindObjectOfType<ScrewEventInvoker>().AddScrewBreakListener(PlayerError);
        FindObjectOfType<ScrewEventInvoker>().AddScrewUnfinishedListener(PlayerError);
    }

    private void PlayerError()
    {
        _errors++;
        
        print("playererror");
        if(_errors >= _errorsToLose)
            transform.GetChild(0).gameObject.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
