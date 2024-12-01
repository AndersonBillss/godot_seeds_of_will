using System;
using Godot;

namespace Utils.AnimationLink{
    public class AnimationLink{
        public Node2D parentNode;
        public AnimatedSprite2D animatedSprite;
        public Node2D rotationCenter;
        public AnimationLink[] branches;
        public AnimationLink(Node2D parentNode, AnimationLink[] branches = null){
            this.parentNode = parentNode;
            this.animatedSprite = parentNode.GetNode<AnimatedSprite2D>("AnimatedSprite");
            this.rotationCenter = parentNode.GetNode<Node2D>("RotationCenter");
            this.branches = branches ?? Array.Empty<AnimationLink>();
        }

        public void SetRotation(float degrees){
            //convert degrees into radians
            float radians = degrees / 180 * (float)Math.PI;
            SetRotationAround(animatedSprite, rotationCenter.Position, radians);
            foreach(AnimationLink item in branches){
                SetRotationAround(item.parentNode, rotationCenter.Position, radians);
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

        public void SetAnimation(string animationName){
            animatedSprite.Animation = animationName;
        }
        public void SetFrame(int frame){
            frame %= GetFrameCount();
            animatedSprite.Frame = frame;
        }
        public void NextFrame(){
            int frameCount = animatedSprite.SpriteFrames.GetFrameCount(animatedSprite.Animation);
            animatedSprite.Frame = (animatedSprite.Frame + 1) % frameCount;
        }
        public void PrevFrame(){
            int frameCount = animatedSprite.SpriteFrames.GetFrameCount(animatedSprite.Animation);
            int next = animatedSprite.Frame -1;
            animatedSprite.Frame = next<0?frameCount-1:next;
        }

        public (int, int) DistanceBetween(int frame1, int frame2){
            int totalFrames = GetFrameCount();
            frame1 %= totalFrames;
            frame2 %= totalFrames;

            int distance1 = frame2-frame1;
            int distance2;
            if(distance1 >= 0){
                distance2 = totalFrames-distance1;
            } else {
                distance2 = totalFrames+distance1;
            }

            return (distance1,distance2);
        }

        public int GetFrameCount(){
            return animatedSprite.SpriteFrames.GetFrameCount(animatedSprite.Animation);
        }

        public int GetCurrentFrame(){
            return animatedSprite.Frame;
        }

    }
}