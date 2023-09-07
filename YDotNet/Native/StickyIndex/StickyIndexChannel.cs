using System.Runtime.InteropServices;

namespace YDotNet.Native.StickyIndex;

internal static class StickyIndexChannel
{
    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "ysticky_index_from_index")]
    public static extern nint FromIndex(nint branch, nint transaction, uint index, sbyte associationType);
}
