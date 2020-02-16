import datetime
import time
import wiringpi
import requests
import sys

def flipSwitch(state):
    wiringpi.wiringPiSetupGpio()
    wiringpi.pinMode(18, wiringpi.GPIO.PWM_OUTPUT)
    wiringpi.pwmSetMode(wiringpi.GPIO.PWM_MODE_MS)
    
    wiringpi.pwmSetClock(192)
    wiringpi.pwmSetRange(2000)

    delay_period = 0.01

    if state:
        for pulse in range(50, 150, 1):
            wiringpi.pwmWrite(18, pulse)
            time.sleep(delay_period)
    else:
        for pulse in range(150, 50, -1):
            wiringpi.pwmWrite(18, pulse)
            time.sleep(delay_period)


if __name__ == "__main__":
    starttime = time.time()
    state = False
    while True:
        print(state)
        try:
            response = requests.get('http://590f34c8.ngrok.io/open_close?action=get_state')
            response = response.json()
            temp = response['Open']
            
            if temp != state:
                state = temp
                flipSwitch(state)
            time.sleep(0.2 - ((time.time() - starttime) % 0.2))
        except (KeyboardInterrupt, SystemExit):
           flipSwitch(False)
           sys.exit()
        except:
            print("Connection refused by the server..")
            print("Let me sleep for 5 seconds")
            print("ZZzzzz...")
            time.sleep(5)
            flipSwitch(False)
            print("Was a nice sleep, now let me continue...")
            continue
