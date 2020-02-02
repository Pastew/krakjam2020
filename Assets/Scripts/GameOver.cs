using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private int _errorsToLose = 5;
    [SerializeField] private Text _errorsLeftToFireText;
    
    private int _errors;

    void Start()
    {
        FindObjectOfType<ScrewEventInvoker>().AddScrewBreakListener(PlayerError);
        FindObjectOfType<ScrewEventInvoker>().AddScrewUnfinishedListener(PlayerError);
        SetText(_errorsToLose );        

    }

    private void PlayerError()
    {
        _errors++;
        SetText(_errorsToLose - _errors);        
        if(_errors >= _errorsToLose)
            transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SetText(int x)
    {
        _errorsLeftToFireText.text = $"Pozostałe błędy: {x}";
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
