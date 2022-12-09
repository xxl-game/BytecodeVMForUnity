using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 字节码虚拟机。
/// </summary>
public class VirtualMachine
{
    private enum VMState
    {
        Ready,
        Running,
        Finished,
    }

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
    public int[] ParamStack { get; set; }

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

    [PropertyOrder(-1)]
    [EnumToggleButtons]
    [ShowInInspector]
    private VMState State { get; set; }

    private bool IsStateCanInterpretStep
    {
        get { return State == VMState.Ready || State == VMState.Running; }
    }

    private bool IsStateCanInterpretAll
    {
        get { return State == VMState.Ready; }
    }

    public VirtualMachine()
    {
        ParamStack = new int[MaxSize];
    }

    public VirtualMachine(int maxSize)
    {
        MaxSize = maxSize;
        ParamStack = new int[this.MaxSize];
    }

    /// <summary>
    /// 设置需要执行的字节码。
    /// </summary>
    /// <param name="byteCodes"></param>
    public void SetByteCodes(int[] byteCodes)
    {
        State = VMState.Ready;
        Clear();

        ByteCode = byteCodes;
    }

    /// <summary>
    /// 解释执行所有字节码
    /// </summary>
    [Button(ButtonSizes.Medium)]
    [EnableIf("IsStateCanInterpretAll")]
    public void InterpretAll()
    {
        if (IsStateCanInterpretAll)
        {
            State = VMState.Running;
            for (indexOfByteCode = 0; indexOfByteCode < ByteCode.Length; indexOfByteCode++)
            {
                var success = InterpretCurrentInstruction();
                if (!success)
                {
                    break;
                }
            }

            indexOfByteCode = -1;
            State = VMState.Finished;
        }
        else
        {
            Debug.LogError($"Interpret when state {State}");
        }
    }

    /// <summary>
    /// 单步调试执行字节码。
    /// </summary>
    [Button(ButtonSizes.Medium)]
    [EnableIf("IsStateCanInterpretStep")]
    public void InterpretStep()
    {
        if (IsStateCanInterpretStep)
        {
            if (null == ByteCode || 0 == ByteCode.Length)
            {
                return;
            }

            indexOfByteCode++;
            var success = InterpretCurrentInstruction();
            if (!success || indexOfByteCode == ByteCode.Length)
            {
                State = VMState.Finished;
            }
        }
        else
        {
            Debug.LogError($"Interpret when state {State}");
        }
    }

    /// <summary>
    /// 参数入栈。
    /// </summary>
    /// <param name="value"></param>
    public void PushParam(int value)
    {
        if (StackSize >= MaxSize)
        {
            Debug.LogError("越界。");
            return;
        }

        ParamStack[StackSize] = value;
        StackSize++;
    }

    /// <summary>
    /// 参数出栈。
    /// </summary>
    /// <returns></returns>
    public int PopParam()
    {
        if (StackSize <= 0)
        {
            return -1;
        }

        StackSize--;
        var result = ParamStack[StackSize];
        ParamStack[StackSize] = 0;
        return result;
    }
    
    /// <summary>
    /// 执行当前位置的指令。
    /// </summary>
    private bool InterpretCurrentInstruction()
    {
        if (null == ByteCode || 0 > indexOfByteCode || ByteCode.Length <= indexOfByteCode)
        {
            Debug.LogError("Interpret failed!");
            return false;
        }

        var byteCode = ByteCode[indexOfByteCode];
        var instruction = GetInstruction(byteCode);
        if (null != instruction)
        {
            if (indexOfByteCode + instruction.Length < ByteCode.Length)
            {
                instruction.Init(this);
                instruction.Interpret(ByteCode, ref indexOfByteCode);
                return true;
            }

            Debug.LogError($"Interpret failed! {instruction.GetType()} need {instruction.Length} bytes, but not enough");
            return false;
        }

        Debug.LogError("Interpret failed! no instruction");
        return false;
    }

    [Button(ButtonSizes.Medium)]
    private void Clear()
    {
        ByteCode = null;
        indexOfByteCode = -1;

        for (var i = 0; i < ParamStack.Length; i++)
        {
            ParamStack[i] = 0;
        }

        StackSize = 0;
    }

    /// <summary>
    /// 根据id获取指令。
    /// </summary>
    /// <param name="byteCode"></param>
    /// <returns></returns>
    private Instruction GetInstruction(int byteCode)
    {
        switch (byteCode)
        {
            case 0:
                return GetInstruction<PushParamInstruction>();
            case 1000:
                return GetInstruction<SetHpInstruction>();
            case 1001:
                return GetInstruction<PlaySoundInstruction>();
        }

        return null;
    }

    private T GetInstruction<T>() where T : Instruction, new()
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