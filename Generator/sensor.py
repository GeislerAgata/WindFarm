import json
import sys
from random import random
from datetime import datetime
from faker import Faker
from faker.providers import date_time
import random

faker = Faker()
faker.add_provider(date_time)


class Sensor:
    def __init__(self, sensor_id, sensor_type, host):
        self.sensor_id = sensor_id
        self.sensor_type = sensor_type
        self.host = host
        self.unit = ""

        self.wind_speed_range = (3.0, 25.0)
        self.temperature_range = (-40.0, 50.0)
        self.vibrations_range = (0.1, 10.0)
        self.noise_range = (20.0, 120.0)

        self.meters_per_second = "m/s"
        self.celsius = "°C"
        self.millimeters = "mm"
        self.decibels = "cB"

    def random_data(self, value_range) -> float:
        min_range, max_range = value_range
        return random.uniform(min_range, max_range)

    def generate_random_measurement(self) -> float:

        value = 0.0
        match self.sensor_type:
            case "wind_speed":
                value = self.random_data(self.wind_speed_range)
                self.unit = self.meters_per_second
            case "temperature":
                value = self.random_data(self.temperature_range)
                self.unit = self.celsius
            case "vibrations":
                value = self.random_data(self.vibrations_range)
                self.unit = self.millimeters
            case "noise":
                value = self.random_data(self.noise_range)
                self.unit = self.decibels
            case _:
                print("Error: Undefined sensor type")
                sys.exit()
        return value

    def generate_message(self):
        timestamp = int(datetime.timestamp(faker.date_time()))
        dt_object = datetime.fromtimestamp(timestamp)
        formatted_date = dt_object.strftime("%Y-%m-%d %H:%M:%S")

        timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

        value = self.generate_random_measurement()
        msg = {
            "timestamp": timestamp,
            "sensor_id": self.sensor_id,
            "sensor_type": self.sensor_type,
            "value": value,
            "unit": self.unit
        }
        print(f"Message created '{msg}'.")
        return json.dumps(msg)


