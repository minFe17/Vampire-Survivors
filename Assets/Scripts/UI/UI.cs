using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameEndUI _gameEndUI;

    void Start()
    {
        _gameEndUI.Init();    
    }
}