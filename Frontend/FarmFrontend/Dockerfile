#stage 1
FROM node:alpine as node
WORKDIR /usr/local/app
COPY ./ /usr/local/app/
RUN npm install
RUN npm run build --prod

#stage 2
FROM nginx:alpine
COPY --from=node /usr/local/app/dist/farm-frontend/browser /usr/share/nginx/html
EXPOSE 80
