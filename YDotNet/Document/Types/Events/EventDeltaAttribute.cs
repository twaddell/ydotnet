using System.Runtime.InteropServices;
using YDotNet.Document.Cells;
using YDotNet.Infrastructure;

namespace YDotNet.Document.Types.Events;

/// <summary>
///     The formatting attribute that's part of an <see cref="EventDelta" /> instance.
/// </summary>
public class EventDeltaAttribute
{
    private readonly Lazy<Output> value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EventDeltaAttribute" /> class.
    /// </summary>
    /// <param name="handle">The handle to the native resource.</param>
    public EventDeltaAttribute(nint handle)
    {
        Handle = handle;

        Key = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(handle));

        value = new Lazy<Output>(() =>
        {
            return new Output(handle + MemoryConstants.PointerSize, false);
        });
    }

    /// <summary>
    ///     Gets the attribute name.
    /// </summary>
    public string Key { get; }

    /// <summary>
    ///     Gets the attribute value.
    /// </summary>
    public Output Value => value.Value;

    /// <summary>
    ///     Gets the handle to the native resource.
    /// </summary>
    internal nint Handle { get; }
}
