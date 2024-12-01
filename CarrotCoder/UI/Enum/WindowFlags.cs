namespace CarrotCoder.UI.Enum;

public enum WindowFlags
{
    None = 0,
    NoTitleBar = 1,
    NoResize = 2,
    NoMove = 4,
    NoScrollbar = 8,
    NoScrollWithMouse = 16, // 0x00000010
    NoCollapse = 32, // 0x00000020
    AlwaysAutoResize = 64, // 0x00000040
    NoBackground = 128, // 0x00000080
    NoSavedSettings = 256, // 0x00000100
    NoMouseInputs = 512, // 0x00000200
    MenuBar = 1024, // 0x00000400
    HorizontalScrollbar = 2048, // 0x00000800
    NoFocusOnAppearing = 4096, // 0x00001000
    NoBringToFrontOnFocus = 8192, // 0x00002000
    AlwaysVerticalScrollbar = 16384, // 0x00004000
    AlwaysHorizontalScrollbar = 32768, // 0x00008000
    AlwaysUseWindowPadding = 65536, // 0x00010000
    NoNavInputs = 262144, // 0x00040000
    NoNavFocus = 524288, // 0x00080000
    UnsavedDocument = 1048576, // 0x00100000
    NoDocking = 2097152, // 0x00200000
    NoNav = NoNavFocus | NoNavInputs, // 0x000C0000
    NoDecoration = NoCollapse | NoScrollbar | NoResize | NoTitleBar, // 0x0000002B
    NoInputs = NoNav | NoMouseInputs, // 0x000C0200
    NavFlattened = 8388608, // 0x00800000
    ChildWindow = 16777216, // 0x01000000
    Tooltip = 33554432, // 0x02000000
    Popup = 67108864, // 0x04000000
    Modal = 134217728, // 0x08000000
    ChildMenu = 268435456, // 0x10000000
    DockNodeHost = 536870912, // 0x20000000
}