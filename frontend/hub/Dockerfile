FROM node:lts-alpine AS build
WORKDIR /app
COPY frontend/hub/ ./
RUN npm ci && npm run build

FROM nginx:alpine
COPY --from=build /app/dist/hub /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

#FROM node:lts-alpine
#ENV NODE_ENV=production
#WORKDIR /usr/src/app
#COPY ["frontend/hub/package.json", "frontend/hub/package-lock.json*", "frontend/hub/npm-shrinkwrap.json*", "./"]
#RUN npm install --production --silent && mv node_modules ../
#COPY . .
#EXPOSE 80
#RUN chown -R node /usr/src/app
#USER node
#CMD ["npm", "start"]
