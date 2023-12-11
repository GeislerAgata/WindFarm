FROM mongo:latest

EXPOSE 27017

COPY init.js /docker-entrypoint-initdb.d/

CMD ["mongod"]