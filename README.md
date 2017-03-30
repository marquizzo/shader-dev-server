# shader-dev-server
Hot-reloader for shader development, inspired by thebookofshaders.com and using Webpack

## Install and run
1. Make sure you have [Node.js](https://nodejs.org/) installed.
2. In Command Prompt (Win), or Terminal (Mac), traverse to the folder where shader-dev-server is stored.
3. Type `npm install`. Wait for node modules to finish installing.
4. Type `npm start`. Wait for `webpack: Compiled successfully.` message.
5. Point your browser to `http://localhost:8080/` and you should see a Simplex Noise shader.

## Developing shaders
1. To make your own shader, add a file ending in `.fs` in the `/js/fs/` folder.
2. In `/js/index.js`, update line 4 so it points to your new `.fs` file.
3. Webpack should hot-reload the browser with your new shader.

## Compile code
`npm run-script compile`