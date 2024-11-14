using Godot;

public partial class CameraSmoothing : Camera2D
{
    [Export] public float camSpeed = 5;
    [Export] public Node2D Target;

    public override void _PhysicsProcess(double delta)
	{
        Vector2 BetweenCamAndTarget = GlobalPosition - Target.GlobalPosition;
        Vector2 newPosition = GlobalPosition - (BetweenCamAndTarget * (float)delta * camSpeed);
        GlobalPosition = newPosition;
	}

}
