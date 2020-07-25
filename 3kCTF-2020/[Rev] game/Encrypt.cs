using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CTF
{
	// Token: 0x02000003 RID: 3
	public class Encrypt : MonoBehaviour
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00004775 File Offset: 0x00002975
		public void Awake()
		{
			Encrypt.current = this;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00004780 File Offset: 0x00002980
		public string DecryptString(string key)
		{
			byte[] array = Convert.FromBase64String(this.cipherText);
			string result;
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cryptoStream.Write(array, 0, array.Length);
							cryptoStream.Close();
						}
						this.cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
					}
					result = this.cipherText;
				}
				catch (Exception)
				{
					result = "wrong Order mate ";
				}
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00004880 File Offset: 0x00002A80
		private string EncryptString(string clearText, string key)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.Close();
					}
					clearText = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return clearText;
		}

		// Token: 0x04000052 RID: 82
		public static Encrypt current;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private string cipherText;
	}
}
