using System;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace Survivor
{
	public partial class SimpleAbility : ViewController
	{
		[SerializeField] private float attackDistance;
		[SerializeField] private float attackCD;
		private float timer;
		[SerializeField] private float hurtDuringTime;

		private Transform playerTrans;

		private void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}
		
		private void Update()
		{
				timer += Time.deltaTime;	

			if (timer >= attackCD)
			{
				var enemies = FindObjectsOfType<Enemy>(false);
				foreach (var enemy in enemies)
				{
					if ((playerTrans.position - enemy.transform.position).magnitude <= attackDistance)
					{
						enemy.Sprite.color = Color.red;
						enemy.Hurt();
						
						ActionKit.Delay(hurtDuringTime,() =>
						{
							if (enemy)
							{
								enemy.Sprite.color = Color.white;	
							}
						}).StartGlobal();
					}
				}
				timer = 0f;

			}
		}
	}
}
