import argparse
import json
import sys
import random
import time

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
sensor_min_range = sensors_data["min_range"]
sensor_max_range = sensors_data["max_range"]
sensor_frequency = sensors_data["frequency_per_minute"]

sensor = Sensor(sensor_id, sensor_type, sensor_min_range, sensor_max_range, sensor_frequency, 'localhost')
print(f"Sensor '{sensor.sensor_id}' - '{sensor.sensor_type}' created.")

connection, channel, queue_id = rabbitmq_connection_open(sensor_type)

try:
    while True:
        time.sleep(60 / sensor.frequency)
        sensor_msg = sensor.generate_message()
        rabbitmq_message_send(sensor_msg, channel, queue_id)
except KeyboardInterrupt:
    rabbitmq_connection_close(connection)
    sys.exit()





