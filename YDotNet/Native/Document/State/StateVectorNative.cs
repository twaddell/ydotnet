using System.Runtime.InteropServices;
using YDotNet.Infrastructure;

namespace YDotNet.Native.Document.State;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct StateVectorNative
{
    public uint EntriesCount { get; }

    public nint ClientIdsHandle { get; }

    public nint ClocksHandle { get; }

    public ulong[] ClientIds()
    {
        return MemoryReader.ReadStructs<ulong>(ClientIdsHandle, EntriesCount);
    }

    public uint[] Clocks()
    {
        return MemoryReader.ReadStructs<uint>(ClientIdsHandle, EntriesCount);
    }
}
