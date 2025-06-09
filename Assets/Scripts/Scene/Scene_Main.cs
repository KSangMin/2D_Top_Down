using UnityEngine;

public class Scene_Main : Scene
{
    protected override void Init()
    {
        base.Init();

        int i = DataManager.Instance.itemDict.Count;
    }
}
