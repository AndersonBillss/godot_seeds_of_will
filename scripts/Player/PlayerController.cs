using System.Collections.Generic;
using System.Linq.Expressions;
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

	private List<Weapon> _weapons = new();
	private AnimatedSprite2D _selectedWeaponSprites;
	private Sprite2D _selectedWeaponSlash;

    public override void _Ready()
    {
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
			_selectedWeaponSlash = _weapons[0].GetNode<Sprite2D>("SlashSprite");
		}
		sharedAnimationProperties.selectedWeaponSprites = _selectedWeaponSprites;
		sharedAnimationProperties.selectedWeaponSlash = _selectedWeaponSlash;
		AnimationInit();
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
		
		if (direction.X != 0 && IsOnFloor()){
			if(Input.IsActionPressed("walk")){
				PlayAnimation("walk");
			} else {
				PlayAnimation("run");
			}
		}
		if(direction.X == 0 && IsOnFloor()){
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
