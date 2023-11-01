import paho.mqtt.client as mqtt


broker_address = "localhost"
port = 1883
topic = "sensor/data"

def on_message(client, userdata, message):
    print(f"Otrzymano wiadomość na temacie: {message.topic}")
    print(f"Treść wiadomości: {message.payload.decode()}")

    # Tutaj umieść kod do weryfikacji formatu danych
    # Możesz użyć funkcji do analizy formatu, np. bibliotekę json, jeśli dane są w formacie JSON.


client = mqtt.Client("Subscriber")
client.on_message = on_message

client.connect(broker_address, port=port)

client.subscribe(topic)

client.loop_forever()
