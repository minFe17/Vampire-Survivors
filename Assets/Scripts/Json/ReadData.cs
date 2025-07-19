using UnityEngine;
using Utils;

public class ReadData
{
    public void ReadSkillData()
    {
        PrefabLoadBase dataPrefabLoad = SimpleSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Data);

        TextAsset textAsset = dataPrefabLoad.GetPrefab<TextAsset>();
        string data = textAsset.text;
        JsonUtility.FromJsonOverwrite(data, SimpleSingleton<SkillDataList>.Instance);
        SimpleSingleton<SkillManager>.Instance.Init();
    }
}