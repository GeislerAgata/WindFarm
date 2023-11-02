import argparse
import json
import sys
import random

from rabbitmq import rabbitmq_connection_open, rabbitmq_message_send, rabbitmq_connection_close
from sensor import Sensor

file = open("sensors.json", "r")
sensors_json = json.load(file)
file.close()

parser = argparse.ArgumentParser()
parser.add_argument("--sensors_number", type=int)
args = parser.parse_args()

sensors_number = args.sensors_number
sensors_data = sensors_json["Sensors"][sensors_number]

sensor_id = sensors_data["sensor_id"]
sensor_type = sensors_data["sensor_type"]
sensor = Sensor(sensor_id, sensor_type, 'localhost')

print(f"Sensor '{sensor.sensor_id}' - '{sensor.sensor_type}' created.")

connection, queue_id, channel = rabbitmq_connection_open(sensor_type)

try:
    while True:
        sleep = random.uniform(10, 15)
        sensor_msg = sensor.generate_message()
        rabbitmq_message_send(sensor_msg, queue_id, channel)
except KeyboardInterrupt:
    rabbitmq_connection_close(connection)
    sys.exit()





