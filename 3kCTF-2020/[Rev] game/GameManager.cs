using System;
using UnityEngine;

namespace CTF
{
	// Token: 0x02000004 RID: 4
	public class GameManager : MonoBehaviour
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00004960 File Offset: 0x00002B60
		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Box1")
			{
				if (this.isCollidingBox1)
				{
					return;
				}
				this.isCollidingBox1 = true;
				UiManager.current.UpdateTexte(this.Box1);
				Object.Destroy(other.gameObject);
			}
			if (other.tag == "Box2")
			{
				if (this.isCollidingBox2)
				{
					return;
				}
				this.isCollidingBox2 = true;
				UiManager.current.UpdateTexte(this.Box2);
				Object.Destroy(other.gameObject);
			}
			if (other.tag == "Box3")
			{
				if (this.isCollidingBox3)
				{
					return;
				}
				this.isCollidingBox3 = true;
				UiManager.current.UpdateTexte(this.Box3);
				Object.Destroy(other.gameObject);
			}
			if (other.tag == "Box4")
			{
				if (this.isCollidingBox4)
				{
					return;
				}
				this.isCollidingBox4 = true;
				UiManager.current.UpdateTexte(this.Box4);
				Object.Destroy(other.gameObject);
			}
			if (other.tag == "Box5")
			{
				if (this.isCollidingBox5)
				{
					return;
				}
				this.isCollidingBox5 = true;
				UiManager.current.UpdateTexte(this.Box5);
				Object.Destroy(other.gameObject);
			}
			if (other.tag == "Box6")
			{
				if (this.isCollidingBox6)
				{
					return;
				}
				this.isCollidingBox6 = true;
				UiManager.current.UpdateTexte(this.Box6);
				Object.Destroy(other.gameObject);
			}
		}

		// Token: 0x04000054 RID: 84
		private bool isCollidingBox1;

		// Token: 0x04000055 RID: 85
		private bool isCollidingBox2;

		// Token: 0x04000056 RID: 86
		private bool isCollidingBox3;

		// Token: 0x04000057 RID: 87
		private bool isCollidingBox4;

		// Token: 0x04000058 RID: 88
		private bool isCollidingBox5;

		// Token: 0x04000059 RID: 89
		private bool isCollidingBox6;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private string Box1;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		private string Box2;

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private string Box3;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private string Box4;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private string Box5;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private string Box6;
	}
}
