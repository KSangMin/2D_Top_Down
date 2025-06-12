using UnityEngine;

public class Scene_Main : Scene
{
    protected override void Init()
    {
        base.Init();

        UIManager.Instance.ShowUI<UI_Collection>();
        UIManager.Instance.ShowUI<UI_Hud>();
    }
}
