using System;
using Godot;

namespace Utils.Skeleton{
    public class Skeleton{
        public Node2D parentNode;
        public AnimatedSprite2D animatedSprite;
        public Node2D rotationCenter;
        public Skeleton[] branches;
        public Skeleton(Node2D parentNode, Skeleton[] branches = null){
            this.parentNode = parentNode;
            this.animatedSprite = parentNode.GetNode<AnimatedSprite2D>("AnimatedSprite");
            this.rotationCenter = parentNode.GetNode<Node2D>("RotationCenter");
            this.branches = branches ?? Array.Empty<Skeleton>();
        }

        public void SetRotation(float degrees){
            SetRotationAround(animatedSprite, rotationCenter.Position, degrees);
            foreach(Skeleton item in branches){
                SetRotationAround(item.parentNode, rotationCenter.Position, degrees);
            }
        }


        private static void SetRotationAround(Node2D rotatingItem, Vector2 relativePosition, float degrees){
            Vector2 direction = -relativePosition;
            
            // Rotate the vector
            direction = direction.Rotated(degrees);
            rotatingItem.Rotation = degrees;
            
            // Set the new position
            rotatingItem.Position = relativePosition + direction;
        }
    }
}