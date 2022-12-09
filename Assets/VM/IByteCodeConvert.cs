public interface IByteCode
{
    int[] GetByteCode();
}

public interface IByteCodeConvert : IByteCode
{
    void FromByteCode(int[] byteCode);
}