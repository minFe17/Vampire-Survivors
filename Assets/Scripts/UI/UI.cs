using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameEndUI _gameEndUI;
    [SerializeField] SelectSkillUI _selectSkillUI;

    void Start()
    {
        _gameEndUI.Init();
        _selectSkillUI.Init();
    }
}