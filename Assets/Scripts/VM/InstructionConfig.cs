using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public abstract class InstructionConfig : IByteCodeProvider
{
    [PropertyOrder(-1)]
    [HideLabel]
    [GUIColor(0f, 0.94f, 0.81f)]
    [ShowInInspector]
    public string Info
    {
        get { return Name; }
    }

    [HideInInspector]
    public string Name;
    
    public abstract int[] GetByteCode();

    public InstructionConfig()
    {
        var configName = GetType().GetCustomAttribute<ConfigNameAttribute>();
        Name = configName?.Name;
    }
}