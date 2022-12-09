using System;
using Sirenix.OdinInspector;

[Serializable]
[ConfigName("加法")]
public class Add  : InstructionConfig
{
    [HideLabel]
    [HorizontalGroup]
    public int left;

    [HideLabel]
    [HorizontalGroup]
    public int right;
    
    public override int[] GetByteCode()
    {
        return null;
    }
}
