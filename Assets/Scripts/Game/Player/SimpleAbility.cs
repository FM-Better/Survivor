using System;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace Survivor
{
	public partial class SimpleAbility : ViewController
	{
		[SerializeField] private float attackDistance;
		private float timer;
		[SerializeField] private float hurtDurationTime;

		private Transform playerTrans;

		private void Start()
		{
			playerTrans = FindObjectOfType<Player>().transform;
		}
		
		private void Update()
		{
			timer += Time.deltaTime;	

			if (timer >= Global.SimpleAbilityCD.Value)
			{
				var enemies = FindObjectsOfType<Enemy>(false);
				foreach (var enemy in enemies)
				{
					if ((playerTrans.position - enemy.transform.position).magnitude <= attackDistance)
					{
						enemy.Hurt(Global.SimpleAbilityDamage.Value, hurtDurationTime);
					}
				}
				timer = 0f;

			}
		}
	}
}
