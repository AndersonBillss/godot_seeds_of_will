using Godot;
using System;

public partial class Weapon : Node2D
{
	string weaponName;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		weaponName = Name;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Attack(){
		GD.Print("Attacked with Weapon: " + weaponName);
	}
}
