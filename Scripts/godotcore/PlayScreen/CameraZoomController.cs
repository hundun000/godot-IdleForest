using Godot;
using System;

public partial class CameraZoomController : Camera2D
{
    [Export]
    public float ZoomSpeed = 0.1f; // 每次滚轮滚动时缩放变化的量

    [Export]
    public Vector2 MinZoom = new Vector2(0.5f, 0.5f); // 最小缩放值 (放大)

    [Export]
    public Vector2 MaxZoom = new Vector2(2.0f, 2.0f); // 最大缩放值 (缩小)

    [Export]
    public float DragSpeed = 1.0f; // 拖动速度调整，可根据需要微调
    // 新增：相机中心的世界坐标限制
    [ExportGroup("Camera Limits")]
    [Export]
    public Vector2 CameraLimitMin = new Vector2(-1000, -1000); // 相机中心能达到的最小世界坐标 (左上角)
    [Export]
    public Vector2 CameraLimitMax = new Vector2(1000, 1000);   // 相机中心能达到的最大世界坐标 (右下角)

    private bool _isDragging = false;
    private Vector2 _dragStartPositionScreen; // 鼠标按下时的屏幕坐标
    private Vector2 _dragStartCameraPosition; // 鼠标按下时相机的世界坐标

    public override void _Ready()
    {
        // 确保 Zoom 的初始值在 MinZoom 和 MaxZoom 之间
        // 如果你需要设置一个默认的初始缩放，可以在检查器中调整 Camera2D 的 Zoom 属性
        // 或者在这里设置：
        // Zoom = new Vector2(1.0f, 1.0f);
    }

    // 使用_UnhandledInput避免抢占UI按钮点击
    public override void _UnhandledInput(InputEvent @event)
    {
        // --- 鼠标滚轮缩放处理 ---
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
            {
                Vector2 newZoom = Zoom - new Vector2(ZoomSpeed, ZoomSpeed);
                Zoom = new Vector2(
                    Mathf.Max(newZoom.X, MinZoom.X),
                    Mathf.Max(newZoom.Y, MinZoom.Y)
                );
                GetViewport().SetInputAsHandled();
            }
            else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
            {
                Vector2 newZoom = Zoom + new Vector2(ZoomSpeed, ZoomSpeed);
                Zoom = new Vector2(
                    Mathf.Min(newZoom.X, MaxZoom.X),
                    Mathf.Min(newZoom.Y, MaxZoom.Y)
                );
                GetViewport().SetInputAsHandled();
            }
            // 鼠标左键按下
            else if (mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed)
            {
                _isDragging = true;
                _dragStartPositionScreen = mouseButtonEvent.Position;
                _dragStartCameraPosition = Position;
                GetViewport().SetInputAsHandled();
            }
            // 鼠标左键释放
            else if (mouseButtonEvent.ButtonIndex == MouseButton.Left && !mouseButtonEvent.Pressed)
            {
                _isDragging = false;
                GetViewport().SetInputAsHandled();
            }
        }
        // --- 鼠标移动拖动处理 ---
        else if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            if (_isDragging)
            {
                Vector2 mouseScreenDelta = mouseMotionEvent.Position - _dragStartPositionScreen;
                Vector2 worldDelta = mouseScreenDelta / Zoom;

                // 计算新的潜在相机位置
                Vector2 potentialNewPosition = _dragStartCameraPosition - worldDelta * DragSpeed;

                // 限制相机位置
                Position = new Vector2(
                    Mathf.Clamp(potentialNewPosition.X, CameraLimitMin.X, CameraLimitMax.X),
                    Mathf.Clamp(potentialNewPosition.Y, CameraLimitMin.Y, CameraLimitMax.Y)
                );
            }
        }
    }
}
