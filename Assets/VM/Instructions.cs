/// <summary>
/// 入参指令。
/// </summary>
public class PushParamInstruction : BaseInstruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
        i++;
        var value = byteCodes[i];
        Push(value);
    }
}

/// <summary>
/// 设置血量指令。
/// </summary>
public class SetHpBaseInstruction : BaseInstruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
        var p1 = Pop();
        var p2 = Pop();
        SetHp(p1, p2);
    }
}

/// <summary>
/// 播放声音指令。
/// </summary>
public class PlaySoundBaseInstruction : BaseInstruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
        PlaySound(Pop());
    }
}

/// <summary>
/// 空指令。
/// </summary>
public class EmptyBaseInstruction : BaseInstruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
    }
}