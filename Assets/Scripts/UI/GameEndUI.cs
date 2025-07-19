using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameEndUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Text _text;

    public void Init()
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.GameEnd, this);
    }

    public void OnClickRetry()
    {
        Time.timeScale = 1;
        SimpleSingleton<MediatorManager>.Instance.ClearAll();
        SceneManager.LoadScene("IngameScene");
    }

    public void OnClickToLobby()
    {
        Time.timeScale = 1;
        SimpleSingleton<MediatorManager>.Instance.ClearAll();
        SceneManager.LoadScene("LobbyScene");
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        Time.timeScale = 0;
        SimpleSingleton<EnemyManager>.Instance.EndGame();
        SimpleSingleton<ExpManager>.Instance.EndGame();
        gameObject.SetActive(true);
        _text.text = data.ToString();
    }
}