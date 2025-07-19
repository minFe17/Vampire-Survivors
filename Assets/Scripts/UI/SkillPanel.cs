using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] Image _skillIcon;
    [SerializeField] Text _name;

    public Image SkillIcon { get => _skillIcon; }
    public Text Name { get => _name; }
}
