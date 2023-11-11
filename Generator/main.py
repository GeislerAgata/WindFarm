import multiprocessing
import os


def create_data_producer(sensors_number: int):
    os.system(f"python producer.py --sensors_number {sensors_number}")


if __name__ == "__main__":
    sensor_process = []
    for sensor_number in range(16):
        sensor = multiprocessing.Process(target=create_data_producer, args=(sensor_number,))
        sensor_process.append(sensor)
        sensor.start()
        print(f"Sensor number '{sensor_number}' added to processes.")

    for sensor in sensor_process:
        sensor.join()
