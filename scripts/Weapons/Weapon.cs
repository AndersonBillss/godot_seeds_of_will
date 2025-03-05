using Godot;

public partial class Weapon : Node2D
{
	string weaponName;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		weaponName = Name;
	}

	public void Attack(){
		GD.Print("Attacked with Weapon: " + weaponName);
	}
}
