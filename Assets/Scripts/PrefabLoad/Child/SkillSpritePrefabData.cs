using System.Threading.Tasks;
using UnityEngine.U2D;

public class SkillSpritePrefabData : PrefabLoadBase
{
    SpriteAtlas _skillIconSprite;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "SkillIconAtlas";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _skillIconSprite = await _addressableManager.GetAddressableAsset<SpriteAtlas>(_name);
    }

    public override T GetPrefab<T>()
    {
        if (typeof(T) == typeof(SpriteAtlas))
            return (T)(object)_skillIconSprite;
        return default(T);
    }
}