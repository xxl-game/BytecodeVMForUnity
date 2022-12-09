using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// 
/// </summary>
[Serializable]
[ConfigName("设置属性")]
public class SetProperty : InstructionConfig
{
    [LabelWidth(100)]
    [HorizontalGroup]
    public string name;

    [LabelWidth(100)]
    [HorizontalGroup]
    public int value;
    
    public override int[] GetByteCode()
    {
        var stack = new Stack<int>();
        
        var id = Instruction.GetId<PushParamInstruction>();
        stack.Push(id);
        stack.Push(value);

        return stack.ToArray();
    }
}

[Serializable]
[ConfigName("获取属性")]
public class GetProperty : InstructionConfig
{
    
    
    public override int[] GetByteCode()
    {
        return null;
    }
}
