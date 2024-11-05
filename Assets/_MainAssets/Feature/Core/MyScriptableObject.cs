using System;
using UnityEngine;

namespace SO
{
    public class MyScriptableObject : ScriptableObject
    {
        public string id;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString();
        }
    }
}