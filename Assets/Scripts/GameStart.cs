using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        MonoSingleton<TimeManager>.Instance.Init();
    }
}