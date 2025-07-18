using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    async void Start()
    {
        await SimpleSingleton<PrefabManager>.Instance.LoadPrefab();
        MonoSingleton<TimeManager>.Instance.Init();
    }
}