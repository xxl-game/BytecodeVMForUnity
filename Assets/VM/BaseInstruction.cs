/// <summary>
/// 指令。
/// </summary>
public abstract class BaseInstruction
{
    public void Init(VirtualMachine virtualMachine)
    {
        VM = virtualMachine;
    }

    protected VirtualMachine VM { get; private set; }

    public abstract void Interpret(int[] byteCodes, ref int i);

    public void Push(int value)
    {
        VM?.Push(value);
    }

    public int Pop()
    {
        return VM.Pop();
    }

    public void SetHp(int value, int max)
    {
    }

    public void PlaySound(int value)
    {
    }
}