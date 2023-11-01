from enum import Enum
from typing import Tuple, Type

import paho.mqtt.client as mqtt
import random
import time

from dataclasses import dataclass

meters_per_second = "m/s"
celsius = "°C"
millimeters = "mm"
decibels = "cB"

wind_speed_range = (3.0, 25.0)
temperature_range = (-40.0, 50.0)
vibrations_range = (0.1, 10.0)
noise_range = (20.0, 120.0)


@dataclass
class SensorData:
    type: str
    range: Type[float, float]
    value: float
    unit: str


wind_speed_sensor = SensorData("Wind speed", wind_speed_range, meters_per_second)
temperature_sensor = SensorData("Temperature", temperature_range, celsius)
vibrations_sensor = SensorData("Vibrations", vibrations_range, millimeters)
noise_sensor = SensorData("Wind speed", noise_range, decibels)

def generate_random_data(sensor_data: SensorData):
    min_value, max_value = sensor_data.range
    sensor_data.value = random.uniform(min_value, max_value)


broker_address = "localhost"
port = 1883
topic = "sensor/data"

client = mqtt.Client("Client")

client.connect(broker_address, port=port)

while True:
    temperature = random.uniform(20.0, 25.0)
    humidity = random.uniform(40.0, 60.0)

    data = f"{{'temperature': {temperature}, 'humidity': {humidity}}}"
    print(f"Temperature: {temperature} °C, Humidity: {humidity}%")

    client.publish(topic, data)

    time.sleep(1)
