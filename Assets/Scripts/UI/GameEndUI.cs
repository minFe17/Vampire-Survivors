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
        SceneManager.LoadScene("IngameScene");
    }

    public void OnClickToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        gameObject.SetActive(true);
        _text.text = data.ToString();
    }
}