using System;

/// <summary>
/// 入参指令。
/// </summary>
public class PushParamInstruction : Instruction
{
    public override int Length { get; } = 1;

    public override void Interpret(int[] byteCodes, ref int i)
    {
        i++;
        var value = byteCodes[i];
        PushParam(value);
    }
}