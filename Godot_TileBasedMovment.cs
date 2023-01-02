using Godot;
using System;

public class Player : KinematicBody2D
{
    public Vector2 velocity = new Vector2();
    public Vector2 direction = new Vector2();
    public Vector2 position = new Vector2();
    public AnimatedSprite anim_player = new AnimatedSprite();
    public Vector2 startPosition = new Vector2(64,64);
    public float gravity = 98f; //unused
    public int TileSize = 16; // How big the tiles are
    public float speed = 1.98f; //unused
    public bool canMove = false;
    public bool moving = false;

    public override void _Ready()
    {
        base._Ready();

        // starting position
        position = startPosition;
    }

    public override void _PhysicsProcess(float delta)
    {
        // Call methods to run
        _direction();
        _movement();

    }
    public void _direction(){  
        // Setup input strength values that give -1,0,1 depending on the key pressed.
        RayCast2D ray = this.GetNode<RayCast2D>("canMove");
        if(Input.IsActionPressed("ui_down") == false && Input.IsActionPressed("ui_up") == false){
            direction.x = Input.GetActionStrength("ui_right")-Input.GetActionStrength("ui_left");
        }
        if(Input.IsActionPressed("ui_right") == false && Input.IsActionPressed("ui_left") == false){
            direction.y = Input.GetActionStrength("ui_down")-Input.GetActionStrength("ui_up");
        }

        // Make sure the RayCast2D checks if we can move to the tile we want to move to, direction needs to be reset to 0 for this to work properly every call.
        if(direction.x > 0 && direction.y == 0){
            ray.RotationDegrees = -90;
            }
        else if(direction.x < 0 && direction.y == 0){
            ray.RotationDegrees = 90;
            }
        else{
            direction.x = 0;
            }
        // Make sure to rotate the RayCast for left and right too,  direction needs to be reset to 0 for this to work properly every call
        if(direction.y < 0 && direction.x == 0){
            ray.RotationDegrees = 180;
            }
        else if(direction.y > 0 && direction.x == 0){
            ray.RotationDegrees = 0;
            }
        else{
            direction.y = 0;
            }
    }
    public void _movement(){
        // Setup kinematicbody collisions
        velocity = MoveAndSlide(velocity,Vector2.Up);  

        // setup Animation player
        anim_player = this.GetNode<AnimatedSprite>("AnimatedSprite");  
        
        /* Cast RayCast2D and assign Player Position field to Snapped().
            Returns this vector with each component snapped to the nearest multiple of step. This can also be used to round to an arbitrary number of decimals.
            Returns: The snapped vector.
        */
        RayCast2D ray = this.GetNode<RayCast2D>("canMove");
        Position = position.Snapped(Vector2.One * TileSize);

        // Then check for raycast collisions
        ray.ForceRaycastUpdate();
        if(!ray.IsColliding()){
            canMove = true;
            GD.Print("I can move");
        }
        else if(ray.IsColliding()){
            canMove = false;
            GD.Print("I can't move");
        }

        // If no raycast collisions and canMove is true then we can move the player and make sure to update the RayCast2D rotation.
        if(!ray.IsColliding() && canMove){
            if(direction.x > 0){
                position += Vector2.One * TileSize/TileSize * direction;ray.RotationDegrees = -90;
                anim_player.Play("walkright");moving = true;
                }
            if(direction.x < 0){
                position += Vector2.One * TileSize/TileSize * direction;ray.RotationDegrees = 90;
                anim_player.Play("walkleft");moving = true;
                }
            if(direction.y < 0){
                position += Vector2.One * TileSize/TileSize * direction;ray.RotationDegrees = 180;
                anim_player.Play("walkup");moving = true;
                }
            if(direction.y > 0){
                position += Vector2.One * TileSize/TileSize * direction;ray.RotationDegrees = 0;
                anim_player.Play("walkdown");moving = true;
                }
            }
        else{
            moving = false;
        }

    }

}

