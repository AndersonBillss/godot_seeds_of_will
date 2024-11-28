using Godot;

public partial class Player : CharacterBody2D
{
	public CollisionShape2D collisionBox;
	public Vector2 center;
	public const float Speed = 600.0f;
	public const float JumpVelocity = -1000.0f;

    public override void _Ready()
    {
		AnimationInit();
        collisionBox = GetNode<CollisionShape2D>("Collision");
		center = collisionBox.Position;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "_", "_");
		if (direction != Vector2.Zero)
		{
			float newVelocity = direction.X * Speed;
			if(Input.IsActionPressed("walk")){
				newVelocity /= 2;
			}
			velocity.X = newVelocity;
		}
		else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (direction.X < 0 && IsFacingRight()){
			Flip();
		} else if (direction.X > 0 && !IsFacingRight()){
			Flip();
		}
		
		if (direction.X != 0 && IsOnFloor()){
			if(Input.IsActionPressed("walk")){
				PlayAnimation("walk");
			} else {
				PlayAnimation("run");
			}
		} else {
			PlayAnimation("default");
		}
		if(!IsOnFloor()){
			PlayAnimation("jump");
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public bool IsFacingRight(){
		if(Rotation < 1 && Scale.Y == 1){
			return true;
		} else {
			return false;
		}
	}

	public void Flip(){
		Scale = new Vector2(-1,Scale.Y);
	}
}
