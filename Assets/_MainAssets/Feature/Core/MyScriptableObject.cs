using System;
using SaveLoad;
using UnityEngine;

namespace Core
{
    public abstract class MyScriptableObject : ScriptableObject, ISaveable
    {
        [HideInInspector] public string id;

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString();
        }

        public virtual string GetUniqueIdentifier() => GetType().Name;

        public virtual object CaptureState() => JsonUtility.ToJson(this);

        public virtual void RestoreState(object state) => JsonUtility.FromJsonOverwrite((string)state, this);
    }
}