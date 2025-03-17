using System.Collections.Generic;
using Godot;
using scripts.Player;

public partial class PlayerController : CharacterBody2D
{
	public CollisionShape2D collisionBox;
	public Vector2 center;
	public const float walkSpeed = 300.0f;
	public const float runSpeed = 500.0f;
	public const float JumpVelocity = -1200.0f;
	public SharedAnimationProperties sharedAnimationProperties = new();
	private List<Weapon> _weapons = [];
	private AnimatedSprite2D _selectedWeaponSprites;
	private AnimatedSprite2D _selectedWeaponSlashSprites;
	

	private bool inAirLastFrame;

    public override void _Ready(){
        collisionBox = GetNode<CollisionShape2D>("Collision");
		center = collisionBox.Position;
	    foreach (Node child in GetChildren())
        {
            if (child is Weapon weapon)
            {
                _weapons.Add(weapon);
            }
        }
		if(_weapons.Count > 0){
			_selectedWeaponSprites = _weapons[0].GetNode<AnimatedSprite2D>("SwordSprites");
			_selectedWeaponSlashSprites = _weapons[0].GetNode<AnimatedSprite2D>("SlashSprites");
		}
		sharedAnimationProperties.selectedWeaponSprites = _selectedWeaponSprites;
		sharedAnimationProperties.selectedWeaponSlashSprites = _selectedWeaponSlashSprites;
		AnimationInit();
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		bool onFloor = IsOnFloor();

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
		if(Input.IsActionJustReleased("jump") && velocity.Y < 0){
			velocity.Y *= .35f;
		}


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "_", "_");
		if (direction != Vector2.Zero)
		{
			velocity.X = Input.IsActionPressed("walk")? direction.X * walkSpeed : direction.X * runSpeed;
		}
		else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, runSpeed);
		}

		if (direction.X < 0 && IsFacingRight()){
			Flip();
		} else if (direction.X > 0 && !IsFacingRight()){
			Flip();
		}
		
		sharedAnimationProperties.running = false;
		if (direction.X != 0 && onFloor){
			if(Input.IsActionPressed("walk")){
				PlayAnimation("walk");
			} else {
				PlayAnimation("run");
				sharedAnimationProperties.running = true;
			}
		}
		if(direction.X == 0 && onFloor){
			PlayAnimation("default");
		}
		if(!IsOnFloor()){
			if(velocity.Y < -400){
				PlayAnimation("jump");
			} else {
				PlayAnimation("fall");
			}
		}

		if (Input.IsActionJustPressed("Attack"))
        {
			sharedAnimationProperties.holdingWeapon = true;
            Attack();
        }

		if(onFloor && inAirLastFrame){
			PlayAnimation("land");
		}

		inAirLastFrame = !onFloor;
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

	private void Attack(){
		if (_weapons.Count > 0){
			PlayAnimation("attack_one_hand_1");
			_weapons[0].Attack();
		}
	}

	public void Flip(){
		Scale = new Vector2(-1,Scale.Y);
	}
}
