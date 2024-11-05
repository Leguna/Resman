﻿using System;
using SaveLoad;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class BaseSo : ScriptableObject, ISaveable
    {
        public virtual string GetUniqueIdentifier()
        {
            return GetType().Name;
        }

        public virtual object CaptureState()
        {
            return JsonUtility.ToJson(this);
        }

        public virtual void RestoreState(object state)
        {
            JsonUtility.FromJsonOverwrite((string)state, this);
        }
    }
}