using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 字节码虚拟机。
/// </summary>
public class VirtualMachine
{
    private readonly int MaxSize = 128;

    /// <summary>
    /// 参数堆栈。
    /// </summary>
    [PropertyOrder(1)]
    [HorizontalGroup(Width = 150f)]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideRemoveButton = true, HideAddButton = true,
        ShowPaging = false, ElementColor = "StackColor")]
    [LabelText("堆栈")]
    [ShowInInspector]
    public int[] Stack { get; set; }

    /// <summary>
    /// 参数堆栈容量。
    /// </summary>
    private int StackSize { get; set; }

    [PropertyOrder(0)]
    [HorizontalGroup]
    [ShowInInspector]
    [ListDrawerSettings(DraggableItems = false, Expanded = true, ShowPaging = false, ElementColor = "ByteCodeColor")]
    [LabelText("字节码")]
    public int[] ByteCode { get; set; } = new int[0];

    /// <summary>
    /// 当前执行的位置。
    /// </summary>
    private int indexOfByteCode = -1;

    public VirtualMachine()
    {
        Stack = new int[MaxSize];
    }

    public VirtualMachine(int maxSize)
    {
        MaxSize = maxSize;
        Stack = new int[this.MaxSize];
    }

    /// <summary>
    /// 设置需要执行的字节码。
    /// </summary>
    /// <param name="byteCodes"></param>
    public void SetByteCodes(int[] byteCodes)
    {
        if (-1 == indexOfByteCode)
        {
            ByteCode = byteCodes;
            indexOfByteCode = 0;
        }
        else
        {
            Debug.LogError("有未执行完的代码。");
        }
    }

    /// <summary>
    /// 解释执行字节码
    /// </summary>
    public void InterpretAll()
    {
        for (indexOfByteCode = 0; indexOfByteCode < ByteCode.Length; indexOfByteCode++)
        {
            Interpret();
        }

        indexOfByteCode = -1;
    }

    public void InterpretStep()
    {
        if (-1 == indexOfByteCode)
        {
            return;
        }
        Interpret();
        indexOfByteCode++;
    }

    private void Interpret()
    {
        if (-1 == indexOfByteCode)
        {
            return;
        }
        var byteCode = ByteCode[indexOfByteCode];
        var instruction = GetInstruction(byteCode);
        instruction.Init(this);
        instruction.Interpret(ByteCode, ref indexOfByteCode);
    }

    /// <summary>
    /// 参数入栈。
    /// </summary>
    /// <param name="value"></param>
    public void Push(int value)
    {
        if (StackSize >= MaxSize)
        {
            Debug.LogError("越界。");
            return;
        }

        Stack[StackSize] = value;
        StackSize++;
    }

    /// <summary>
    /// 参数出栈。
    /// </summary>
    /// <returns></returns>
    public int Pop()
    {
        if (StackSize <= 0)
        {
            return -1;
        }

        StackSize--;
        var result = Stack[StackSize];
        Stack[StackSize] = 0;
        return result;
    }

    /// <summary>
    /// 根据id获取指令。
    /// </summary>
    /// <param name="byteCode"></param>
    /// <returns></returns>
    private BaseInstruction GetInstruction(int byteCode)
    {
        switch (byteCode)
        {
            case 0:
                return GetInstruction<PushParamInstruction>();
            case 1000:
                return GetInstruction<SetHpBaseInstruction>();
            case 1001:
                return GetInstruction<PlaySoundBaseInstruction>();
        }

        return GetInstruction<EmptyBaseInstruction>();
    }

    private T GetInstruction<T>() where T : BaseInstruction, new()
    {
        return new T();
    }

#if UNITY_EDITOR

    Color StackColor(int index, Color defaultColor)
    {
        return index == StackSize - 1 ? Color.green : defaultColor;
    }

    Color ByteCodeColor(int index, Color defaultColor)
    {
        return index == indexOfByteCode ? Color.green : defaultColor;
    }

#endif
}