import pika
from pika.adapters.blocking_connection import BlockingChannel


def rabbitmq_connection_open(sensor_type: str) -> (pika.BlockingConnection, BlockingChannel, str):
    host = 'localhost'
    connection = pika.BlockingConnection(pika.ConnectionParameters(host))
    channel = connection.channel()
    queue_id = sensor_type
    channel.queue_declare(queue=queue_id, durable=True)
    return connection, queue_id, channel


def rabbitmq_connection_close(connection: pika.BlockingConnection):
    connection.close()


def rabbitmq_message_send(msg, queue_id, channel):
    channel.basic_publish(
        exchange='',
        routing_key='queue_id',
        body=msg,
        properties=pika.BasicProperties(
            delivery_mode=2,
            content_type='application/json'
        )
    )
    print(f"Sent '{msg}' to RabbitMQ")
