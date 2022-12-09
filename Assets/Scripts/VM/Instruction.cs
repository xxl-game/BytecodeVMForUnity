using System;

/// <summary>
/// 指令。
/// </summary>
public abstract class Instruction
{
    /// <summary>
    /// 虚拟机实例，在子类中调用虚拟机实例方法。
    /// </summary>
    private VirtualMachine VM { get; set; }

    public virtual int Length { get; } = 0;

    /// <summary>
    /// 初始化指令，指定虚拟机。
    /// </summary>
    /// <param name="virtualMachine"></param>
    public void Init(VirtualMachine virtualMachine)
    {
        VM = virtualMachine;
    }
    
    /// <summary>
    /// 解释字节码命令。
    /// </summary>
    /// <param name="byteCodes"></param>
    /// <param name="i"></param>
    public abstract void Interpret(int[] byteCodes, ref int i);

    public void PushParam(int value)
    {
        VM?.PushParam(value);
    }

    public int PopParam()
    {
        return VM.PopParam();
    }

    public void SetHp(int value, int max)
    {
    }

    public void PlaySound(int value)
    {
    }

    public static int GetId<T>() where T : Instruction, new()
    {
        return 1;
    }
}