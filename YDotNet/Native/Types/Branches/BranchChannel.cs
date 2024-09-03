using System.Runtime.InteropServices;

namespace YDotNet.Native.Types.Branches;

internal static class BranchChannel
{
    public delegate void ObserveCallback(nint state, uint length, nint eventsHandle);

    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "yobserve_deep")]
    public static extern nint ObserveDeep(nint type, nint state, ObserveCallback callback);

    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "ytype_kind")]
    public static extern byte Kind(nint branch);

    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "ybranch_id")]
    public static extern BranchIdNative Id(nint branch);

    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "ybranch_get")]
    public static extern nint Get(nint branchId, nint transaction);

    [DllImport(
        ChannelSettings.NativeLib,
        CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "ybranch_alive")]
    public static extern byte Alive(nint branchId);
}
