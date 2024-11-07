using System;
using UnityEngine;
using Utilities.SaveLoad;

[Serializable]
public abstract class MyScriptableObject : ScriptableObject, ISaveable
{
    public string id;

    protected virtual void OnValidate()
    {
        TryCatchWrapper(() =>
        {
            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString();
        });
    }
        
    public void TryCatchWrapper(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in {name}: {e.Message}", this);
        }
    }

    public virtual string GetUniqueIdentifier() => GetType().Name;

    public virtual object CaptureState() => JsonUtility.ToJson(this);

    public virtual void RestoreState(object state) => JsonUtility.FromJsonOverwrite((string)state, this);

    public override string ToString() => $"{GetType().Name} ({id})";
        
}