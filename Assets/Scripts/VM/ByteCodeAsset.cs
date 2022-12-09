
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class ByteCodeAsset : ScriptableObject, IByteCodeProviderConvert
{
    [HideReferenceObjectPicker]
    [ListDrawerSettings(Expanded = true, ShowPaging = false)]
    [SerializeReference]
    public InstructionConfig[] instructionConfigs;

    public int[] GetByteCode()
    {
        return new int[5];
    }

    public void FromByteCode(int[] byteCode)
    {
        
    }
}