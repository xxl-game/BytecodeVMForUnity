/// <summary>
/// 设置血量指令。
/// </summary>
public class SetHpInstruction : Instruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
        var p1 = PopParam();
        var p2 = PopParam();
        SetHp(p1, p2);
    }
}