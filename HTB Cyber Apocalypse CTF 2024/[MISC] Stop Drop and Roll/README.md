#  Stop Drop and Roll
The Fray: The Video Game is one of the greatest hits of the last... well, we don't remember quite how long. Our "computers" these days can't run much more than that, and it has a tendency to get repetitive...



## Solution
The first step was to connect to the server:

After connecting to the server, I was given the following instructions of the game:
```
===== THE FRAY: THE VIDEO GAME =====
Welcome!
This video game is very simple
You are a competitor in The Fray, running the GAUNTLET
I will give you one of three scenarios: GORGE, PHREAK or FIRE
You have to tell me if I need to STOP, DROP or ROLL
If I tell you there's a GORGE, you send back STOP
If I tell you there's a PHREAK, you send back DROP
If I tell you there's a FIRE, you send back ROLL
Sometimes, I will send back more than one! Like this: 
GORGE, FIRE, PHREAK\nIn this case, you need to send back STOP-ROLL-DROP!
```

I then wrote a small script to automate the game:
```python
from pwn import *

scenario_map = {"GORGE": "STOP", "PHREAK": "DROP", "FIRE": "ROLL"}


def get_respone(scenarios):
    response = ""
    for scenario in scenarios.split(", "):
        response += scenario_map[scenario] + "-"
    return response[:-1] + "\n"


conn = remote("83.136.250.140", 32142)
print(conn.recv())

conn.send(b"y\n")
print(conn.recvline())
scenarios = conn.recvline().decode("utf-8").strip()
conn.send(get_respone(scenarios).encode("utf-8"))

while True:
    scenarios = conn.recvline().decode("utf-8").strip().split("? ")[1]
    print(scenarios)
    conn.send(get_respone(scenarios).encode("utf-8"))
```

After solving 500 games, I received the flag:
```
HTB{1_wiLl_sT0p_dR0p_4nD_r0Ll_mY_w4Y_oUt!}
```