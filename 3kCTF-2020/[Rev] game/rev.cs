using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CTF
{
	// Token: 0x02000003 RID: 3
	public class Encrypt
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00004775 File Offset: 0x00002975
		public void Awake()
		{
			Encrypt.current = this;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00004780 File Offset: 0x00002980
		public string DecryptString(string key)
		{
			this.cipherText = "jR9MDCzkFQFzZtHjzszeYL1g6kG9+eXaATlf0wCGmnf62QJ9AjmemY0Ao3mFaubhEfVbXfeRrne/VAD59ESYrQ==";
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
		private string cipherText;
	}
}
					
public class Program
{
	public static void Main()
	{
		CTF.Encrypt enc = new CTF.Encrypt();
		string[] words = new string[] {"Hannibal", "Tanit", "Astarté", "Amilcar", "Melqart", "Dido"};
		foreach (string word1 in words)
		{
			foreach (string word2 in words)
			{
				foreach (string word3 in words)
				{
					foreach (string word4 in words)
					{
						foreach (string word5 in words)
						{
							foreach (string word6 in words)
							{
								string word = word1+word2+word3+word4+word5+word6;
								string text = enc.DecryptString(word);
								if (text != "wrong Order mate ")
								{
									Console.WriteLine(text);
								}
							}
						}
					}
				}
			}
		}
	}
}
