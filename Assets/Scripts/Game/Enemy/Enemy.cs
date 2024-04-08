using UnityEngine;
using QFramework;
using QAssetBundle;

namespace Survivor
{
	public partial class Enemy : GamePlayObject, IEnemy
	{
		[SerializeField] private float moveSpeed;
		public float Hp => hp;
		[SerializeField] private float hp;
		[SerializeField] private float hurtDurationTime;
		private bool isHurt;
		[SerializeField] private Color dissolveColor = Color.yellow;

		private Collider2D mHitBox;
		protected override Collider2D collider => mHitBox;
		[SerializeField] private bool isTreasureEnemy;
		
		void Start()
		{
			EnemySpawner.enemyCount.Value++; // 计数
			isHurt = false;
			
			mHitBox = transform.Find("HitBox").GetComponent<Collider2D>();
			Sprite.OnBecameVisibleEvent(() =>
			{
				mHitBox.enabled = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Sprite.OnBecameInvisibleEvent(() =>
			{
				mHitBox.enabled = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (isHurt) 
				return;
			
			if (Player.Default)
			{
				var direction = transform.NormalizedDirection2DTo(Player.Default);
				selfRigidbody.velocity = direction * moveSpeed;
			}
			else
			{
				selfRigidbody.velocity = Vector2.zero;
			}
		}

		public void Hurt(float damage, bool isCritical = false)
		{
			if (isHurt) return;

			isHurt = true;
			selfRigidbody.velocity = Vector2.zero;

			AudioKit.PlaySound(Sound.HIT);
			FloatingTextController.ShowFloatingText(transform.position + Vector3.up * 0.5f, damage.ToString("0")
				,isCritical); // 伤害飘字效果
			
			Sprite.color = Color.red;
			ActionKit.Delay((hurtDurationTime), () =>
			{
				this.Sprite.color = Color.white;
				isHurt = false;
				hp -= damage;
				if (hp <= 0)
				{
					Dead();
				}
			}).Start(this);
		}

		public void PopulateHp(float nowWaveHpScale)
		{
			hp *= nowWaveHpScale;
		}

		public void PopulateSpeed(float nowWaveSpeedScale)
		{
			moveSpeed *= nowWaveSpeedScale;
		}

		public void Dead()
		{
			DropManager.Default.SpawnDrop(transform.position, isTreasureEnemy);
			EnemySpawner.enemyCount.Value--;
			FxConrtoller.Play(Sprite, dissolveColor);
			AudioKit.PlaySound(Sound.ENEMYDIE);
			this.DestroyGameObjGracefully();
		}
	}
}
