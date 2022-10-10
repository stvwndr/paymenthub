FROM node:lts-alpine AS build
WORKDIR /app
COPY frontend/hub/ ./
RUN npm ci && npm run build

FROM nginx:alpine
COPY /frontend/hub/nginx-custom.conf /etc/nginx/conf.d/
COPY --from=build /app/dist/hub /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
