import * as THREE from "three";

// Relative path to frag shader
var fShader = require("./frag/default.fs");
var vShader = require("./vert/vShader.vs");

// THREE.js setup
// Ported from THREE.js example in http://thebookofshaders.com/04/
var camera, scene, renderer;
var uniforms;

init();
animate();

function init() {
    camera = new THREE.Camera();
    camera.position.z = 1;

    scene = new THREE.Scene();

    var geometry = new THREE.PlaneBufferGeometry( 2, 2 );

    // Using uniform naming convention from thebookofshaders.com
    uniforms = {
        u_time: { value: 1.0 },
        u_resolution: { value: new THREE.Vector2() },
        u_mouse: { value: new THREE.Vector2() }
    };
    
    /*
    // Using uniform naming convention from Shadertoy
    uniforms = {
        iGlobalTime: { value: 1.0 },
        iResolution: { value: new THREE.Vector2() },
        iMouse: { value: new THREE.Vector2() }
    };
    */

    var material = new THREE.ShaderMaterial( {
        uniforms: uniforms,
        vertexShader: vShader,
        fragmentShader: fShader
    } );

    var mesh = new THREE.Mesh( geometry, material );
    scene.add( mesh );

    renderer = new THREE.WebGLRenderer();

    document.body.appendChild( renderer.domElement );

    onWindowResize(null);
    window.addEventListener( 'resize', onWindowResize, false );

    document.onmousemove = onMouseMove;
}

function onMouseMove(event){
    // The Book of Shaders
    uniforms.u_mouse.value.x = event.pageX;
    uniforms.u_mouse.value.y = event.pageY;

    // Shadertoy
    // uniforms.iMouse.value.x = event.pageX;
    // uniforms.iMouse.value.y = event.pageY;
}

function onWindowResize(event) {
    renderer.setSize( window.innerWidth, window.innerHeight );
    // The Book of Shaders
    uniforms.u_resolution.value.x = renderer.domElement.width;
    uniforms.u_resolution.value.y = renderer.domElement.height;

    // Shadertoy
    // uniforms.iResolution.value.x = renderer.domElement.width;
    // uniforms.iResolution.value.y = renderer.domElement.height;
}

function animate() {
    requestAnimationFrame( animate );
    render();
}

function render() {
    // The Book of Shaders
    uniforms.u_time.value += 0.05;

    // Shadertoy
    // uniforms.iGlobalTime.value += 0.05;

    renderer.render( scene, camera );
}