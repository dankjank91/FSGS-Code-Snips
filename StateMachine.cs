using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STATEMACHINE;

namespace STATEMACHINE
{
    public class StateMachine : MonoBehaviour
    {   
        public enum States{
            titleScreen, gameOverScreen, gameScreen, bonusScreen, highScoreScreen, warpScreen,idle, move, attack, remove,idleOnCircle,pointA,pointB,pointC,moveTocenter,waitingOncircle,moveAway,orbitTarget,spiralOutwards
        }
        string TRANSITION = null;
        public string transition{get{return TRANSITION;}set{TRANSITION = value;}}
        public string state = string.Empty;
        public string previous_state = string.Empty;
        float d;

        void Start(){

            
        }
        void Update()
        {
            d = Time.deltaTime;
            if(state != null)
            {
                _state_logic();
            }
            string transition = _transition(d,TRANSITION);
    		if (transition != null){
    			set_state(transition);
            }
      
        }
 
        public virtual string _transition(float delta,string transition){return transition;}
        public void set_state(string new_state){
            previous_state = state;
            state = new_state;

            if (previous_state != null){
    		    _exit_state(previous_state, new_state);
            }
	        if( new_state != null){
	    	    _enter_state(new_state, previous_state);
            };
        }
        public virtual void _state_logic(){_enter_state(state,previous_state);_exit_state(previous_state,state);}
        public void _enter_state(string new_state,string old_state){
            new_state = state;
            if(new_state!=previous_state){
                old_state = previous_state;
            }
        }
        public void _exit_state(string old_state,string new_state){
            old_state = previous_state;
            if(old_state!=state){
                new_state = state;
            }
        }
    

    }
}