# shader-dev-server
Hot-reloader for GLSL shader development in Three.js, inspired by thebookofshaders.com. Uses Webpack to refresh your browser when a file change is detected. This environment has been optimized to use with [The Book of Shaders](http://www.thebookofshaders.com), although [Shadertoy](http://www.shadertoy.com) also works with a few modifications.

## Install and run
1. Make sure you have [Node.js](https://nodejs.org/) installed.
2. In Command Prompt (Win), or Terminal (Mac), traverse to the folder where shader-dev-server is stored.
3. Type `npm install`. Wait for node modules to finish installing.
4. Type `npm start`. Wait for `webpack: Compiled successfully.` message.
5. Point your browser to [`http://localhost:8080/`](http://localhost:8080/), you should see a simple shader animation.

## Developing
2. Edit or duplicate **/js/frag/default.fs**.
3. To view other **.fs** files, update `fShader` variable at the top of  **/js/entry.js**.
   (If Webpack doesn't recognize your new shader file, `Ctrl + C` and `npm start` in terminal again so it re-reads the new files)

## Developing for [Shadertoy.com](http://www.shadertoy.com)
- Shadertoy frag shaders use: `void mainImage( out vec4 fragColor, in vec2 fragCoord )`, but this build uses `void main()` and has `gl_FragColor` and `gl_FragCoord` already built-in, so you'll have to adjust your shaders accordingly when uploading to the site.
- Also, uncomment the uniforms in **js/entry.js** so they follow the naming convention to comply with shadertoy.com's standard:
```
    uniforms = {
        iGlobalTime: { value: 1.0 },
        iResolution: { value: new THREE.Vector2() },
        iMouse: { value: new THREE.Vector2() }
    };
```