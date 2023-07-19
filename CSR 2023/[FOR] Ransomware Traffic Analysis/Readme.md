# Ransomware Traffic Analysis
This was a forensic challenge that was split into 2 parts.
The same `.pcap` file was provided for both of them.
The first Task was to decypt the encrypted file.

I used Wireshark to analyze the file and found a lot of SMB traffic.
It seems like SMB was used after the attack to read some files including the ransom file `help_recover_instructions.TXT` and the encrypted document `important_doc.zip.micro`. I recovered these files by using Whireshark's `Export Objects` function.

By researching the file extension I found the [Trend Micro Ransomware File Decryptor](https://success.trendmicro.com/dcx/s/solution/1114221-downloading-and-using-the-trend-micro-ransomware-file-decryptor?language=en_US).
According to the website the files were encrypted using `TeslaCrypt V3` and should be able to be recovered with the tool. 
After selecting the correct ransomware the tool was able to recover the pdf and gave me the flag: `csr{h4ve_f(_)n_there!?}`.

The second challenge was to find the private decryption key that was also hidden somewhere in the network.
While searching the http traffic I found the ransomware being downloaded:
```
GET /hostile1.exe HTTP/1.1
Host: 192.168.6.148
[...]
```
From the same Ip another file called `password.txt` was also accessed:
```
GET /password.txt HTTP/1.1
Host: 192.168.6.148
Connection: keep-alive
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7
Referer: http://192.168.6.148/
Accept-Encoding: gzip, deflate
Accept-Language: de-DE,de;q=0.9,en-US;q=0.8,en;q=0.7

HTTP/1.0 200 OK
Server: SimpleHTTP/0.6 Python/3.11.2
Date: Thu, 25 May 2023 13:57:18 GMT
Content-type: text/plain
Content-Length: 16
Last-Modified: Thu, 25 May 2023 11:35:59 GMT

CSRumble2023$$$
```
This is where I then got stuck. 
I looked at a lot of traffic but I wasn't able to find any way to use this password. 
After some time the admins of the CTF gave a hint for this challenge because no one had solved it yet:
 ```
 The thread actor is really evil and might even like steganography.
 ```
 This told me that I had to find some kind of image in the `.pcap` and use the password I found to decrypt it. 
 I did not find any images directly in the `.pcap`, but I did find `Mandatory Security Update for Your NVISO Account.eml` in the `SMB object list` using the `Export Objects` function again.
 This email file contained a link to an image called [signature-hidden.jpg](https://ci3.googleusercontent.com/mail-sig/AIorK4yF0siiu2z-YcXTU8Jd6S8hselp-9S8b1XlRFh8WGa-Rq6BXMPbJmmGygfU1J8U07w2hpwmykc).
 Finally I used an online [Steganographic Decoder](https://futureboy.us/stegano/decinput.html) with the password I found earlier to decrypt the file:
 ```
 pssst the key is 440A241DD80FCC5664E861989DB716E08CE627D8D40C7EA360AE855C727A49EE 

csr{4n6_i$_f(_)N_y3$?!}
 ```