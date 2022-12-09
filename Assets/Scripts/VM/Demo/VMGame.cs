using Sirenix.OdinInspector;
using UnityEngine;

public class VMGame : MonoBehaviour
{
    public ByteCodeAsset byteCodeProviderConvert;
    
    [HorizontalGroup]
    [HideLabel]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    private VirtualMachine VM { get; set; } = new VirtualMachine(16);

    void Execute()
    {
        VM.InterpretAll();
    }
}