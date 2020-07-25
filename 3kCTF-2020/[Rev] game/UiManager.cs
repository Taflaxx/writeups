using System;
using UnityEngine;
using UnityEngine.UI;

namespace CTF
{
	// Token: 0x02000005 RID: 5
	public class UiManager : MonoBehaviour
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00004ADB File Offset: 0x00002CDB
		public void Awake()
		{
			UiManager.current = this;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void UpdateTexte(string textToAdd)
		{
			this.counter++;
			Text text = this.textHolder;
			text.text += textToAdd;
			if (this.counter == 6)
			{
				this.cText = Encrypt.current.DecryptString(this.textHolder.text);
				this.textHolder.text = this.cText;
			}
		}

		// Token: 0x04000060 RID: 96
		[SerializeField]
		private Text textHolder;

		// Token: 0x04000061 RID: 97
		public static UiManager current;

		// Token: 0x04000062 RID: 98
		[SerializeField]
		private string cText;

		// Token: 0x04000063 RID: 99
		private int counter;
	}
}
