using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public gameover gmscreen;
		//public ProjectileGunTutorial gun;
		public int lives;
		public int kills;
		public bool isPower = false;
		public TextMeshProUGUI lifedisplay;
		public TextMeshProUGUI killdisplay;




		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		void Start()
		{
			kills = 0;
			lives = 400;

		}

		public void killscore()
        {
			
				kills += 1;
				

			
		}

		void OnTriggerEnter(Collider collision)
		{
			if (scoreboard.currentPowerScore < 30 && isPower == false)
			{
				
				if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "enemy")
				{
					lives = lives - 1;
					soundmanager.PlaySound("mindamage");
					//SetHealthText();
				}
				if (lives == 0)
				{
					//Destroy(gameObject);


					GameOver();
				}

			}


			

            else if(collision.gameObject.tag == "enemy" && isPower == true)
            {
				soundmanager.PlaySound("guarddeath");

				Destroy(collision.gameObject);
			}

			

		}

		public void TakeDamage(int damage)
		{
			lives = lives - damage;
			soundmanager.PlaySound("mindamage");
			if (lives <= 0)
			{
				//Destroy(gameObject);


				GameOver();
			}

		}

		//public void addammo()
		//{
		//	gun.magazineSize += 10;

		//}




		public void GameOver()
		{
			gmscreen.setup();
		}

		private void Update()
		{

			if (lifedisplay != null)
				lifedisplay.SetText("Lives: "+lives );

			if (killdisplay != null)
				killdisplay.SetText("Kills: " + kills);


			if (gameObject.transform.position.y < -50)
			{
				GameOver();
			}


			


		}


#endif

	}

}