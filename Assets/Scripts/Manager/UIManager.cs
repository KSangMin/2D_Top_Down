using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<Type, UI> _sceneDict = new();

    Transform _root;
    Transform Root
    {
        get
        {
            if(_root == null || _root.gameObject == null)
            {
                _root = new GameObject("@UI_Root").transform;
            }
            return _root;
        }
    }

    public T GetUI<T>() where T : UI
    {
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            return existingUI as T;
        }
        Debug.Log($"There's No {uiType.Name} in UIManager");
        return null;
        //throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    public T HideUI<T>() where T : UI
    {
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            existingUI.Hide();
            return existingUI as T;
        }

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: Root);
        _sceneDict[uiType] = ui;
        ui.Hide();

        return null;
    }

    public T ShowUI<T>(Transform par) where T : UI
    {
        if(par == null) return ShowUI<T>();

        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            existingUI.Show();
            return existingUI as T;
        }

        T ui = Util.InstantiatePrefabAndGetComponent<T>(path: $"UI/{uiType.Name}", parent: par);
        _sceneDict[uiType] = ui;
        ui.Show();

        return ui;
    }

    public T ShowUI<T>() where T : UI
    {
        return ShowUI<T>(Root);
    }

    //다른 클래스들에서 호출하는 메서드
    public void RemoveUI<T>() where T: UI
    {
        Type uiType = typeof(T);

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            _sceneDict.Remove(uiType);
            Destroy(existingUI.gameObject);
            return;
        }
        else throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    //UI의 Close에서 호출하는 메서드
    public void RemoveUI(UI ui)
    {
        Type uiType = ui.GetType();

        if (_sceneDict.TryGetValue(uiType, out UI existingUI))
        {
            _sceneDict.Remove(uiType);
            Destroy(existingUI.gameObject);
            return;
        }
        else throw new InvalidOperationException($"There's No {uiType.Name} in UIManager");
    }

    public void RemoveAllUI()
    {
        foreach (UI ui in _sceneDict.Values.ToList())
        {
            ui.Clear();
            if (ui) Destroy(ui.gameObject);
        }
        _sceneDict.Clear();
    }

    public void Clear()
    {
        RemoveAllUI();
        _root = null;
    }
}
