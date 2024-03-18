using System;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		[SerializeField] private float moveSpeed;

		public static Player Default;

		private void Awake()
		{
			Default = this;
		}

		private void Start()
		{
			HurtBox.OnTriggerEnter2DEvent((colider) =>
			{
				var hitBox = colider.GetComponent<HitBox>();
				if (hitBox)
				{
					if (hitBox.Owner.CompareTag("Enemy"))
					{
						Global.Hp.Value--;
						if (Global.Hp.Value <= 0)
						{
							AudioKit.PlaySound(Sound.DIE);
							this.DestroyGameObjGracefully();
							UIKit.OpenPanel<UIGameOverPanel>();	
						}
						else
						{
							AudioKit.PlaySound(Sound.HURT);
						}
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Hp.RegisterWithInitValue((hp) =>
			{
				UpdateHpUI();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.MaxHp.RegisterWithInitValue((hp) =>
			{
				UpdateHpUI();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		void UpdateHpUI() => HpValue.fillAmount = Global.Hp.Value / (float)Global.MaxHp.Value;
		
		private void FixedUpdate()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var direction = new Vector2(horizontal, vertical).normalized;
            
			selfRigidbody.velocity =  Vector2.Lerp(selfRigidbody.velocity, direction * moveSpeed, 1 - Mathf.Exp(-Time.deltaTime * 5));
		}
	}
}
