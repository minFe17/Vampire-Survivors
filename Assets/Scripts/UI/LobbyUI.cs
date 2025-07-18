using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class LobbyUI : MonoBehaviour
{
    Animator _animator;

    async void Start()
    {
        if (!SimpleSingleton<PrefabManager>.Instance.CheckPrefab())
            await SimpleSingleton<PrefabManager>.Instance.LoadPrefab();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShow", true);
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("IngameScene");
    }
}