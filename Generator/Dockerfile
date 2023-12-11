FROM python:3.11.3

# Ustaw katalog roboczy w kontenerze
WORKDIR /app

# Skopiuj plik zależności
COPY requirements.txt /app

# Zainstaluj zależności
RUN pip install --no-cache-dir -r requirements.txt

# Skopiuj pozostałe pliki
COPY . /app

ENTRYPOINT ["python", "main.py"]