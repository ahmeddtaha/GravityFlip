# Gravity Flip

**Gravity Flip** is a fast-paced 2D endless runner developed in Unity. The player controls a character running continuously across a space-themed environment, with the unique ability to flip gravity at any time. The main objective is to avoid randomly spawning obstacles by switching between the floor and ceiling, collect coins to increase score, and survive as long as possible. The game also features a login/register system and an online leaderboard.

## 3rd Party Plugins & Tools Used
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html) – for stylized UI text
- [RestClient for Unity](https://assetstore.unity.com/packages/tools/network/rest-client-for-unity-102501) – to handle API requests to the backend server
- SQLite3 via Node.js – for storing user and score data in the backend
- JWT (jsonwebtoken) – for secure user authentication

## How to Run the Project

### Game (Unity):
1. Clone or download the repository.
2. Open the project in Unity 2021.3 LTS or later.
3. Press Play to test locally.
4. Make sure the backend server is running to access login/leaderboard functionality.

### Backend Server:
1. Navigate to the `game-backend` folder in a terminal.
2. Run `npm install` to install required packages.
4. Run the server using `npm run dev`.

The game will connect automatically to `http://localhost:3000/` for login, registration, and leaderboard functions.
