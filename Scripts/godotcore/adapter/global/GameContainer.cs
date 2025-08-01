using Godot;
using GodotIdleForest.Scripts;
using GodotIdleForest.Scripts.godotcore;
using System;
using System.Runtime.InteropServices;
using System.Text;

public partial class GameContainer : Node
{
	public static DemoIdleGame Game
	{
		get; private set;
	}

    public static SceneManager SceneManager
    {
        get; private set;
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int AllocConsole();

        // 用于设置控制台输出代码页
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleOutputCP(uint wCodePageID);

        // 用于设置控制台输入代码页
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCP(uint wCodePageID);

        public static void SetupConsoleForUtf8()
        {
            // 分配控制台
            if (AllocConsole() == 0)
            {
                // AllocConsole 返回 0 表示失败，通常是因为已经分配了控制台。
                // 在 Godot 这种GUI应用中，首次 AllocConsole 才会返回非零值。
                // 如果已经分配了，这里可能不需要处理，或者可以忽略错误。
                // 如果在其他上下文调用，可能需要根据 GetLastError() 进行更详细的错误检查。
            }

            // 设置控制台输出代码页为 UTF-8 (65001)
            if (!SetConsoleOutputCP(65001))
            {
                // 处理错误，例如打印到调试输出
                Console.WriteLine($"Failed to set console output code page to UTF-8. Error: {Marshal.GetLastWin32Error()}");
            }

            // 设置控制台输入代码页为 UTF-8 (65001)
            if (!SetConsoleCP(65001))
            {
                // 处理错误
                Console.WriteLine($"Failed to set console input code page to UTF-8. Error: {Marshal.GetLastWin32Error()}");
            }

            // 告诉 .NET Runtime 它的控制台输出编码现在是 UTF-8
            // 这一步很重要，它影响 Console.WriteLine 的行为
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
	{
        // 只有在 Windows 平台上才需要设置控制台
        #if GODOT_WINDOWS
        NativeMethods.SetupConsoleForUtf8();
        #endif

        SceneManager = GodotUtils.FindFirstChildOfType<SceneManager>(this);
        Game = new DemoIdleGame();
		Game.create();
	}

}
