import time
from sense_hat import SenseHat
from datetime import datetime
BROADCAST_TO_PORT = 7000
from socket import * 

sense = SenseHat()
sense.clear()

s = socket(AF_INET, SOCK_DGRAM)
s.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)

while True:
  hum = str(sense.get_humidity())
  time.sleep(60)
  now = datetime.now().time().minute
  if now == 15 or now == 30 or now == 45 or now == 0:
    s.sendto(bytes(hum), ('<broadcast>', BROADCAST_TO_PORT))
    print(hum)
