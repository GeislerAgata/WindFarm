#stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/farm-frontend /usr/share/nginx/html
#COPY nginx.conf /etc/nginx/nginx.conf
