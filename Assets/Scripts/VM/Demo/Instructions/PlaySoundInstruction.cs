/// <summary>
/// 播放声音指令。
/// </summary>
public class PlaySoundInstruction : Instruction
{
    public override void Interpret(int[] byteCodes, ref int i)
    {
        PlaySound(PopParam());
    }
}