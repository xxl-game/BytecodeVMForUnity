using System;

[Serializable]
public class PlaySound : InstructionConfig
{
    public int soundId;

    public override int[] GetByteCode()
    {
        return null;
    }
}
