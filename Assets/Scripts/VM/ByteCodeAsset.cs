
using UnityEngine;

[CreateAssetMenu]
public class ByteCodeAsset : ScriptableObject, IByteCodeConvert
{
    public int[] GetByteCode()
    {
        return new int[5];
    }

    public void FromByteCode(int[] byteCode)
    {
        
    }
}