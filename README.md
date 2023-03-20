# Project-Applit
Applit is based on cutting-edge technologies, to provide community IDE that is extremely powerful and yet very simple to use.
The Applit API facilitates the creation of different environments in containers by users. To initiate containers, Docker is used, and the API calls the Docker.Dotnet library provided by .NET Core.
In order to use this API, it is necessary to have Docker Desktop running and the required images presentlocally on the machine. The API can be started by running it with IIS Express. Once the API is up and running, users can test it using SwaggerUI, which provides an interface to interact with the API locally. The API also has a basic frontend implementation, which can be started by running the "npm install" and "npm start" commands. This frontend implementation enables users to initiate containers by specifying the container name and image. To call the API and initiate containers, Axios is used. When the submit button is clicked, the container starts running, and users can monitor the containers in Docker Desktop. It should be noted that the API must be running while users make requests to initiate containers from the frontend.

# You can run this project by the following steps: (Web Api using SwaggerUI)
 - Open cmd in you computer and type the following command (copy and paste this command in you cmd):-
  ``` 
  git clone https://github.com/ArbazDev/Project-Applit.git
  ```
 - Open up this project in visual studio IDE. 
 - Build up the project.
 - Before running the project you have to make sure that your Docker Desktop is in running state.
 - Now Run the project under IIS Express

# Running the Frontend by the following steps: (Frontend)
 - Navigate to the project folder, Open the cmd and paste the following command.
 ```
  cd Frontend/my-app
 ```
 - Before running the project you have to install the npm packages. Run the following command:-
 ```
  npm install
 ```
 - After installing all the packages, run the following command to run the Frontend project:-
 ```
  npm start
 ```
 - Thumbs up 👍 
